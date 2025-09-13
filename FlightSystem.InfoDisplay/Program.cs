var builder = WebApplication.CreateBuilder(args);

// Blazor Server сервисүүд нэмэх
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// HttpClient тохируулах
builder.Services.AddHttpClient("FlightSystem", client =>
{
    client.BaseAddress = new Uri("https://localhost:7261");
    client.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

// HTTP pipeline тохируулах
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Blazor Hub маршрут нэмэх
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

Console.WriteLine("🛫 Нислэгийн мэдээллийн дэлгэц эхэллээ...");
Console.WriteLine("📺 Веб хуудас: https://localhost:7285");
Console.WriteLine("🔗 API сервер: https://localhost:7261");

app.Run();
