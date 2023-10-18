<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//The middleware must be added after UseEndpoints.
	//The app needs to call UseRouting and UseEndpoints so that the terminal middleware can be placed at the correct location.
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.UseRouting();
	
	//Why this line Dupmed in the other system but not this one?
	app.MapGet("/", () => "Hello World!".Dump("result"));
	
	app.UseEndpoints(e => { });

	MyExtensions.ProcessStart();

	app.Run(context =>
	{
		context.Response.StatusCode = 404;
		return Task.CompletedTask;
	});
	
	app.Run();
}