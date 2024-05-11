using Newtonsoft.Json;
using System;

namespace StuartWilliams.ScatterGatherMunge.Lib.Models
{
    /// <summary>
    /// Configuration: Specific Queue
    /// </summary>
    public class RabbitMqQueueConfiguration
    {
        /// <summary>
        /// Quick Name: Exchange
        /// </summary>
        public const string ExchangeName_Default = "myExchange";

        /// <summary>
        /// Quick Name: Queue
        /// </summary>
        public const string QueueName_Default = "myQueue";

        /// <summary>
        /// Quick Name: Routing Key
        /// </summary>
        public const string RoutingKey_Default = "myRoutingKey";

        /// <summary>
        /// (optional) Exchange
        /// </summary>
        public string ExchangeName { get; set; } = RabbitMqQueueConfiguration.ExchangeName_Default;

        /// <summary>
        /// (required) Queue Name
        /// </summary>
        public string QueueName { get; set; } = RabbitMqQueueConfiguration.QueueName_Default;

        /// <summary>
        /// (optional) Route
        /// </summary>
        public string RoutingKey { get; set; } = RabbitMqQueueConfiguration.RoutingKey_Default;

        /// <summary>
        /// Is this valid?
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.QueueName);
            }
        }

        /// <summary>
        /// Create Default Configuration
        /// </summary>
        /// <returns>RabbitMqQueueConfiguration</returns>
        public static RabbitMqQueueConfiguration CreateDefault() {
            return new RabbitMqQueueConfiguration()
            {
                ExchangeName = ExchangeName_Default,
                QueueName = QueueName_Default,
                RoutingKey = RoutingKey_Default
            };
        }

        /// <summary>
        /// Set Property
        /// </summary>
        /// <param name="key">(sic)</param>
        /// <param name="value">(sic)</param>
        public void SetProperty(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            switch (key.ToLowerInvariant())
            {
                case "rabbitmq-exchangename": this.ExchangeName = value; break;
                case "rabbitmq-queuename": this.QueueName = value; break;
                case "rabbitmq-routingkey": this.RoutingKey = value; break;
            }
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns>Debug String</returns>
        public override string ToString()
        {
            return $"Exchange: {this.ExchangeName}, Queue: {this.QueueName}, Route: {this.RoutingKey}";
        }

        /// <summary>
        /// To JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }

}
