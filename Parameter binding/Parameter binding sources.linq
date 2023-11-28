<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	/*
	Parameter binding is the process of converting request data into strongly typed parameters that are expressed by route handlers.
	A binding source determines where parameters are bound from. Binding sources can be explicit or inferred based on HTTP method and parameter type.

	Supported binding sources:

	Route values
	Query string
	Header
	Body (as JSON)
	Services provided by dependency injection
	Custom
	Binding from form values is not natively supported in .NET 6 and 7.
	*/

	//The following GET route handler uses some of these parameter binding sources:
	var builder = WebApplication.CreateBuilder();

	//Added as service
	builder.Services.AddSingleton<Service>();

	var app = builder.Build();

	"1. Route values\n2. Query string\n3. Header\n4. Body(as JSON)\n5. Services provided by dependency injection\n6. Custom".Dump("Supported binding sources: ");

	app.MapGet("/{id}", (int id,
	int page,
	[FromHeader(Name = "X-CUSTOM-HEADER")] string customHeader,
	Service service) =>
	{
		$"Received request with id: {id}".Dump("id");
		$"Received request with page: {page}".Dump("page");
		$"Received request with custom header: {customHeader}".Dump("header");
		$"Service instance: {service}".Dump("service");
	});

	curl.GET(url: "http://localhost:5000/123?page=1", headers: new Dictionary<string, string> { { "X-CUSTOM-HEADER", "header-value" } });

	app.Run();
}

class Service
{
}