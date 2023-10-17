<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//The following code shows how to get services from the DI container during application startup:
	var builder = WebApplication.CreateBuilder();

	builder.Services.AddControllers();
	builder.Services.AddScoped<SampleService>();

	var app = builder.Build();

	app.MapControllers();

	using (var scope = app.Services.CreateScope())
	{
		var sampleService = scope.ServiceProvider.GetRequiredService<SampleService>();
		sampleService.DoSomething();
	}
	
	MyExtensions.ProcessStart();

	app.Run();
}

public class SampleService
{
	public string DoSomething()
	{
		return "Done Something!".Dump("DoSomething method executed...");
	}
}