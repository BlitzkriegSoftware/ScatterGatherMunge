using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StuartWilliams.ScatterGatherMunge.Lib.Delegates;
using StuartWilliams.ScatterGatherMunge.Lib.Enums;
using StuartWilliams.ScatterGatherMunge.Lib.Interfaces;
using StuartWilliams.ScatterGatherMunge.Lib.Models;
using System;
using System.Linq;
using System.Text;

namespace StuartWilliams.ScatterGatherMunge.Lib.Clients
{
    /// <summary>
    /// Client: RabbitMQ Blitzkrieg Style
    /// </summary>
    public class RabbitMQClient : IQueueEngine
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        private readonly RabbitMqEngineConfiguration _engineConfiguration;

        // Default CTOR not allowed
        private RabbitMQClient() { }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="config">IConfigurationRoot</param>
        public RabbitMQClient(ILogger logger, IConfiguration config)
        {
            this._logger = logger;
            this._config = config;

            this._engineConfiguration = RabbitMqEngineConfiguration.CreateDefault();
            foreach (var c in this._config.AsEnumerable())
            {
                this._engineConfiguration.SetProperty(c.Key, c.Value);
            }

            this._logger.LogDebug(this._engineConfiguration.ToString());
        }

        /// <summary>
        /// Keep Listening
        /// </summary>
        public bool KeepListening { get; set; } = true;

        /// <summary>
        /// Dequeue a message 
        /// </summary>
        /// <param name="queueConfiguration">(sic)</param>
        /// <param name="handler">QueueMessageHandler</param>
        public void SetupDequeueEvent(Models.RabbitMqQueueConfiguration queueConfiguration, QueueMessageHandler handler)
        {
            var factory = Utilities.RabbitMqHelper.RabbitMQMakeConnectionFactory(this._engineConfiguration.Host, this._engineConfiguration.Port, this._engineConfiguration.Username, this._engineConfiguration.Password);

            using (var connection = factory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    Utilities.RabbitMqHelper.SetupDurableQueue(model, this._engineConfiguration, queueConfiguration);

                    var consumer = new EventingBasicConsumer(model);

                    consumer.Received += (_, ea) =>
                    {
                        handler(this, this._logger, model, ea);
                    };

                    model.BasicConsume(queue: queueConfiguration.QueueName,
                                         autoAck: false,
                                         consumer: consumer);

                    while (this.KeepListening) { }
                }
            }
        }

        /// <summary>
        /// Enqueue message
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="message">Message of T</param>
        /// <param name="queueConfiguration">RabbitMqQueueConfiguration</param>
        public void Enqueue<T>(T message, Models.RabbitMqQueueConfiguration queueConfiguration)
        {
            var factory = Utilities.RabbitMqHelper.RabbitMQMakeConnectionFactory(this._engineConfiguration.Host, this._engineConfiguration.Port, this._engineConfiguration.Username, this._engineConfiguration.Password);

            using (var connection = factory.CreateConnection())
            {
                using (IModel model = connection.CreateModel())
                {
                    Utilities.RabbitMqHelper.SetupDurableQueue(model, this._engineConfiguration, queueConfiguration);

                    var messageProperties = Utilities.RabbitMqHelper.MessageBasicPropertiesPersistant(model, this._engineConfiguration.MessageDeliveryMode, this._engineConfiguration.MessagePersistent, this._engineConfiguration.MessageExpiration);

                    // Make message
                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);

                    // Send message
                    Utilities.RabbitMqHelper.Publish(model, queueConfiguration.ExchangeName, queueConfiguration.RoutingKey, messageProperties, body);

                    this._logger.LogInformation($"Published: {json}");
                }
            }
        }

        /// <summary>
        /// Ack/Nack/Reject Message (must be called by the <c>QueueMessageHandler</c>
        /// </summary>
        /// <param name="model">IModel</param>
        /// <param name="ea">BasicDeliverEventArgs</param>
        /// <param name="state">ReceivedMessageState</param>
        public void SendResponse(IModel model, BasicDeliverEventArgs ea, ReceivedMessageState state)
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(ea);

            switch (state)
            {
                case ReceivedMessageState.SuccessfullyProcessed:
                    // Success remove from queue
                    model.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    break;
                case ReceivedMessageState.UnsuccessfulProcessing:
                    // Unsuccessful, requeue and retry
                    model.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                    break;
                default:
                    // Bad Message, Reject and Delete
                    model.BasicReject(deliveryTag: ea.DeliveryTag, requeue: false);
                    break;
            }
        }

        /// <summary>
        /// Delete all message in a queue (Purge)
        /// </summary>
        /// <param name="queueConfiguration">QueueInstanceConfiguration</param>
        public void PurgeQueue(Models.RabbitMqQueueConfiguration queueConfiguration)
        {
            ArgumentNullException.ThrowIfNull(queueConfiguration);

            var factory = Utilities.RabbitMqHelper.RabbitMQMakeConnectionFactory(this._engineConfiguration.Host, this._engineConfiguration.Port, this._engineConfiguration.Username, this._engineConfiguration.Password);

            using (var connection = factory.CreateConnection())
            {
                using (IModel model = connection.CreateModel())
                {
                    try
                    {
                        model.QueuePurge(queueConfiguration.QueueName);
                    }
                    catch { }
                }
            }
        }

    }

}
