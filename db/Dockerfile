FROM mysql:latest 
ENV MYSQL_DATABASE mydb
COPY ./scripts/ /docker-entrypoint-initdb.d/
EXPOSE 3306