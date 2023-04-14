using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

#nullable disable

//ADD THIS FOR PULL CONNECTİON STRING FROM APPSETTINGs.JSON
var configBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = configBuilder.Build();

//CONFIGURATION FOR RABBITMQ
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri(uriString: configuration.GetConnectionString("DefaultConnection"));
factory.ClientProvidedName = "Rabbit Producer App"; //application name that I provided 

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

byte[] messageBodyBytes = Encoding.UTF8.GetBytes(s: "Hello Erdal");
channel.BasicPublish(exchangeName, routingKey, basicProperties: null, messageBodyBytes);

channel.Close();
conn.Close();