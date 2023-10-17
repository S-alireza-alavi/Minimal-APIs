<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

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
	
	MyExtensions.ProcessStart();

	//When the below line uncommented, it will throw FileNotFoundException:
	//Could not find file 'C:\Users\User\Documents\LINQPad Queries\Minimal APIs quick reference\WebApplicationBuilder\cert.pem'.

	//app.Run();
}