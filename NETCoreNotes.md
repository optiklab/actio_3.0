

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