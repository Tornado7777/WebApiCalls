using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

#nullable disable

namespace WebApiCalls.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedDbTablesCalls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                        table: "Calls",
                        columns: new[] { "FromId", "ToId", "TimeStart", "TimeEnd" },
                         values: new object[] { 1, 2, new DateTime(1990, 4, 12, 0, 0, 0, 0,
                                                DateTimeKind.Unspecified), new DateTime(1990, 4, 12, 0, 20, 0, 0,
                                                DateTimeKind.Unspecified)}
             );

            migrationBuilder.InsertData(
                        table: "Calls",
                        columns: new[] { "FromId", "ToId", "TimeStart", "TimeEnd" },
                         values: new object[] { 1, 2,new DateTime(1990, 4, 12, 0, 0, 0, 0,
                                                  DateTimeKind.Unspecified), new DateTime(1990, 4, 12, 0, 20, 0, 0,
                                                  DateTimeKind.Unspecified)}
             );
            migrationBuilder.InsertData(
                        table: "Calls",
                        columns: new[] { "FromId", "ToId", "TimeStart", "TimeEnd" },
                         values: new object[] { 1, 3, new DateTime(1990, 4, 12, 0, 0, 0, 0,
                                                  DateTimeKind.Unspecified), new DateTime(1990, 4, 12, 0, 20, 0, 0,
                                                  DateTimeKind.Unspecified)}
             );
            migrationBuilder.InsertData(
                        table: "Calls",
                        columns: new[] { "FromId", "ToId", "TimeStart", "TimeEnd" },
                         values: new object[] { 1, 4, new DateTime(1990, 4, 12, 0, 0, 0, 0,
                                                  DateTimeKind.Unspecified), new DateTime(1990, 4, 12, 0, 20, 0, 0,
                                                  DateTimeKind.Unspecified)}
             );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calls");
            migrationBuilder.CreateTable(
                name: "Calls",
                columns: table => new
                {
                    CallId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromId = table.Column<int>(type: "integer", nullable: false),
                    ToId = table.Column<int>(type: "integer", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "timestamp", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calls", x => x.CallId);
                    table.ForeignKey(
                        name: "FK_Calls_Contacts_FromId",
                        column: x => x.FromId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calls_Contacts_ToId",
                        column: x => x.ToId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Calls_FromId",
                table: "Calls",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_ToId",
                table: "Calls",
                column: "ToId");
        }
    }
}
