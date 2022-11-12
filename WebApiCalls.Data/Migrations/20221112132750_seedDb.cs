using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

#nullable disable

namespace WebApiCalls.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                       table: "Contacts",
                       columns: new[] { "Phone", "FIO", "Company", "Description" },
                        values: new object[] { "+1-111-111-11-11", "FIO1", "Company1", "Desc1" }
            );

            migrationBuilder.InsertData(
                       table: "Contacts",
                       columns: new[] { "Phone", "FIO", "Company", "Description" },
                        values: new object[] { "+2-111-111-11-11", "FIO2", "Company2", "Desc2" }
            );
            migrationBuilder.InsertData(
                       table: "Contacts",
                       columns: new[] {"Phone", "FIO", "Company", "Description", "Locked" },
                        values: new object[] { "+3-111-111-11-11", "FIO3", "Company3", "Desc3", false }
            );
            migrationBuilder.InsertData(
                       table: "Contacts",
                       columns: new[] { "Phone", "FIO", "Company", "Description", "Locked" },
                        values: new object[] { "+4-111-111-11-11", "FIO4", "Company4", "Desc4", false }
            );

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    FIO = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Company = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "character varying(384)", maxLength: 384, nullable: false),
                    Locked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });
        }
    }
}
