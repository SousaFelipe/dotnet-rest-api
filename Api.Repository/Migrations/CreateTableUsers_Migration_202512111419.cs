using FluentMigrator;


namespace Api.Repository.Migrations;


[Migration(202512111419)]
public class CreateTableUsers_Migration_202512111419 : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(64).NotNullable()
            .WithColumn("surname").AsString(64).NotNullable()
            .WithColumn("email").AsString(64).Unique().NotNullable()
            .WithColumn("password").AsString(255).NotNullable()
            .WithColumn("phone_number").AsString(16).NotNullable()
            .WithColumn("birth_date").AsDate().NotNullable();
    }


    public override void Down()
    {
        Delete.Table("users");
    }
}
