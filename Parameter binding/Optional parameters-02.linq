<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//To make pageNumber optional, define the type as optional or provide a default value:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/products", (int? pageNumber) => $"Requesting page {pageNumber ?? 1}".Dump("/products"));

	string ListProducts(int pageNumber = 1) => $"Requesting page {pageNumber}".Dump("/products2");

	app.MapGet("/products2", ListProducts);
	
	curl.GET(url: "http://localhost:5000/products");
	curl.GET(url: "http://localhost:5000/products?pageNumber=2");

	curl.GET(url: "http://localhost:5000/products2");
	curl.GET(url: "http://localhost:5000/products2?pageNumber=2");

	app.Run();
}