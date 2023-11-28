<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

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
	
	curl.POST(url: "http://localhost:5000/uploadstream", filePaths: new List<string> { "./Read the request body.txt" });
	
	app.Run();
}

/*
The preceding code:

Accesses the request body using HttpRequest.BodyReader.
Copies the request body to a local file.
*/