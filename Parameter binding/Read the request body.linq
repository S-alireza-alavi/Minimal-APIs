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

	var storedFilesPath = Path.GetDirectoryName(Util.CurrentQueryPath);

	app.MapPost("/uploadstream", async (HttpRequest request) =>
	{
		var filePath = Path.Combine(storedFilesPath, "RequestBody.txt");

		await using var writeStream = File.Create(filePath);
		await request.BodyReader.CopyToAsync(writeStream);
		
		filePath.Dump("Generated file path");
	});
	
	curl.POST(url: "http://localhost:5000/uploadstream", data: "test");

	app.Run();
}

/*
The preceding code:

Accesses the request body using HttpRequest.BodyReader.
Copies the request body to a local file.
*/