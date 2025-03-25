using FluentMigrator;

namespace PetPartner.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_USER_AND_ADDRESS, "Create table to save users and addresses")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("UserIdentifier").AsGuid().NotNullable();

        CreateTable("Addresses")
            .WithColumn("Street").AsString(255).NotNullable()
            .WithColumn("Number").AsString(50).NotNullable()
            .WithColumn("Neighborhood").AsString(255).NotNullable()
            .WithColumn("Complement").AsString(255).Nullable()
            .WithColumn("Country").AsString(100).NotNullable()
            .WithColumn("City").AsString(255).NotNullable()
            .WithColumn("State").AsString(100).NotNullable()
            .WithColumn("ZipCode").AsString(20).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Addresses_Users_Id", "Users", "Id");
    }
}
