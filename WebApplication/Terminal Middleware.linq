<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The middleware must be added after UseEndpoints.
	//The app needs to call UseRouting and UseEndpoints so that the terminal middleware can be placed at the correct location.
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.UseRouting();
	
	app.MapGet("/", () => "Hello World!".Dump("result"));
	
	app.UseEndpoints(e => { });
	
	curl.GET();

	curl.GET(url: "http://localhost:5000/A");

	app.Run(context =>
	{
		context.Response.StatusCode = 404;
		"Not found".Dump("Result");
		return Task.CompletedTask;
	});
	
	app.Run();
}