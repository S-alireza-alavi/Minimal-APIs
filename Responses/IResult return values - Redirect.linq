<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var app = WebApplication.Create();

	app.MapGet("/new-path",() => "Welcome to New Path".Dump("Result"));

	app.MapGet("/old-path", () => Results.Redirect("/new-path".Dump("Redirected to new-path from old-path")));

	curl.GET();

	app.Run();
}