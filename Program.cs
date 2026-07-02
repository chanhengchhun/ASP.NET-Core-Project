var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("x-my-custom-header", "My customheader value");
    await next.Invoke();
});

app.MapGet("/", () => "Hello World!");

app.MapGet("/About", () => "This is about page!");

app.Run();
