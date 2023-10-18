<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//For more information on the development certificate, <see https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0#trust/>
	var app = WebApplication.Create();

	app.Urls.Add("https://localhost:5000");

	app.MapGet("/", () => "Hello World".Dump("Ran on HTTPS"));
	
	MyExtensions.ProcessStart();

	app.Run();
}