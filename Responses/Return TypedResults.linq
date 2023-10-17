<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var app = WebApplication.Create();
	
	app.MapGet("/hello", () => TypedResults.Ok(new Message() {  Text = "Hello World!" }));
	
	MyExtensions.ProcessStart();
	
	app.Run();
}

//Returning TypedResults is preferred to returning Results. For more information, <see https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/responses#typedresults-vs-results/>