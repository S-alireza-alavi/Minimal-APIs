<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//Any existing ASP.NET Core middleware can be configured on the WebApplication:
	var app = WebApplication.Create();

	// Setup the file server to serve static files.
	app.UseFileServer();

	app.MapGet("/", () => "Hello World!".Dump("UseFileServer middleware"));
	
	MyExtensions.ProcessStart();

	app.Run();
}