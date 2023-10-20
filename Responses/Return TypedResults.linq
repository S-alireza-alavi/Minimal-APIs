<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var app = WebApplication.Create();
	
	app.MapGet("/hello", () => TypedResults.Ok(new Message() {  Text = "Hello World!" }));
	
	MyExtensions.ProcessStart();
	
	app.Run();
}

//Returning TypedResults is preferred to returning Results. For more information, <see https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/responses#typedresults-vs-results/>