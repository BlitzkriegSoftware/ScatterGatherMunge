<#
	Run RabbitMq in the background in Docker
	See: https://hub.docker.com/_/rabbitmq/
#>
$name="some-rabbit"
$image="rabbitmq:3-management-alpine"
$port=5672
docker stop $name 2>1 | out-null
docker rm $name 2>1 | out-null
#docker pull rabbitmq:3-alpine
Write-Host "${name}, Image: ${image}, Port: ${port}"
docker run -d --hostname ${name} --name ${name} -p 8080:15672 -p ${port}:${port} ${image}