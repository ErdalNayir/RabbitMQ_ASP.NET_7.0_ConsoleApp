# Use official docker image
FROM rabbitmq:3-management

# Change workind directory to  /etc/rabbitmq 
WORKDIR /etc/rabbitmq

#Assign new username and user password inside Docker environment variables
ENV RABBITMQ_DEFAULT_USER=erdal \
    RABBITMQ_DEFAULT_PASS=*2001*2001*

# if it is needed, You can declare new environment variables for RabbitMQ
# ENV RABBITMQ_DEFAULT_VHOST=<YENI_VHOST> \
#     RABBITMQ_DEFAULT_PORT=<YENI_PORT> \
#     ...

# Docker konteynerini çalıştırdığında kullanılacak komutu belirtin Define the command when docker container run
CMD ["rabbitmq-server"]
