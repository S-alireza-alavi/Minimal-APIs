<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//Route parameters can be captured as part of the route pattern definition:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();
	
	"You must use this pattern 'http://localhost:5000/users/{userId}/books/{bookId}' to send the request in order to see the result".Dump("Tip");

	app.MapGet("/users/{userId}/books/{bookId}",
		(int userId, int bookId) => $"The user id is {userId} and book id is {bookId}".Dump("Result"));

	curl.GET(url: "http://localhost:5000/users/1/books/5");

	app.Run();
}

/*
The preceding code returns The user id is 3 and book id is 7 from the URI /users/3/books/7.

The route handler can declare the parameters to capture. When a request is made a route with parameters declared to capture, the parameters are parsed and passed to the handler. This makes it easy to capture the values in a type safe way. In the preceding code, userId and bookId are both int.

In the preceding code, if either route value cannot be converted to an int, an exception is thrown. The GET request /users/hello/books/3 throws the following exception:

BadHttpRequestException: Failed to bind parameter "int userId" from "hello".
*/