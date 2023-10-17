<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

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

	MyExtensions.ProcessStart();

	app.Run();
}