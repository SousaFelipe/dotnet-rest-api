using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Repository.Extensions;


public static class MigrationsRunnerServiceExtension
{
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
