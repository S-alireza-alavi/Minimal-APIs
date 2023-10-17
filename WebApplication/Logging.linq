<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//The following code writes a message to the log on application startup:
	var app = WebApplication.Create();

	app.Logger.LogInformation("The app started".Dump("Log"));

	app.MapGet("/", () => "Hello World".Dump("result"));
	
	MyExtensions.ProcessStart();

	app.Run();
}