namespace API;

public class QueueConfiguration
{
    public string StorageConnectionString { get; set; }
    public string QueueName { get; set; }
    public Message MessageSample { get; set; }
}

public class Message
{
    public string ProviderId { get; set; }
    public string JobId { get; set; }
}