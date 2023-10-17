<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/",() => HelloHandler.Hello().Dump("Result"));
	
	MyExtensions.ProcessStart();

	app.Run();
}

class HelloHandler
{
	public static string Hello()
	{
		return "Hello static method";
	}
}