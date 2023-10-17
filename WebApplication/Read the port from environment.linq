<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//The following code reads the port from the environment:
	var app = WebApplication.Create();

	var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

	app.MapGet("/", () => "Hello World".Dump("result"));
	
	MyExtensions.ProcessStart();

	app.Run($"http://localhost:{port}".Dump("Port:"));
}