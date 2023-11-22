<Query Kind="Program">
<Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//By default the WebApplicationBuilder reads configuration from multiple sources, including:

	//	appSettings.json and appSettings.{environment}.json
	//	Environment variables
	//	The command line
	//	The following code reads HelloKey from configuration and displays the value at the '/' endpoint. If the configuration value is null, "Hello" is assigned to message:
	//	Environment.CurrentDirectory = Path.GetDirectoryName(Util.CurrentQueryPath);
	
	var builder = WebApplication.CreateBuilder();
	
	var appsettingsPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "appsettingsReadConfigurationQuery.json");
	
	builder.Configuration.AddJsonFile(appsettingsPath.Dump("Path"));

	var message = builder.Configuration["HelloKey"] ?? "Hello";

	var app = builder.Build();

	app.MapGet("/", () => message.Dump("Message"));

	MyExtensions.ProcessStart();
	
	app.Run();
}