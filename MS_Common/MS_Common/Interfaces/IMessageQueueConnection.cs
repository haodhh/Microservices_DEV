using RabbitMQ.Client;

namespace MS_Common.Interfaces
{
    /// <summary>
    /// IMessageQueueConnection
    /// </summary>
    public interface IMessageQueueConnection
    {
        /// <summary>
        /// 
        /// </summary>
        void CreateConnection();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IConnection GetConnection();
    }
}