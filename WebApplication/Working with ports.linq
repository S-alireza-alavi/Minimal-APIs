<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following sections set the port the app responds to.
	var app = WebApplication.Create();

	app.MapGet("/", () => "Hello World!").Dump("result");

	Process.Start(new ProcessStartInfo("curl", "http://localhost:3000") { CreateNoWindow = true });

	app.Run("http://localhost:3000".Dump("Ran on 3000 port"));
}