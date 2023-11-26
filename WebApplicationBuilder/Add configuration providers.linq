<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">D:\Repositories\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following sample adds the INI configuration provider:
	Environment.CurrentDirectory = Path.GetDirectoryName(Util.CurrentQueryPath);

	var builder = WebApplication.CreateBuilder();

	builder.Configuration.AddIniFile("appsettings.conf");

	var app = builder.Build();

	var connectionString = builder.Configuration["ConnectionString"];
	var someSetting = builder.Configuration["SomeSetting"];

	connectionString.Dump("ConnectionString");
	someSetting.Dump("SomeSetting");

	MyExtensions.SendRequestToServer();

	app.Run();
}