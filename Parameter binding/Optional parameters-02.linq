<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\Extensions.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//To make pageNumber optional, define the type as optional or provide a default value:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/products", (int? pageNumber) => $"Requesting page {pageNumber ?? 1}".Dump("/products"));

	string ListProducts(int pageNumber = 1) => $"Requesting page {pageNumber}";

	app.MapGet("/products2", ListProducts);

	app.Run();
}