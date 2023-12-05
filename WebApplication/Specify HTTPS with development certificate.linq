<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//Run this code to create certificates that are needed in https mode
	Process.Start(new ProcessStartInfo("dotnet", "dev-certs https --trust") { CreateNoWindow = true });
	//For more information on the development certificate, <see https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0#trust/>
	var app = WebApplication.Create();

	app.Urls.Add("https://localhost:5000");

	app.MapGet("/", () => "Hello World".Dump("Run on HTTPS"));
	
	curl.GET(url: "https://localhost:5000");

	app.Run();
}