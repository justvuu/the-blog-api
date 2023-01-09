using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Data;
using TheBlogAPI.Interface;
using TheBlogAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://28dc-14-0-18-72.ap.ngrok.io", "http://localhost:3000");
        });
});
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IVocabRepository, VocabRepository>();
builder.Services.AddScoped<IVocabCategoryRepository, VocabCategoryRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TheBlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TheBlogConnectionString")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

