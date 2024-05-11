using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StuartWilliams.ScatterGatherMunge.Lib.Delegates;
using StuartWilliams.ScatterGatherMunge.Lib.Enums;

namespace StuartWilliams.ScatterGatherMunge.Lib.Interfaces
{
    /// <summary>
    /// IQueue Engine
    /// </summary>
    public interface IQueueEngine
    {
        /// <summary>
        /// Enqueue a message of type T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="message">message</param>
        /// <param name="queueConfiguration">(sic)</param>
        void Enqueue<T>(T message, Models.RabbitMqQueueConfiguration queueConfiguration);

        /// <summary>
        /// Get a message of type <c>IModel</c>
        /// </summary>
        /// <param name="queueConfiguration">(sic)</param>
        /// <param name="handler">Handler to pass called back for each message</param>
        /// <returns>IModel</returns>
        void SetupDequeueEvent(Models.RabbitMqQueueConfiguration queueConfiguration, QueueMessageHandler handler);

        /// <summary>
        /// Ack/Nack/Reject Message (must be called by the <c>QueueMessageHandler</c>
        /// </summary>
        /// <param name="model">IModel</param>
        /// <param name="ea">BasicDeliverEventArgs</param>
        /// <param name="state">ReceivedMessageState</param>
        void SendResponse(IModel model, BasicDeliverEventArgs ea, ReceivedMessageState state);

        /// <summary>
        /// Delete all message in a queue (Purge)
        /// </summary>
        /// <param name="queueConfiguration">RabbitMqQueueConfiguration</param>
        void PurgeQueue(Models.RabbitMqQueueConfiguration queueConfiguration);

    }

}
