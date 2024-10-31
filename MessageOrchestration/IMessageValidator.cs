using FluentValidation;
using FluentValidation.Results;

namespace MessageOrchestration;

public interface IMessageValidator<
    TMessage,
    TMessageMetadata,
    TDataFactoryResult,
    TValidationFailedEvent>
    : IValidator<MessageValidationParameters<
        TMessage,
        TMessageMetadata,
        TDataFactoryResult>>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
    where TValidationFailedEvent : Message
{
    TValidationFailedEvent CreateValidationFailedEvent(
        MessageValidationParameters<TMessage, TMessageMetadata, TDataFactoryResult> validationParameters,
        ValidationResult validationResult);
}