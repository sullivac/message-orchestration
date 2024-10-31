namespace MessageOrchestration;

public class ConventionalCommandHandler<
    TMessage,
    TUnverified,
    TVerified,
    TValidationFailedEvent>(
    IDataFactory<TMessage, CommandMetadata, TUnverified> _dataFactory,
    IMessageValidator<TMessage, CommandMetadata, TUnverified, TValidationFailedEvent> _validator,
    IEventPublisher _eventPublisher)
    where TMessage : Message
    where TValidationFailedEvent : Message
{
    public async Task HandleAsync(
        MessageContainer<TMessage, CommandMetadata> messageContainer)
    {
        TUnverified unverified = await _dataFactory.QueryAsync(messageContainer);

        var validationParameters =
            new MessageValidationParameters<TMessage, CommandMetadata, TUnverified>(
                messageContainer,
                unverified);

        var validationResult = _validator.Validate(validationParameters);

        if (validationResult.IsValid)
        {
            // Do stuff
        }
        else
        {
            await _eventPublisher.PublishAsync(
                messageContainer,
                _validator.CreateValidationFailedEvent(
                    validationParameters,
                    validationResult));
        }
    }
}