using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly IRepository<SecureMessage> _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessagingService(IRepository<SecureMessage> messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<MessageSummaryDto>> GetInboxAsync(string userId)
        {
            var messages = await _messageRepository.SearchAsync(message => message.RecipientId == userId);
            return messages
                .OrderByDescending(message => message.CreatedOn)
                .Select(Map)
                .ToList();
        }

        public async Task<MessageSummaryDto> SendMessageAsync(MessageComposeRequest request)
        {
            var message = new SecureMessage(request.Subject, request.Body, request.SenderId, request.RecipientId, request.IsSupportMessage);
            await _messageRepository.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return Map(message);
        }

        public async Task MarkAsReadAsync(Guid messageId)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);
            if (message == null)
            {
                throw new InvalidOperationException("Message not found");
            }

            message.MarkAsRead();
            await _messageRepository.UpdateAsync(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ArchiveMessageAsync(Guid messageId)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);
            if (message == null)
            {
                throw new InvalidOperationException("Message not found");
            }

            message.Archive();
            await _messageRepository.UpdateAsync(message);
            await _unitOfWork.SaveChangesAsync();
        }

        private static MessageSummaryDto Map(SecureMessage message)
        {
            var previewLength = Math.Min(120, message.Body?.Length ?? 0);
            return new MessageSummaryDto
            {
                Id = message.Id,
                Subject = message.Subject,
                Preview = message.Body?.Substring(0, previewLength),
                SenderId = message.SenderId,
                RecipientId = message.RecipientId,
                Status = message.Status,
                SentOn = message.CreatedOn,
                ReadOn = message.ReadOn
            };
        }
    }
}
