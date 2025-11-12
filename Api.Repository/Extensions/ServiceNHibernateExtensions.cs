using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using NHibernate.Driver;
using Api.Repository.Settings;
using Api.Repository.Interfaces;


namespace Api.Repository.Extensions;


public static class ServiceNHibernateExtensions
{
    public static IServiceCollection AddDataBaseServiceExtension(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var dataBaseSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        services.AddScoped(provider =>
        {
            return BuildSessionFactory(dataBaseSettings);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }


    private static ISessionFactory BuildSessionFactory(DatabaseSettings? settings)
    {
        if (settings == null)
        {
            throw new Exception("❌ As configurações do banco de dados não foram encontradas.");
        }

        var configuration = new Configuration();
        configuration.DataBaseIntegration(db => DataBaseIntegrationAction(db, settings));
        configuration.AddAssembly(typeof(ServiceNHibernateExtensions).Assembly);

        return configuration.BuildSessionFactory();
    }
    

    private static void DataBaseIntegrationAction(
        DbIntegrationConfigurationProperties db,
        DatabaseSettings settings)
    {
        db.ConnectionString = settings.ConnectionString;
        db.Dialect<MsSql2012Dialect>();
        db.Driver<MicrosoftDataSqlClientDriver>();
        db.LogSqlInConsole = settings.ShowSql;
        db.LogFormattedSql = settings.FormatSql;
        db.Timeout = (byte)settings.CommandTimeout;
        db.BatchSize = settings.BatchSize;
    }
}
