using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using NHibernate.Driver;
using Api.Repository.Interfaces;
using Api.Repository.Settings;


namespace Api.Repository.Extensions;


public static class NHibernateServiceExtension
{
    public static IServiceCollection AddDataBaseServiceExtension(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var configSection = configuration.GetSection("DatabaseSettings");
        var dataBaseSettings = configSection.Get<DatabaseSettings>();

        services.Configure<DatabaseSettings>(configSection);

        services.AddSingleton(provider =>
        {
            return BuildSessionFactory(dataBaseSettings);
        });

        services.AddScoped(provider =>
        {
            var sessionFactory = provider.GetRequiredService<ISessionFactory>();
            return sessionFactory.OpenSession();
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
        configuration.AddAssembly(typeof(NHibernateServiceExtension).Assembly);

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
