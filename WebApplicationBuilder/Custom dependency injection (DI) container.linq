<Query Kind="Program">
  <NuGetReference>Autofac</NuGetReference>
  <NuGetReference>Autofac.Extensions.DependencyInjection</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Autofac.Extensions.DependencyInjection</Namespace>
  <Namespace>Autofac</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();

	builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

	// Register services directly with Autofac here. Don't
	// call builder.Populate(), that happens in AutofacServiceProviderFactory.
	builder.Host.ConfigureContainer<ContainerBuilder>((hostContext, containerBuilder) =>
	{
		containerBuilder.RegisterModule(new MyCustomModule());
	});

	var app = builder.Build();
	
	var myService = app.Services.GetRequiredService<IMyService>();
	
	var message = myService.GetMessage();
	
	message.Dump("Service result");
}

public class MyCustomModule : Autofac.Module
{
	protected override void Load(ContainerBuilder builder)
	{
		// Register your services and dependencies here
		builder.RegisterType<MyService>().As<IMyService>();
	}
}

public interface IMyService
{
	string GetMessage();
}

public class MyService : IMyService
{
	public string GetMessage()
	{
		return "Hello from MyService";
	}
}