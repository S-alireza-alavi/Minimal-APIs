<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">D:\Repositories\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//WebApplication.CreateBuilder initializes a new instance of the WebApplicationBuilder class with preconfigured defaults.
	//The developer exception page is enabled in the preconfigured defaults. When the following code is run in the development environment,
	//navigating to / renders a friendly page that shows the exception.
	var builder = WebApplication.CreateBuilder();

	var app = builder.Build();
	
	app.UseDeveloperExceptionPage();

	app.MapGet("/", () =>
	{
		throw new InvalidOperationException("Oops, the '/' route has thrown an exception.".Dump("Developer Exception"));
	});
	
	MyExtensions.SendRequestToServer();

	app.Run();
}