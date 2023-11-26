<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>System.Net.Mime</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/html",() => Results.Extensions.Html(@$"<!doctype html>
	<html>
    	<head><title>miniHTML</title></head>
    	<body>
        	<h1>Hello World</h1>
        	<p>The time on the server is {DateTime.Now:O}</p>
    	</body>
	</html>").Dump("result"));

	MyExtensions.SendRequestToServer();

	app.Run();
}

public static class ResultsExtensions
{
	public static IResult Html(this IResultExtensions resultExtensions, string html)
	{
		ArgumentNullException.ThrowIfNull(resultExtensions);

		return new HtmlResult(html);
	}
}

public class HtmlResult : IResult
{
	private readonly string _html;

	public HtmlResult(string html)
	{
		_html = html;
	}

	public Task ExecuteAsync(HttpContext httpContext)
	{
		httpContext.Response.ContentType = MediaTypeNames.Text.Html;
		httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(_html);
		return httpContext.Response.WriteAsync(_html);
	}
}