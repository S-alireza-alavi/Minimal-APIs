<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following code writes a message to the log on application startup:
	var app = WebApplication.Create();

	app.Logger.LogInformation("The app started".Dump("Log"));

	app.MapGet("/", () => "Hello World".Dump("result"));
	
	Curl.GET();

	app.Run();
}