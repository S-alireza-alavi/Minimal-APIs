<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//By default, the web root is relative to the content root in the wwwroot folder. Web root is where the static files middleware looks for static files. Web root can be changed with WebHostOptions, the command line, or with the UseWebRoot method:
	var builder = WebApplication.CreateBuilder(new WebApplicationOptions
	{
		// Look for static files in webroot
		WebRootPath = "webroot".Dump("changed the webroot path")
	});

	var app = builder.Build();
	
	MyExtensions.ProcessStart();

	app.Run();
}