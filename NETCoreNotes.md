!This document is better to read with Markdown reader.

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


Dotnet commands used for TEST projects
--------------------------------------

$>dotnet sln add tests/Actio.Api.Tests/Actio.Api.Tests.csproj
$>dotnet sln add tests/Actio.Services.Activities.Tests/Actio.Services.Activities.Tests.csproj
$>dotnet sln add tests/Actio.Services.Identity.Tests/Actio.Services.Identity.Tests.csproj

$>dotnet add tests/Actio.Api.Tests/Actio.Api.Tests.csproj reference src/Actio.Common/Actio.Common.csproj
$>dotnet add tests/Actio.Services.Identity.Tests/Actio.Services.Identity.Tests.csproj reference src/Actio.Common/Actio.Common.csproj
$>dotnet add tests/Actio.Services.Identity.Tests/Actio.Services.Identity.Tests.csproj reference src/Actio.Api/Actio.Api.csproj

$>dotnet add tests/Actio.Api.Tests/Actio.Api.Tests.csproj reference src/Actio.Api/Actio.Api.csproj
$>dotnet add tests/Actio.Services.Activities.Tests/Actio.Services.Activities.Tests.csproj reference src/Actio.Services.Activities/Actio.Services.Activities.csproj
$>dotnet add tests/Actio.Services.Identity.Tests/Actio.Services.Identity.Tests.csproj reference src/Actio.Services.Identity/Actio.Services.Identity.csproj

$>dotnet add package Microsoft.AspNetCore.Mvc -v 2.2.0
$>dotnet add package Microsoft.AspNetCore.TestHost -v 2.2.0
$>dotnet add package Microsoft.AspNetCore.Mvc.Teting -v 2.2.0
$>dotnet add package Microsoft.AspNetCore.HttpsPolicy -v 2.2.0
$>dotnet add package Moq
$>dotnet add package FluentAssertions


MORE
----

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
