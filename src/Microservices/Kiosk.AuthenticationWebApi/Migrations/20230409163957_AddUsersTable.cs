using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kiosk.AuthenticationWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_Name = table.Column<string>(type: "text", nullable: false),
                    User_Password = table.Column<string>(type: "text", nullable: false),
                    User_Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.Sql("INSERT INTO \"Users\" (\"User_Name\", \"User_Password\", \"User_Role\") VALUES ('Client', '0c77fe09ab336a3e57cc2b6804a9bac65e19a27782b5a9c815d585e894c3b7f2', 0)");
            migrationBuilder.Sql("INSERT INTO \"Users\" (\"User_Name\", \"User_Password\", \"User_Role\") VALUES ('Courier', 'bbc5df524d41b1e3bf11709dc12e69563b07d87e3706ededd847ad6a5e59a8f0', 1)");
            migrationBuilder.Sql("INSERT INTO \"Users\" (\"User_Name\", \"User_Password\", \"User_Role\") VALUES ('Seller', '01498fa31d5a344a6a66c69dc5671c8d0f86953c2268c1c9274b259add7bae4b', 2)");
            migrationBuilder.Sql("INSERT INTO \"Users\" (\"User_Name\", \"User_Password\", \"User_Role\") VALUES ('Admin', 'c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d6f', 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
