using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IMessagingService
    {
        Task<IReadOnlyList<MessageSummaryDto>> GetInboxAsync(string userId);
        Task<MessageSummaryDto> SendMessageAsync(MessageComposeRequest request);
        Task MarkAsReadAsync(Guid messageId);
        Task ArchiveMessageAsync(Guid messageId);
    }
}
