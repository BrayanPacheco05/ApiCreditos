using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Estado", "Nombre", "PasswordHash", "Rol" },
                values: new object[] { 1, "admin@miapp.com", "Activo", "Admin", "AQAAAAIAAYagAAAAEEU9sP1mdt8rz+GJwqcWhYWQ4nM7rUJ4iPlOb/iMfJbCxSexxzOixVruM7DquT5UFQ==", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
