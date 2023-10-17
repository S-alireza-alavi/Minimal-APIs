<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//In the following code, the app responds to port 3000 and 4000.
	var app = WebApplication.Create();

	app.Urls.Add("http://localhost:3000");
	app.Urls.Add("http://localhost:4000");

	app.MapGet("/", () => "Hello World".Dump("result"));

	foreach (var url in app.Urls)
	{
		MyExtensions.ProcessStart();
	}

	"I can run on both 3000 and 4000 ports!".Dump("Multiple ports running");

	app.Run();
}