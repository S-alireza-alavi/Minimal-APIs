<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">D:\Repositories\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Hosting</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//Existing extension methods on IHostBuilder can be accessed using the Host property:
	var builder = WebApplication.CreateBuilder();

	// Wait 30 seconds for graceful shutdown.
	builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30).Dump("Graceful shutdown will take:"));

	var app = builder.Build();

	app.MapGet("/", () => "Hello World!".Dump("result"));
	
	MyExtensions.SendRequestToServer();

	app.Run();
}