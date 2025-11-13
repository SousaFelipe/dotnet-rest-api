using Api.Domain.Helpers;
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

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddSwaggerGen(options =>
{
    SwaggerOptionsBuilder.BuildDocInfo(options);
    SwaggerOptionsBuilder.BuildSecurityDefinitions(options);
    SwaggerOptionsBuilder.BuildSecurityRequirement(options);
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
