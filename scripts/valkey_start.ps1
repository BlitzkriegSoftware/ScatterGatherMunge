<#
	Start valkey
#>
$valkeyPORT=6379
$valkeyNAME="d-valkey"
$valkeyIMAGE="codingpaws/valkey"
docker stop "${valkeyNAME}" 2>&1 | out-null
docker rm "${valkeyNAME}" 2>&1 | out-null
Write-Output "${valkeyNAME}, Image: ${valkeyIMAGE}, Port: ${valkeyPORT}"
docker run --name ${valkeyNAME} -d -p ${valkeyPORT}:${valkeyPORT} ${valkeyIMAGE}