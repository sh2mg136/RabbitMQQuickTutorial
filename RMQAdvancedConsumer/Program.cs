using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("RabbitMQ AdvancedConsumer");

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

    var queueName = channel.QueueDeclare().QueueName;
    var severity = (args.Length > 0) ? args[0] : "info";
    channel.QueueBind(queue: queueName,
                      exchange: "direct_logs",
                      routingKey: severity);

    Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;
        Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);

        // Simulate work
        int dots = message.Split('.').Length - 1;
        System.Threading.Thread.Sleep(dots * 1000);

        Console.WriteLine(" [x] Done");

        // Acknowledge the message
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    };

    // Set prefetch count to 1
    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

    channel.BasicConsume(queue: queueName,
                         autoAck: false,
                         consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}