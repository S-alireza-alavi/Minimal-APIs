<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//If you want to test this query on a different port uncomment the following line and change the second parameter value as required
	//Environment.SetEnvironmentVariable("PORT", "4000");
	
	//The following code reads the port from the environment:
	var app = WebApplication.Create();

	var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

	app.MapGet("/", () => "Hello World".Dump("result"));
	
	curl.GET(port: int.Parse(port));

	app.Run($"http://localhost:{port}".Dump("Port:"));
}