using Cocona;
using Console.Commands.WarmUrls;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommands<WarmUrlsCommand>();

await app.RunAsync();