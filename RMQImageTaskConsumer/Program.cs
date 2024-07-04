using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

internal class Consumer
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "image_tasks",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var imageTask = JsonSerializer.Deserialize<RMQImageTasks.ImageTask>(message);
                Debug.Assert(imageTask != null);

                Console.WriteLine(" [x] Received {0}", message);

                Task.Run(() => ProcessImageTask(imageTask));

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: "image_tasks",
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    private static void ProcessImageTask(RMQImageTasks.ImageTask imageTask)
    {
        // Simulate image processing
        // Console.WriteLine($"Processing {imageTask.TaskType} task for {imageTask.ImagePath}");
        Console.WriteLine($"{imageTask}");
        // Add actual image processing logic here (e.g., resizing, adding watermark)
        var wait = RMQImageTasks.Ext.random.Next(500, 4000);
        Thread.Sleep(wait);
        Console.WriteLine($"{imageTask.Id} :: {wait} ms");
    }
}