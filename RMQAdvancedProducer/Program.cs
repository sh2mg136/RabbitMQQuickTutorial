using RabbitMQ.Client;
using System.Net.NetworkInformation;
using System.Text;

Console.WriteLine("Let's dive into a more advanced example using RabbitMQ in C#.");

// This example will cover the following advanced features:
// Exchanges and Bindings: Using different types of exchanges (direct, topic, fanout).
// Message Acknowledgments: Ensuring messages are acknowledged properly.
// Durable Queues and Messages: Making sure messages are not lost even if RabbitMQ crashes.
// Prefetch Count: Limiting the number of unacknowledged messages sent to consumers.

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

    var severity = (args.Length > 0) ? args[0] : "info";
    var message = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello World!";
    var body = Encoding.UTF8.GetBytes(message);

    var properties = channel.CreateBasicProperties();
    properties.Persistent = true; // Make message persistent

    channel.BasicPublish(exchange: "direct_logs",
                         routingKey: severity,
                         basicProperties: properties,
                         body: body);
    Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();