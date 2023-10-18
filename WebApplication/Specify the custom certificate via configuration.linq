<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var builder = WebApplication.CreateBuilder();

	// Specify the path to your JSON configuration file
	var configFilePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "appsettings.json");
	builder.Configuration.AddJsonFile(configFilePath);

	//builder.Configuration["Kestrel:Certificates:Default:Path"] = "cert.pem".Dump();
	//builder.Configuration["Kestrel:Certificates:Default:KeyPath"] = "key.pem".Dump();

	var app = builder.Build();

	app.Urls.Add("http://localhost:3000");

	app.MapGet("/",() => "Hello World");

	MyExtensions.ProcessStart();

	app.Run();
}