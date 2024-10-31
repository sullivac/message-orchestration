namespace MessageOrchestration;

public record MessageValidationParameters<TMessage, TMessageMetadata, TDataFactoryResult>(
    MessageContainer<TMessage, TMessageMetadata> MessageContainer,
    TDataFactoryResult DataFactoryResult)
    where TMessage : Message
    where TMessageMetadata : MessageMetadata;