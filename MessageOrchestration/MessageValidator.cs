using System.Data;
using FluentValidation;
using FluentValidation.Results;

namespace MessageOrchestration;

public abstract class MessageValidator<TMessage, TMessageMetadata, TUnverified, TValidationFailedEvent>
    : AbstractValidator<MessageValidationParameters<TMessage, TMessageMetadata, TUnverified>>,
        IMessageValidator<TMessage, TMessageMetadata, TUnverified, TValidationFailedEvent>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
    where TValidationFailedEvent : Message
{
    public abstract TValidationFailedEvent CreateValidationFailedEvent(
        MessageValidationParameters<TMessage, TMessageMetadata, TUnverified> validationParameters,
        ValidationResult validationResult);
    public TValidationFailedEvent CreateAuthorizationFailedEvent(
        MessageValidationParameters<TMessage, TMessageMetadata, TUnverified> validationParameters,
        ValidationResult validationResult)
    {
        return CreateAuthorizationFailedEventInternal(
            validationParameters,
            validationResult,
            string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage)));
    }

    protected abstract TValidationFailedEvent CreateAuthorizationFailedEventInternal(
        MessageValidationParameters<TMessage, TMessageMetadata, TUnverified> validationParameters,
        ValidationResult validationResult,
        string message);
}

// public MySpecificMessagevalidator<MyMessage, CommandMetatdata, MyUnverified, MyValidationFailedEvent>
//     : MessageValidator<MyMessage, CommandMetatdata, MyUnverified, MyValidationFailedEvent>
// {
//     public MySpecificMessagevalidator()
// {
//     RuleFor(x => x.DataFactoryResult.Account).NotNull();
// }

// public override MyValidationFailedEvent CreateValidationFailedEvent(
//     MessageValidationParameters<MyMessage, CommandMetatdata, MyUnverified> validationParameters,
//     ValidationResult validationResult)
// {
//     return new MyValidationFailedEvent(validationResult.Errors);
// }
// }