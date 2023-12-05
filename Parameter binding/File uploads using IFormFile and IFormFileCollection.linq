<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/", () => "Hello World!".Dump("MapGet execution"));

	app.MapPost("/upload", async (IFormFile file) =>
	{
		file.Dump("file");

		if (file != null && file.Length > 0)
		{
			var tempFile = Path.GetTempFileName();
			app.Logger.LogInformation(tempFile);
			using var stream = File.OpenWrite(tempFile);
			await file.CopyToAsync(stream);

			tempFile.Dump("tempFilePath");
		}
		else
		{
			app.Logger.LogInformation("Empty file received");
		}
	}).DisableAntiforgery();

	app.MapPost("/upload_many", async (IFormFileCollection myFiles) =>
	{
		myFiles.Dump("Files");

		foreach (var file in myFiles)
		{
			var tempFile = Path.GetTempFileName();

			if (file.Length > 0)
			{
				app.Logger.LogInformation(tempFile);
				using var stream = File.OpenWrite(tempFile);
				await file.CopyToAsync(stream);

				tempFile.Dump("tempFilesPath");
			}
			else
			{
				app.Logger.LogInformation("Empty files received");
			}
		}
	}).DisableAntiforgery();

	curl.GET(url: "http://localhost:5000");

	var directory = Path.GetDirectoryName(Util.CurrentQueryPath);

	var filePath = Path.Combine(directory, "File upload using IFormFile.txt");
	filePath.Dump("filePath");
	curl.POST(url: "http://localhost:5000/upload", filePaths: new List<string> { filePath });

	var filePaths = new List<string> { Path.Combine(directory, "File uploads using IFormFileCollection-first.txt"),
	Path.Combine(directory, "File uploads using IFormFileCollection-second.txt") };
	curl.POST(url: "http://localhost:5000/upload_many", filePaths: filePaths);

	app.Run();
}