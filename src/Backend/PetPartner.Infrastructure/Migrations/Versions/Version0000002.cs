using FluentMigrator;

namespace PetPartner.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_PET, "Create table to save pets.")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Pets")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Species").AsString(100).NotNullable()
            .WithColumn("Breed").AsString(100).NotNullable()
            .WithColumn("BirthDate").AsDateTime().NotNullable()
            .WithColumn("Gender").AsString(10).NotNullable() // "Male" or "Female"
            .WithColumn("HasPedigree").AsBoolean().Nullable()
            .WithColumn("Color").AsString(50).Nullable()
            .WithColumn("Weight").AsDecimal(5, 2).Nullable()

            // Health Information
            .WithColumn("Vaccinated").AsBoolean().NotNullable()
            .WithColumn("Dewormed").AsBoolean().NotNullable()
            .WithColumn("HealthNotes").AsString(1000).Nullable()

            // Availability for Sale or Adoption
            .WithColumn("AvailableForSale").AsBoolean().NotNullable()
            .WithColumn("AvailableForAdoption").AsBoolean().NotNullable()
            .WithColumn("Price").AsDecimal(10, 2).Nullable()

            // Breeding Information
            .WithColumn("AvailableForBreeding").AsBoolean().NotNullable()
            .WithColumn("BreedingNotes").AsString(1000).Nullable()

            // Relationship with User (Owner)
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Pets_Users_Id", "Users", "Id")
            .OnDelete(System.Data.Rule.Cascade);

        CreateTable("Pictures")
            .WithColumn("PetId").AsInt64().NotNullable().ForeignKey("FK_Photos_Pets_Id", "Pets", "Id")
            .OnDelete(System.Data.Rule.Cascade)
            .WithColumn("Url").AsString(5000).NotNullable();
    }
}