// RPC Server (Consumer)

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json;

Random rnd = new Random();

static async Task<string> ProcessMessage(IRmqTaskInfo.IRmqTaskInfo message)
{
    // Implement your message processing logic here
    // For demonstration, we'll just return the message in uppercase
    var wait = (new Random()).Next(100, 2500);
    Thread.Sleep(wait);
    await Task.Delay(10);
    return $"[{message.Id}] {message.Message.ToUpper()} ({wait} ms)";
}

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "rpc_queue",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    channel.BasicQos(0, 1, false);

    var consumer = new EventingBasicConsumer(channel);
    channel.BasicConsume(queue: "rpc_queue",
                         autoAck: false,
                         consumer: consumer);

    Console.WriteLine(" [x] Awaiting RPC requests");

    consumer.Received += async (model, ea) =>
    {
        string response = null;

        var body = ea.Body.ToArray();
        var props = ea.BasicProperties;
        var replyProps = channel.CreateBasicProperties();
        replyProps.CorrelationId = props.CorrelationId;

        try
        {
            var message = Encoding.UTF8.GetString(body);
            var taskInfo = JsonSerializer.Deserialize<IRmqTaskInfo.TaskInfo>(message);

            Contract.Assert(taskInfo != null);
            Console.WriteLine($" [.] Received request: [{taskInfo.Id}] {taskInfo.Message}");
            response = await ProcessMessage(taskInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(" [.] " + e.Message);
            response = "";
        }
        finally
        {
            var responseBytes = Encoding.UTF8.GetBytes(response ?? "NO DATA");

            channel.BasicPublish(exchange: "",
                                 routingKey: props.ReplyTo,
                                 basicProperties: replyProps,
                                 body: responseBytes);

            channel.BasicAck(deliveryTag: ea.DeliveryTag,
                             multiple: false);
        }
    };

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}