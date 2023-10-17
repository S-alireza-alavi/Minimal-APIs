<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//I've modified the sample code a little to see the 'Succeed' message.
	//If you want to check the Failed message as well, you can comment '1' and '2' parts...
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

	var message = app.Configuration["HelloKey"].Dump("Succeed") ?? "Config failed!".Dump("Failed");

	app.MapGet("/",() => message);

	MyExtensions.ProcessStart();

	app.Run();
}