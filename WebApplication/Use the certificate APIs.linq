<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Hosting</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();

	builder.WebHost.ConfigureKestrel(options =>
	{
		options.ConfigureHttpsDefaults(httpsOptions =>
		{
			var certPath = Path.Combine(builder.Environment.ContentRootPath, "cert.pem");
			var keyPath = Path.Combine(builder.Environment.ContentRootPath, "key.pem");

			httpsOptions.ServerCertificate = X509Certificate2.CreateFromPemFile(certPath,
										 keyPath);
		});
	});

	var app = builder.Build();

	app.Urls.Add("https://localhost:3000");

	app.MapGet("/",() => "Hello World");
	
	app.Run();
}