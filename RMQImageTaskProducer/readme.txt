Let's consider a more practical real-life example: 
a task queue system for processing image files. In this scenario, 
we have a web application where users can upload images, 
and we need to process these images (e.g., resizing, adding watermarks, etc.) 
in the background.

Scenario
1.	Web Application:	Users upload images via a web interface.
2.	Producer:			The web application sends a message to a RabbitMQ queue 
						with details about the uploaded image.
3.	Consumer:			A background worker (consumer) processes the image 
						based on the message received from the queue.


==============================================================================


using System;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;

public class ImageTask
{
    public string ImagePath { get; set; }
    public string TaskType { get; set; } // e.g., "resize", "watermark"
}

class Producer
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

            var imageTask = new ImageTask
            {
                ImagePath = "/path/to/image.jpg",
                TaskType = "resize"
            };

            var message = JsonConvert.SerializeObject(imageTask);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                 routingKey: "image_tasks",
                                 basicProperties: properties,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}


==============================================================================


using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

public class ImageTask
{
    public string ImagePath { get; set; }
    public string TaskType { get; set; } // e.g., "resize", "watermark"
}

class Consumer
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
                var imageTask = JsonConvert.DeserializeObject<ImageTask>(message);

                Console.WriteLine(" [x] Received {0}", message);
                ProcessImageTask(imageTask);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: "image_tasks",
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    private static void ProcessImageTask(ImageTask imageTask)
    {
        // Simulate image processing
        Console.WriteLine($"Processing {imageTask.TaskType} task for {imageTask.ImagePath}");
        // Add actual image processing logic here (e.g., resizing, adding watermark)
    }
}


==============================================================================


Explanation

Producer:

Connects to RabbitMQ server running on localhost.
Declares a durable queue named "image_tasks".
Creates an ImageTask object with details about the image and the task type.
Serializes the ImageTask object to JSON and sends it to the queue with persistent delivery mode.

Consumer:

Connects to RabbitMQ server running on localhost.
Declares the same durable queue "image_tasks".
Sets up a consumer to listen for messages on the "image_tasks" queue.
Deserializes the received message to an ImageTask object.
Processes the image task (simulated by a console output).
Acknowledges the message after processing.

Running the Example
Start the RabbitMQ server.
Run the Consumer application to start processing image tasks.
Run the Producer application to send an image task to the queue.