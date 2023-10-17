<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var app = WebApplication.Create();
	
	app.MapGet("/405", () => Results.StatusCode(405).Dump("405 error"));
	
	MyExtensions.ProcessStart();
	
	app.Run();
}