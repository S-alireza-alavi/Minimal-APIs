<Query Kind="Program">
<Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\Extensions.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following samples demonstrate listening on all interfaces
	//Uncomment the method you want to execute.

	// Method 1: http://*:5000
	 RunOnAllInterfaces();

	// Method 2: http://+:5000
	// RunOnPlus();

	// Method 3: http://0.0.0.0:5000
	// RunOnSpecificIpAddress();
	
}

void RunOnAllInterfaces()
{
	var app = WebApplication.Create();
	
	app.Urls.Add("http://*:5000");
	
	app.MapGet("/", () => "Hello World".Dump("Result on http://*:5000"));
	
	MyExtensions.SendRequestToServer();
	
	app.Run();
}

void RunOnPlus()
{
		var app = WebApplication.Create();
	
		app.Urls.Add("http://+:5000");
	
		app.MapGet("/", () => "Hello World".Dump("Result on http://+:5000"));
		
		MyExtensions.SendRequestToServer();
	
		app.Run();
}

void RunOnSpecificIpAddress()
{
		var app = WebApplication.Create();
	
		app.Urls.Add("http://0.0.0.0:5000");
	
		app.MapGet("/", () => "Hello World".Dump("Result on http://0.0.0.0:5000"));
		
		MyExtensions.SendRequestToServer();
	
		app.Run();
}

