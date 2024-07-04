using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Threading;
using RMQImageTasks;
using System.Text.Json;

namespace RMQImageTaskProducer
{
    /// <summary>
    /// RPC Client (Producer)
    /// </summary>
    internal class RpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public Action<TaskInfo, string>? OnCompleted;

        public RpcClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            var correlationId = Guid.NewGuid().ToString();
            props = channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    respQueue.Add(response);
                }
            };

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);
        }

        public string Call(TaskInfo taskInfo)
        {
            var message = JsonSerializer.Serialize(taskInfo);
            var body = Encoding.UTF8.GetBytes(message);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                routingKey: "rpc_queue",
                                basicProperties: props,
                                body: messageBytes);
            var result = respQueue.Take();
            
            OnCompleted?.Invoke(taskInfo, result);

            return result;
        }

        public void Close()
        {
            connection.Close();
        }
    }

    /*
    internal class Program
    {
        public static void Main()
        {
            var rpcClient = new RpcClient();

            Console.WriteLine(" [x] Requesting to process 'hello'");
            var response = rpcClient.Call("hello");

            Console.WriteLine(" [.] Got '{0}'", response);
            rpcClient.Close();
        }
    }
    */
}