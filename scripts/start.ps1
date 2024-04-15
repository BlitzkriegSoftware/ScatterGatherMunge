<#
    Start All
#>
. .\valkey_start.ps1
Start-Sleep -Seconds 1
. .\rabbitmq_start.ps1
Start-Sleep -Seconds 1
docker ps