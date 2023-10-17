<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var app = WebApplication.Create();
	
	app.MapGet("/text", () => Results.Text("This is some text".Dump("Result")));
	
	MyExtensions.ProcessStart();
	
	app.Run();
}