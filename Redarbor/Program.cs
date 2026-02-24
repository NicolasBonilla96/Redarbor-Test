using Redarbor.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeLine();

await app.InitializeDbAsync();

app.Run();