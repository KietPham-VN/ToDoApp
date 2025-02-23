using ToDoApp.Application.Dtos;
using ToDoApp.Application.Services;
using ToDoApp.infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddTransient<IGuidGenerator, GuidGenerator>();
builder.Services.AddSingleton<ISingletonGenerator, SingletonGenerator>();
builder.Services.AddTransient<GuidData>();
// DI container, IServiceProvider

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
