#UI
docker build -t gsflc-ui --build-arg ENVVAR=development --no-cache .

#API
docker build -f DockerfileApi -t gsflc-report --no-cache .

#REPORT
docker build -f DockerfileReport -t gsflc-report --no-cache .
