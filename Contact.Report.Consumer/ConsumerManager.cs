using Contact.Report.Common;
using Contact.Report.Common.Contract;
using Contact.Report.Common.Contracts;
using Contact.Report.Common.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Report.Consumer
{
    public class ConsumerManager : IConsumerService
    {
        private SemaphoreSlim _semaphore;

        private EventingBasicConsumer _consumer;
        private IModel _channel;
        private IConnection _connection;

        private readonly IRabbitMQService _rabbitMQServices;
        private readonly IObjectFormatConvert _objectConvertFormat;
        private readonly IMongoProvider _mongoProvider;

        public ConsumerManager(
            IRabbitMQService rabbitMQServices,
            IObjectFormatConvert objectConvertFormat,
            IMongoProvider mongoProvider
            )
        {
            _rabbitMQServices = rabbitMQServices;
            _objectConvertFormat = objectConvertFormat;
            _mongoProvider = mongoProvider;
        }

        public async Task Start()
        {
            try
            {
                _semaphore = new SemaphoreSlim(RabbitMQConsts.ParallelThreadsCount);
                _connection = _rabbitMQServices.GetConnection();
                _channel = _rabbitMQServices.GetModel(_connection);
                _channel.QueueDeclare(queue: RabbitMQConsts.RabbitMqConstsList.ContactReport.ToString(),
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                _channel.BasicQos(0, RabbitMQConsts.ParallelThreadsCount, false);
                _consumer = new EventingBasicConsumer(_channel);
                _consumer.Received += Consumer_Received;
                await Task.FromResult(_channel.BasicConsume(queue: RabbitMQConsts.RabbitMqConstsList.ContactReport.ToString(), autoAck: false, consumer: _consumer));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message.ToString());
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            try
            {
                _semaphore.Wait();
                var request = _objectConvertFormat.JsonToObject<ContactReportProducerRequestModel>(Encoding.UTF8.GetString(ea.Body));
                var contactIdList = _mongoProvider.GetContactIdListByLocation(request, Common.Enum.MongoCollectionType.Information);
                var contactList = _mongoProvider.GetContactListListByUuid(contactIdList, Common.Enum.MongoCollectionType.Contact);
                ContactReportDto resultData = new ContactReportDto
                {
                    Location = request.Location,
                    Contacts = contactList.Select(x => new ReportDto
                    {
                        Firm = x.Firm,
                        Lastname = x.Lastname,
                        Name = x.Name,
                        UUID = x.UUID
                    }).ToList()
                };

                var jsonResult = JsonConvert.SerializeObject(resultData);
                Task.Run(() =>
                {
                    try
                    {
                        Console.WriteLine(jsonResult);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.InnerException.Message.ToString());
                    }
                    finally
                    {
                        _channel.BasicAck(ea.DeliveryTag, false);
                        _semaphore.Release();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message.ToString());
            }
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            _channel.Dispose();
            _semaphore.Dispose();
        }
    }
}
