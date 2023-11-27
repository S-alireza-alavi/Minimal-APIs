<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var app = WebApplication.Create();

	var proxyClient = new HttpClient();
	app.MapGet("/pokemon", async () =>
	{
		var stream = await proxyClient.GetStreamAsync("https://jsonplaceholder.typicode.com/users");
		//Proxy the response as JSON
		//return Results.Stream(stream, "application.json");

		var jsonData = new StreamReader(stream).ReadToEnd();
		jsonData.Dump("JSON data");
	});

	curl.GET();

	app.Run();
}