using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonsWebApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DateOfBirth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateOfBirth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirstNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LastNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parent2Child",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parent2Child", x => x.Id);
                    table.CheckConstraint("ValidParent2Child", "ParentId <> ChildId");
                });

            migrationBuilder.CreateTable(
                name: "Patronymics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patronymic = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patronymics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonGuid = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastNameId = table.Column<int>(type: "int", nullable: false),
                    FirstNameId = table.Column<int>(type: "int", nullable: false),
                    PatronymicId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirthId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalData_DateOfBirth_DateOfBirthId",
                        column: x => x.DateOfBirthId,
                        principalTable: "DateOfBirth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalData_FirstNames_FirstNameId",
                        column: x => x.FirstNameId,
                        principalTable: "FirstNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalData_LastNames_LastNameId",
                        column: x => x.LastNameId,
                        principalTable: "LastNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalData_Patronymics_PatronymicId",
                        column: x => x.PatronymicId,
                        principalTable: "Patronymics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person2PersonalData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PersonalDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person2PersonalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person2PersonalData_PersonalData_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person2PersonalData_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateOfBirth_DateOfBirth",
                table: "DateOfBirth",
                column: "DateOfBirth",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FirstNames_FirstName",
                table: "FirstNames",
                column: "FirstName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LastNames_LastName",
                table: "LastNames",
                column: "LastName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parent2Child_ParentId_ChildId",
                table: "Parent2Child",
                columns: new[] { "ParentId", "ChildId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patronymics_Patronymic",
                table: "Patronymics",
                column: "Patronymic",
                unique: true,
                filter: "[Patronymic] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person2PersonalData_PersonalDataId",
                table: "Person2PersonalData",
                column: "PersonalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Person2PersonalData_PersonId_PersonalDataId",
                table: "Person2PersonalData",
                columns: new[] { "PersonId", "PersonalDataId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_DateOfBirthId",
                table: "PersonalData",
                column: "DateOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_FirstNameId",
                table: "PersonalData",
                column: "FirstNameId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_LastNameId_FirstNameId_PatronymicId_DateOfBirthId",
                table: "PersonalData",
                columns: new[] { "LastNameId", "FirstNameId", "PatronymicId", "DateOfBirthId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_PatronymicId",
                table: "PersonalData",
                column: "PatronymicId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonGuid",
                table: "Persons",
                column: "PersonGuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parent2Child");

            migrationBuilder.DropTable(
                name: "Person2PersonalData");

            migrationBuilder.DropTable(
                name: "PersonalData");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "DateOfBirth");

            migrationBuilder.DropTable(
                name: "FirstNames");

            migrationBuilder.DropTable(
                name: "LastNames");

            migrationBuilder.DropTable(
                name: "Patronymics");
        }
    }
}
