Dotnet commands used to create project
--------------------------------------
dotnet new sln
mkdir src
mkdir tests
cd src
dotnet new webapi -n Actio.Api
dotnet new webapi -n Actio.Services.Identity
dotnet new webapi -n Actio.Services.Activities
dotnet new classlib -n Actio.Common
dotnet add Actio.Api/Actio.Api.csproj reference Actio.Common/Actio.Common.csproj
dotnet add Actio.Services.Identity/Actio.Services.Identity.csproj reference Actio.Common/Actio.Common.csproj
dotnet add Actio.Services.Activities/Actio.Services.Activities.csproj reference Actio.Common/Actio.Common.csproj
cd..
dotnet sln add src/Actio.Api/Actio.Api.csproj
dotnet sln add src/Actio.Common/Actio.Common.csproj
dotnet sln add src/Actio.Services.Identity/Actio.Services.Identity.csproj
dotnet sln add src/Actio.Services.Activities/Actio.Services.Activities.csproj
dotnet restore
dotnet build

(https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.0#endpoint-configuration)
You can specify URLs using the:
- ASPNETCORE_URLS environment variable.
- --urls command-line argument.
- urls host configuration key.
- UseUrls extension method:      ...WebHost.CreateDefaultBuilder(args).UseUrls("http://*:5000")....

...

A development certificate is created:

When the .NET Core SDK is installed.
The dev-certs tool is used to create a certificate.
