<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\Extensions.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	var handler = new HelloHandler();

	app.MapGet("/", () => handler.Hello().Dump("Result"));
	
	MyExtensions.SendRequestToServer();

	app.Run();
}

	class HelloHandler
{
	public string Hello()
	{
		return "Hello Instance method";
	}
}