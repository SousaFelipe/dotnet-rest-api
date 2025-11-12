using Api.Data.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using NHibernate.Driver;


namespace Api.Data;


public class DataBase
{
    private static readonly object _lock = new();
    private static ISessionFactory? _sessionFactory;


    public static ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory == null)
            {
                lock (_lock)
                {
                    _sessionFactory ??= CreateSessionFactory();
                }
            }

            return _sessionFactory;
        }
    }


    private static ISessionFactory CreateSessionFactory()
    {
        try
        {
            Configuration configuration = new();
            
            configuration.DataBaseIntegration(DataBaseIntegrationAction);

            configuration.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
            configuration.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");

            configuration.AddAssembly(typeof(User).Assembly);

            return configuration.BuildSessionFactory();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }


    private static void DataBaseIntegrationAction(DbIntegrationConfigurationProperties db)
    {
        db.ConnectionString = "Server=localhost;Database=dotnet_rest_api_db;User Id=sa;Password=Strong(#01)Password;TrustServerCertificate=True;";
        db.Dialect<MsSql2012Dialect>();
        db.Driver<MicrosoftDataSqlClientDriver>();
        db.LogSqlInConsole = true;
        db.LogFormattedSql = true;
        db.AutoCommentSql = true;
    }


    public static ISession OpenSession()
    {
        return SessionFactory.OpenSession();
    }
}
