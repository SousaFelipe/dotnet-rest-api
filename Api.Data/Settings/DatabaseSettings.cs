namespace Api.Data.Settings;


public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool ShowSql { get; set; }
    public bool FormatSql { get; set; }
    public int CommandTimeout { get; set; }
    public short BatchSize { get; set; }
}
