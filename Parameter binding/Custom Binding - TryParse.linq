<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	/*
		There are two ways to customize parameter binding:

		For route, query, and header binding sources, bind custom types by adding a static TryParse method for the type.
		Control the binding process by implementing a BindAsync method on a type.
	*/

	//The following code displays Point: 12.3, 10.1 with the URI /map?Point=12.3,10.1:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	// GET /map?Point=12.3,10.1
	app.MapGet("/map", (Point point) => $"Point: {point.X}, {point.Y}".Dump("Result"));
	
	MyExtensions.SendRequestToServer();
	
	app.Run();
}

public class Point
{
	public double X { get; set; }
	public double Y { get; set; }

	public static bool TryParse(string? value, IFormatProvider? provider, out Point? point)
	{
		// Format is "(12.3,10.1)"
		var trimmedValue = value?.TrimStart('(').TrimEnd(')');
		var segments = trimmedValue?.Split(',',
		StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		if (segments?.Length == 2
		&& double.TryParse(segments[0], out var x)
		&& double.TryParse(segments[1], out var y))
		{
			point = new Point { X = x, Y = y };
			return true;
		}

		point = null;
		return false;
	}
}

//static bool TryParse(string value, out T result);
//static bool TryParse(string value, IFormatProvider provider, out T result);