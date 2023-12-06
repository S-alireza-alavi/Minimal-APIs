<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following code creates a WebApplication (app) without explicitly creating a WebApplicationBuilder:
	var app = WebApplication.Create();

	app.MapGet("/", () => "Hello World!".Dump("result"));
	
	"Created WebApplication directly".Dump("WebApplication.Create()");

	curl.GET();

	app.Run();
}