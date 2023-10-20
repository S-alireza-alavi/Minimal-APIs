<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\Extensions.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following samples demonstrate listening on all interfaces
	//Uncomment the one you want to see the result.

	//http://*:3000
	//	var app = WebApplication.Create();
	//
	//	app.Urls.Add("http://*:3000");
	//
	//	app.MapGet("/", () => "Hello World").Dump("Result on http://*:3000");
	//	
	//	MyExtensions.ProcessStart();
	//
	//	app.Run();

	//http://+:3000
	//	var app = WebApplication.Create();
	//
	//	app.Urls.Add("http://+:3000");
	//
	//	app.MapGet("/", () => "Hello World").Dump("Result on http://+:3000");
	//	
	//	MyExtensions.ProcessStart();
	//
	//	app.Run();

	//http://0.0.0.0:3000
	//	var app = WebApplication.Create();
	//
	//	app.Urls.Add("http://0.0.0.0:3000");
	//
	//	app.MapGet("/", () => "Hello World").Dump("Result on http://0.0.0.0:3000");
	//	
	//	MyExtensions.ProcessStart();
	//
	//	app.Run();
}

