<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Routing</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//Endpoints can be given names in order to generate URLs to the endpoint. Using a named endpoint avoids having to hard code paths in an app:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/hello", () => "Hello named route".Dump("result"))
	   .WithName("hi");

	app.MapGet("/", (LinkGenerator linker) =>
			$"The link to the hello route is {linker.GetPathByName("hi", values: null)}".Dump("Link generated"));

	MyExtensions.SendRequestToServer();

	app.Run();
}

/*
The preceding code displays The link to the hello endpoint is /hello from the / endpoint.

NOTE: Endpoint names are case sensitive.

Endpoint names:

Must be globally unique.
Are used as the OpenAPI operation id when OpenAPI support is enabled. For more information, <see https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/openapi?view=aspnetcore-7.0/>.
*/