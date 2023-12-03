<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>System.Security.Claims</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following types are bound without explicit attributes:
	//Uncomment the one you want to test...
	var app = WebApplication.Create();

	//HttpContext
	//app.MapGet("/", (HttpContext context) => context.Response.WriteAsync("Hello World".Dump("HttpContext")));

	//HttpRequest and HttpResponse
	//app.MapGet("/", (HttpRequest request, HttpResponse response) =>
	//response.WriteAsync($"Hello World {request.Query["name"]}".Dump("HttpRequest and HttpResponse using Query String")));

	//CancellationToken
	app.MapGet("/", async (CancellationToken cancellationToken) =>
	await MakeLongRunningRequestAsync(cancellationToken).Dump("Doing the task"));

	//ClaimsPrincipal
	//app.MapGet("/", (ClaimsPrincipal user) => user.Identity.Name.Dump("ClaimsPrincipal"));

	curl.GET();

	app.Run();
}

async Task MakeLongRunningRequestAsync(CancellationToken cancellationToken)
{
	try
	{
		for (int i = 0; i < 10; i++)
		{
			cancellationToken.ThrowIfCancellationRequested();

			Console.WriteLine($"Working on iteration {i}");
			await Task.Delay(TimeSpan.FromSeconds(1));
			
			cancellationToken.ThrowIfCancellationRequested();
		}
	}
	catch (TaskCanceledException)
	{
		Console.WriteLine("Task was canceled.".Dump("Canceled"));
	}
	catch (Exception ex)
	{
		Console.WriteLine($"An error occurred: {ex.Message}".Dump("Error"));
	}
}