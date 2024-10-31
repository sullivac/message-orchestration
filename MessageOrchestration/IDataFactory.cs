namespace MessageOrchestration;

public interface IDataFactory<TMessage, TMessageMetadata, TUnverified>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
{
    Task<TUnverified> QueryAsync(MessageContainer<TMessage, TMessageMetadata> messageContainer);
}