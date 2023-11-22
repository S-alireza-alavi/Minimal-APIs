<Query Kind="Program">
<Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following sections set the port the app responds to.
	var app = WebApplication.Create();

	app.MapGet("/", () => "Hello World!".Dump("result"));

	MyExtensions.ProcessStart(3000);

	app.Run("http://localhost:3000".Dump("Run on 3000 port"));
}