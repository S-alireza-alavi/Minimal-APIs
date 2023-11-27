<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following catch all route returns Routing to hello from the `/posts/hello' endpoint:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();
	
	"You must use this pattern 'http://localhost:5000/posts/{*rest}' to send the request in order to see the result".Dump("Tip");

	app.MapGet("/posts/{*rest}", (string rest) => $"Routing to {rest}".Dump($"/Result of using '{rest}' as a parameter"));
	
	curl.GET(url: "http://localhost:5000/posts/parameter");

	app.Run();
}