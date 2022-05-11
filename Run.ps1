Start-Process -FilePath 'dotnet' -WorkingDirectory 'D:\repos\Auto_Microservices\UI' -ArgumentList 'run --debug'

Start-Process -FilePath 'dotnet' -WorkingDirectory 'D:\repos\Auto_Microservices\MicroServices\Car_GrpcService' -ArgumentList 'run --debug'

Start-Process -FilePath 'dotnet' -WorkingDirectory 'D:\repos\Auto_Microservices\MicroServices\CarStation_GrpcService' -ArgumentList 'run --debug'

Start-Process -FilePath 'dotnet' -WorkingDirectory 'D:\repos\Auto_Microservices\MicroServices\Order_GrpcService' -ArgumentList 'run --debug'

Start-Process -FilePath 'dotnet' -WorkingDirectory 'D:\repos\Auto_Microservices\MicroServices\User_GrpcService' -ArgumentList 'run --debug'