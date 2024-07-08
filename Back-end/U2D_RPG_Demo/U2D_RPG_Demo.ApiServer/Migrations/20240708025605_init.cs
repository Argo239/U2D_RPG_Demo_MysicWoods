using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace U2D_RPG_Demo.ApiServer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new {
                    UID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10000000, 1"),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Aa123456"),
                    Name = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true, defaultValue: "nobody"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    HasDelete = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DeleteTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table => {
                    table.PrimaryKey("PK__UserInfo__C5B196020D417FD6", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAttributes",
                columns: table => new {
                    PAID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    Experience = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    HP = table.Column<double>(type: "float", nullable: true, defaultValue: 100.0),
                    MaxHP = table.Column<double>(type: "float", nullable: true, defaultValue: 100.0),
                    MP = table.Column<double>(type: "float", nullable: true, defaultValue: 100.0),
                    MaxMP = table.Column<double>(type: "float", nullable: true, defaultValue: 100.0),
                    ATK = table.Column<double>(type: "float", nullable: true, defaultValue: 5.0),
                    DEF = table.Column<double>(type: "float", nullable: true, defaultValue: 2.0),
                    DR = table.Column<double>(type: "float", nullable: true, defaultValue: 0.10000000000000001),
                    SPD = table.Column<double>(type: "float", nullable: true, defaultValue: 5.0),
                    SPD_MULT = table.Column<double>(type: "float", nullable: true, defaultValue: 0.0)
                },
                constraints: table => {
                    table.PrimaryKey("PK__PlayerAt__5986FD6D38FACDC1", x => x.PAID);
                    table.ForeignKey(
                        name: "FK__PlayerAttri__UID__09A971A2",
                        column: x => x.UID,
                        principalTable: "UserInfo",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAttributes_UID",
                table: "PlayerAttributes",
                column: "UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "PlayerAttributes");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
