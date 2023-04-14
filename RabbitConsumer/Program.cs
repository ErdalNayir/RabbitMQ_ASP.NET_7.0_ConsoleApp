using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

#nullable disable

//ADD THIS FOR PULL CONNECTİON STRING FROM APPSETTINGs.JSON
var configBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = configBuilder.Build();

//CONFIGURATION FOR RABBITMQ
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri(uriString: configuration.GetConnectionString("DefaultConnection"));
factory.ClientProvidedName = "Rabbit Consumer App"; //application name that I provided 

//CREATE CONNECTION
IConnection conn = factory.CreateConnection();

//CREATE MODEL
IModel channel = conn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queueName, exchangeName, routingKey, arguments: null);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
    var body = args.Body.ToArray();

    string message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Message received: {message}");

    channel.BasicAck(args.DeliveryTag, false);

};

string consumerTag = channel.BasicConsume(queueName, autoAck: false, consumer);
Console.ReadLine();

channel.BasicCancel(consumerTag);

channel.Close();
conn.Close();