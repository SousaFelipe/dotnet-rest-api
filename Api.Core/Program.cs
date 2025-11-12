using Api.Core;
using Api.Data.Extensions;
using Api.Data.Interfaces;
using Api.Data.Repositories;
using Api.Service.Interfaces;
using Api.Service.Services;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Extensão criada em /Api.Data/Extensions/ServiceCollectionExtensions.cs
builder.Services.AddDataServiceExtension(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "1.0.0",
        Title = ".NET REST API",
        Description = "API REST com um CRUD simples para manipulação de autenticação de usuários",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "flpssdocarmo@gmail.com",
            Name = "Felipe S. Carmo",
            Url = new Uri("https://github.com/SousaFelipe")
        }
    });
});


WebApplication app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
