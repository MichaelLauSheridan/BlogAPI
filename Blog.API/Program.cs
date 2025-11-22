using Blog.Core.Interfaces;
using Blog.Infrastructure.Data;
// using Blog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Register DbContext with SQLite
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
// builder.Services.AddScoped<IPostRepository, PostRepository>();
// builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

// Map controller routes (very important)
app.MapControllers();

app.Run();
