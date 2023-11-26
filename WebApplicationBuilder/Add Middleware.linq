<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">D:\Repositories\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//Any existing ASP.NET Core middleware can be configured on the WebApplication:
	var app = WebApplication.Create();

	// Setup the file server to serve static files.
	app.UseFileServer();
	
	var currentDirectory = Path.GetDirectoryName(Util.CurrentQueryPath);
	var fileName = "FileServer.txt";
	var filePath = Path.Combine(currentDirectory, fileName);

	app.MapGet("/", () =>
    {
		if (File.Exists(filePath))
		{
			var fileContents = File.ReadAllText(filePath);
			fileContents.Dump("Contents of Static File");
			return fileContents;
		}
		else
		{
			"Static file not found.".Dump();
			return "Static file not found.";
		}
	});

	MyExtensions.SendRequestToServer();

	app.Run();
}