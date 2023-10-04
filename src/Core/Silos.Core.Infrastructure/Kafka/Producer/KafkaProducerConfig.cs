namespace Silos.Core.Infrastructure.Kafka.Producer;

public class KafkaProducerConfig
{
    public string Topic { get; set; }
    public string ConnectionString { get; set; }
}