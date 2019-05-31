using MS.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.Common.Interfaces
{
    /// <summary>
    /// IMessageQueueProcessing
    /// </summary>
    public interface IMessageQueueProcessing
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="messageQueue"></param>
        /// <param name="connectionStrings"></param>
        /// <returns></returns>
        Task<ResponseModel<MessageQueue>> CommitInboundMessage(MessageQueue messageQueue, ConnectionStrings connectionStrings);

        /// <summary>
        ///
        /// </summary>
        /// <param name="messageQueueConfigurations"></param>
        /// <param name="outboundSemaphoreKey"></param>
        /// <param name="connectionStrings"></param>
        /// <returns></returns>
        Task<ResponseModel<List<MessageQueue>>> SendQueueMessages(List<IMessageQueueConfiguration> messageQueueConfigurations, string outboundSemaphoreKey, ConnectionStrings connectionStrings);

        /// <summary>
        ///
        /// </summary>
        /// <param name="inboundSemaphoreKey"></param>
        /// <param name="connectionStrings"></param>
        /// <returns></returns>
        Task<ResponseModel<List<MessageQueue>>> ProcessMessages(string inboundSemaphoreKey, ConnectionStrings connectionStrings);
    }
}