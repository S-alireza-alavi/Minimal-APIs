<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Threading.Channels</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>Microsoft.Extensions.Hosting</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	/*
		The request body can bind as a Stream or PipeReader to efficiently support scenarios where the user has to process data and:

		Store the data to blob storage or enqueue the data to a queue provider.
		Process the stored data with a worker process or cloud function.
		For example, the data might be enqueued to Azure Queue storage or stored in Azure Blob storage.

		The following code implements a background queue:
	*/

	var builder = WebApplication.CreateBuilder();
	// The max memory to use for the upload endpoint on this instance.
	var maxMemory = 500 * 1024 * 1024;

	// The max size of a single message, staying below the default LOH size of 85K.
	var maxMessageSize = 80 * 1024;

	// The max size of the queue based on those restrictions
	var maxQueueSize = maxMemory / maxMessageSize;

	// Create a channel to send data to the background queue.
	builder.Services.AddSingleton<Channel<ReadOnlyMemory<byte>>>((_) =>
						 Channel.CreateBounded<ReadOnlyMemory<byte>>(maxQueueSize));

	// Create a background queue service.
	builder.Services.AddHostedService<BackgroundQueue>();
	var app = builder.Build();

	// curl --request POST 'https://localhost:<port>/register' --header 'Content-Type: application/json' --data-raw '{ "Name":"Samson", "Age": 23, "Country":"Nigeria" }'
	// curl --request POST "https://localhost:<port>/register" --header "Content-Type: application/json" --data-raw "{ \"Name\":\"Samson\", \"Age\": 23, \"Country\":\"Nigeria\" }"
	"Must simulate a POST request including Person information as JSON to see the result.".Dump("Tip");
	app.MapPost("/register", async (HttpRequest req, Stream body,
	Channel<ReadOnlyMemory<byte>> queue) =>
	{
		if (req.ContentLength is not null && req.ContentLength > maxMessageSize)
		{
			return Results.BadRequest();
		}

		// We're not above the message size and we have a content length, or
		// we're a chunked request and we're going to read up to the maxMessageSize + 1. 
		// We add one to the message size so that we can detect when a chunked request body
		// is bigger than our configured max.
		var readSize = (int?)req.ContentLength ?? (maxMessageSize + 1);

		var buffer = new byte[readSize];

		// Read at least that many bytes from the body.
		var read = await body.ReadAtLeastAsync(buffer, readSize, throwOnEndOfStream: false);

		// We read more than the max, so this is a bad request.
		if (read > maxMessageSize)
		{
			return Results.BadRequest();
		}

		// Attempt to send the buffer to the background queue.
		if (queue.Writer.TryWrite(buffer.AsMemory(0..read)))
		{
			return Results.Accepted().Dump("Success");
		}

		// We couldn't accept the message since we're overloaded.
		return Results.StatusCode(StatusCodes.Status429TooManyRequests);
	});
	
	app.Run();
}

class BackgroundQueue : BackgroundService
{
	private readonly Channel<ReadOnlyMemory<byte>> _queue;
	private readonly ILogger<BackgroundQueue> _logger;

	public BackgroundQueue(Channel<ReadOnlyMemory<byte>> queue,
							   ILogger<BackgroundQueue> logger)
	{
		_queue = queue;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await foreach (var dataStream in _queue.Reader.ReadAllAsync(stoppingToken))
		{
			try
			{
				var person = JsonSerializer.Deserialize<Person>(dataStream.Span)!;
				_logger.LogInformation($"{person.Name} is {person.Age} " +
									   $"years and from {person.Country}");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}
		}
	}
}

class Person
{
	public string Name { get; set; } = String.Empty;
	public int Age { get; set; }
	public string Country { get; set; } = String.Empty;
}