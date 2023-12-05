<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//WebApplication.CreateBuilder initializes a new instance of the
	//WebApplicationBuilder class with preconfigured defaults.
	//The developer exception page is enabled in the preconfigured defaults.
	//When the following code is run in the development environment,
	//navigating to / renders a friendly page that shows the exception.
	var builder = WebApplication.CreateBuilder();

	var app = builder.Build();
	
	app.UseDeveloperExceptionPage();
	
	"Test on browser".Dump("result");

	app.MapGet("/", () =>
	{
		throw new InvalidOperationException("Oops, the '/' route has thrown an exception.".Dump("Developer Exception"));
	});
	
	curl.GET();

	app.Run();
}