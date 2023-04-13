using AM.Infra.EFCore.Persistence;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SettingService.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.MigrateDataFromScript();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
