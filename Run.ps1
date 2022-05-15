$path = Get-Location

$pathUI = "$path" + "\UI"
Start-Process -FilePath 'dotnet' -WorkingDirectory "$pathUI" -ArgumentList 'run --debug'

$pathCar = "$path" + "\MicroServices\Car_GrpcService"
Start-Process -FilePath 'dotnet' -WorkingDirectory "$pathCar" -ArgumentList 'run --debug'

$pathCarStation = "$path" + "\MicroServices\CarStation_GrpcService"
Start-Process -FilePath 'dotnet' -WorkingDirectory "$pathCarStation" -ArgumentList 'run --debug'

$pathOrder = "$path" + "\MicroServices\Order_GrpcService"
Start-Process -FilePath 'dotnet' -WorkingDirectory "$pathOrder" -ArgumentList 'run --debug'

$pathUser = "$path" + "\MicroServices\User_GrpcService"
Start-Process -FilePath 'dotnet' -WorkingDirectory "$pathUser" -ArgumentList 'run --debug'



