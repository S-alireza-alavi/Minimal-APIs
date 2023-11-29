<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var app = WebApplication.Create();
	
	app.MapGet("/text", () => Results.Text("This is some text".Dump("Result")));
	
	curl.GET(url: "http://localhost:5000/text");
	
	app.Run();
}