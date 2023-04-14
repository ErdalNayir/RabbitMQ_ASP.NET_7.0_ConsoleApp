
# RabbitMq Project with Console App

* I have created console app with .net core 7.0 in order to understand how rabbit mq and generally queuing works.
* I will use rabbit mq with web api soon 

## How to create RabbitMq Server ?
* I have used docker for creating rabbit mq server. Her is how you can create your on container with dockerfile

```console

# Create docker image
docker build -t rabbitmq-custom .

# Run docker container
docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq-custom

```
