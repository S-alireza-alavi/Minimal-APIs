<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//I've modified the sample code a little to see the 'Succeed' message.
	//If you want to check the Failed message, you can comment '1' and '2' parts...
	
	//1
	var customConfiguration = new Dictionary<string, string>
	{
		{ "HelloKey", "CustomHelloValue" },
    };

	var builder = WebApplication.CreateBuilder();
	
	//2
	foreach (var kvp in customConfiguration)
	{
		builder.Configuration[kvp.Key] = kvp.Value;
	}
	
	var app = builder.Build();

	var message = app.Configuration["HelloKey"]?.Dump("Succeed") ?? "Config failed!".Dump("Failed");

	app.MapGet("/",() => message);

	curl.GET();

	app.Run();
}