rm -f nohup.out
nohup java -jar scott-pic-server-0.0.1.jar -run cataindex catalog/nations.txt --spring.config.location=app.properties &