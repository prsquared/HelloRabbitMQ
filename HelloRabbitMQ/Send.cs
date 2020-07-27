using System;
using RabbitMQ.Client;
using System.Text;

namespace HelloRabbitMQ
{
    
    class Send
    {
        public static void Main()
        {
            //Uri uri = new Uri("amqp://guest:guest@localhost:5672/");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent => {0}", message);
                }
            }
        }
    }
}
