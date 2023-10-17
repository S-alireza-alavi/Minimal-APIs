<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var app = WebApplication.Create();

	app.MapGet("/new-path",() => "Welcome to New Path".Dump("Result"));

	app.MapGet("/old-path", () => Results.Redirect("/new-path".Dump("Redirected to new-path from old-path")));

	MyExtensions.ProcessStart();

	app.Run();
}