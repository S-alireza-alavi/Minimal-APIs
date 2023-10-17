<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	var handler = new HelloHandler();

	app.MapGet("/", () => handler.Hello().Dump("Result"));
	
	MyExtensions.ProcessStart();

	app.Run();
}

	class HelloHandler
{
	public string Hello()
	{
		return "Hello Instance method";
	}
}