<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following code displays SortBy:xyz, SortDirection:Desc, CurrentPage:99 with the URI /products?SortBy=xyz&SortDir=Desc&Page=99:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	// GET /products?SortBy=xyz&SortDir=Desc&Page=99
	app.MapGet("/products", (PagingData pageData) => $"SortBy: {pageData.SortBy}, " +
	$"SortDirection: {pageData.SortDirection}, CurrentPage: {pageData.CurrentPage}".Dump("Result"));
	
	MyExtensions.ProcessStart();
	
	app.Run();
}

public class PagingData
{
	public string? SortBy { get; init; }
	public SortDirection SortDirection { get; init; }
	public int CurrentPage { get; init; } = 1;

	public static ValueTask<PagingData?> BindAsync(HttpContext context,
	ParameterInfo parameter)
	{
		const string sortByKey = "sortBy";
		const string sortDirectionKey = "sortDir";
		const string currentPageKey = "page";

		Enum.TryParse<SortDirection>(context.Request.Query[sortDirectionKey],
		ignoreCase: true, out var sortDirection);
		int.TryParse(context.Request.Query[currentPageKey], out var page);
		page = page == 0 ? 1 : page;

		var result = new PagingData
		{
			SortBy = context.Request.Query[sortByKey],
			SortDirection = sortDirection,
			CurrentPage = page
		};

		return ValueTask.FromResult<PagingData?>(result);
	}
}

public enum SortDirection
{
	Defualt,
	Asc,
	Desc
}