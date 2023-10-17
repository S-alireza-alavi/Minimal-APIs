<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Hosting</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//Extension methods on IWebHostBuilder can be accessed using the WebApplicationBuilder.WebHost property.
	var builder = WebApplication.CreateBuilder();

	// Change the HTTP server implemenation to be HTTP.sys based
	builder.WebHost.UseHttpSys().Dump("UseHttpSys()");

	var app = builder.Build();

	app.MapGet("/", () => "Hello HTTP.sys".Dump("result"));
	
	MyExtensions.ProcessStart();

	app.Run();
}