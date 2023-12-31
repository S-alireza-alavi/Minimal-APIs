<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();

	var app = builder.Build();

	app.MapGet("/download", () =>
	{
		var currentDirectory = Path.GetDirectoryName(Util.CurrentQueryPath);
		var fileName = "myfile.txt";
		var filePath = Path.Combine(currentDirectory, fileName);

		if (File.Exists(filePath))
		{
			return Results
			.File(filePath);
		}
		else
		{
			return Results.NotFound("File not found");
		}
	});

	curl.GET(url: "http://localhost:5000/download");

	app.Run();
}