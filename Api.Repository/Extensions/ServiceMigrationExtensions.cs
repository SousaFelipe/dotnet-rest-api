using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Repository.Extensions;


public static class ServiceMigrationExtensions
{
    public static void AddMigrationServiceExtension(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value
            ?? throw new Exception("❌ As string de conexão com o banco de dados não foi encontrada.");

        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(ServiceMigrationExtensions).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }


    public static void UseMigrationRunner(this IServiceProvider provider, string[] args)
    {
        if (args.Length < 1)
        {
            return;
        }

        using var scope = provider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        if (args.Length > 1 && args[0] == "down")
        {
            runner.MigrateDown(long.Parse(args[1]));
        }
        
        if (args[0] == "up")
        {
            runner.MigrateUp();
        }
    }
}
