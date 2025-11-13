using Api.Domain.Core;
using Api.Domain.Extensions;
using Api.Repository.Extensions;
using Api.Service.Interfaces;
using Api.Service.Services;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddDataBaseServiceExtension(builder.Configuration);
builder.Services.AddMigrationServiceExtension(builder.Configuration);
builder.Services.AddJwtServiceExtension(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddControllers();
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
    app.Services.UseMigrationRunner(args);
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
