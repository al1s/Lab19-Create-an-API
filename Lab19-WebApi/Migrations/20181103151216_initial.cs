using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab19WebApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todolists",
                columns: table => new
                {
                    TodolistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todolists", x => x.TodolistId);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    TodoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Task = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    Finished = table.Column<bool>(nullable: false),
                    TodolistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_Todos_Todolists_TodolistId",
                        column: x => x.TodolistId,
                        principalTable: "Todolists",
                        principalColumn: "TodolistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Todolists",
                columns: new[] { "TodolistId", "Name" },
                values: new object[] { 1, "Lab19" });

            migrationBuilder.InsertData(
                table: "Todolists",
                columns: new[] { "TodolistId", "Name" },
                values: new object[] { 2, "Family" });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "TodoId", "Details", "Finished", "Task", "TodolistId" },
                values: new object[,]
                {
                    { 1, null, false, "Make DB migration", 1 },
                    { 2, null, false, "Implement controllers", 1 },
                    { 3, null, false, "Write tests", 1 },
                    { 4, null, false, "Play with kids", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodolistId",
                table: "Todos",
                column: "TodolistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Todolists");
        }
    }
}
