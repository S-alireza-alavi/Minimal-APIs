<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//Parameter binding for minimal APIs binds parameters through dependency injection when the type is configured as a service. It's not necessary to explicitly apply the [FromServices] attribute to a parameter. In the following code, both actions return the time:
	var builder = WebApplication.CreateBuilder();
	builder.Services.AddSingleton<IDateTime, SystemDateTime>();

	var app = builder.Build();

	app.MapGet("/", (IDateTime dateTime) =>
	{
		var now = dateTime.Now;
		now.Dump("result");
		return now;
	});
	app.MapGet("/fs", ([FromServices] IDateTime dateTime) =>
	{
		var now = dateTime.Now;
		now.Dump("Result using [FromServices]");
		return now;
	});
	app.Run();
}

public interface IDateTime
{
	DateTime Now { get; }
}

public class SystemDateTime : IDateTime
{
	public DateTime Now => DateTime.Now;
}
