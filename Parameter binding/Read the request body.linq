<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//Read the request body directly using a HttpContext or HttpRequest parameter:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapPost("/uploadstream", async (IConfiguration config, HttpRequest request) =>
	{
		var filePath = Path.Combine(config["StoredFilesPath"], Path.GetRandomFileName());
		
		await using var writeStream = File.Create(filePath);
		await request.BodyReader.CopyToAsync(writeStream);
	});
	
	MyExtensions.ProcessStart();
	
	app.Run();
}

/*
The preceding code:

Accesses the request body using HttpRequest.BodyReader.
Copies the request body to a local file.
*/