<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Hosting</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//The following code sets the content root, application name, and environment:
	var builder = WebApplication.CreateBuilder(new WebApplicationOptions
	{
		ApplicationName = typeof(UserQuery).Assembly.FullName,
		ContentRootPath = Directory.GetCurrentDirectory(),
		EnvironmentName = Environments.Staging,
		WebRootPath = "customwwwroot"
	});

	Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}".Dump("App name"));
	Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}".Dump("Environment name"));
	Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}".Dump("ContentRoot path"));
	Console.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}".Dump("WebRoot path"));

	var app = builder.Build();
	
	MyExtensions.ProcessStart();
	
	app.Run();
}