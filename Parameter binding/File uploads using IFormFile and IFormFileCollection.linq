<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/", () => "Hello World!".Dump("MapGet execution"));

	app.MapPost("/upload", async (IFormFile file) => 
	{
		var tempFile = Path.GetTempFileName().Dump("tempFile");
		file.Dump("fileDump");
		app.Logger.LogInformation(tempFile);
		using var stream = File.OpenWrite(tempFile);
		await file.CopyToAsync(stream);
	});

	app.MapPost("/upload_many", async (IFormFileCollection myFiles) => 
	{
		myFiles.Dump("Files");
		foreach (var file in myFiles)
		{
			var tempFile = Path.GetTempFileName().Dump("tempFile2");
			file.Dump("file");
			app.Logger.LogInformation(tempFile);
			using var stream = File.OpenWrite(tempFile);
			await file.CopyToAsync(stream);
		}
	});
	
	MyExtensions.ProcessStart();
	
	app.Run();
}