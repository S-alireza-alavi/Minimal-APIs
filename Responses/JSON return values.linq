<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var app = WebApplication.Create();

	app.MapGet("/hello", () => new { Message = "Hello World".Dump("JSON type return value") });
	
	Curl.GET();
	
	app.Run();
}