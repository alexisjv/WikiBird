using Azure.Storage.Blobs;
using WikiBird.Model.Entities;
using WikiBird.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<WikiBirdContext>();
builder.Services.AddScoped<IBirdService, BirdService>();

builder.Services.AddSingleton(new BlobServiceClient(builder.Configuration.GetConnectionString("BlobStorage")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BlobStorage}/{action=Index}/{id?}");

app.Run();
