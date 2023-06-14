# Kiosk
### Настройки рабочего окружения:
### Необходимое дополнительное ПО: PgAdmin 4, Docker Desktop.
#### Используемая СУБД для микросервисов: PostgreSQL.
Для корректной настройки необходимо в PgAdmin создать сервер со следующими настройками (это поможет в работе с таблицами, периодически возникает нужда писать скрипты):
* Host: localhost 
* Port: 9595
* Username: postgres
#### Для работы сервера используется Docker-контейнер. Для запуска нужно включить Docker Desktop, в консоли нужно прописать:
* docker run --restart always --name some-postgres -p 9595:5432 -e POSTGRES_PASSWORD=mysecretpassword -d postgres
#### Также нам необходимо подключить RabbitMQ, для этого в консоли нужно прописать:
* docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management
#### Настройка запуска проектов выглядит следующим образом:
![ЗапускПроектов](https://github.com/I1ss/Kiosk/blob/Dev/StartupProjects.png)