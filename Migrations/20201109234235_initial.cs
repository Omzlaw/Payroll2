using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectConnectDB.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    GrossSalary = table.Column<double>(nullable: false),
                    NetSalary = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Bonuses = table.Column<double>(nullable: false),
                    Pension = table.Column<double>(nullable: false),
                    TotalWorkingHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
