<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The preceding nullable and default value applies to all sources:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapPost("/products", (Product? product) =>
	{
		product.Dump("Received product");
	});
	
	var sampleProduct = new Product
    {
        Id = 1,
        Name = "Sample Product",
        Price = 19.99
    };
	
	var sampleProductJson = System.Text.Json.JsonSerializer.Serialize(sampleProduct);

	curl.POST(
        url: "http://localhost:5000/products",
        data: sampleProductJson
    );

	app.Run();
}

class Product
{
	public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
//The preceding code calls the method with a null product if no request body is sent.
//NOTE: If invalid data is provided and the parameter is nullable, the route handler is not run.