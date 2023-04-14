# Resmi RabbitMQ Docker imajını kullanın
FROM rabbitmq:3-management

# Çalışma dizinini /etc/rabbitmq yapın
WORKDIR /etc/rabbitmq

# Yeni kullanıcı adını ve şifresini Docker ortam değişkenlerine atayabilirsiniz
ENV RABBITMQ_DEFAULT_USER=erdal \
    RABBITMQ_DEFAULT_PASS=*2001*2001*

# Eğer gerekli ise diğer RabbitMQ ortam değişkenlerini de tanımlayabilirsiniz
# ENV RABBITMQ_DEFAULT_VHOST=<YENI_VHOST> \
#     RABBITMQ_DEFAULT_PORT=<YENI_PORT> \
#     ...

# Docker konteynerini çalıştırdığında kullanılacak komutu belirtin
CMD ["rabbitmq-server"]