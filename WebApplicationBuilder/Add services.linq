<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();

	// Add the memory cache services.
	builder.Services.AddMemoryCache();

	// Add a custom scoped service.
	"Service added using AddScoped".Dump("AddScoped");
	builder.Services.AddScoped<ITodoRepository, TodoRepository>();
	var app = builder.Build();
	
	app.Services.GetRequiredService<ITodoRepository>().DoSomething().Dump("DoSomething method execution...");
}

public interface ITodoRepository
{
	string DoSomething();
}

public class TodoRepository : ITodoRepository
{
	public string DoSomething()
	{
		return "Done Something";
	}
}