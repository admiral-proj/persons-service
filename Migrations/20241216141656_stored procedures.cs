using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonsWebApi.Migrations
{
    public partial class storedprocedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"EXEC ('CREATE PROCEDURE AddLastNameProc
                    @lastName nvarchar(25)
                AS
                    DECLARE @id int

                    IF NOT EXISTS(SELECT 1 FROM LastNames WHERE(@lastName=LastName))
	                BEGIN
		                INSERT INTO LastNames(LastName) VALUES(@lastName);
		                SET @id=@@IDENTITY;
	                END

	                ELSE SET @id=(SELECT TOP 1 Id FROM LastNames WHERE(@lastName=LastName));

                RETURN @id;')");

            migrationBuilder.Sql(
                @"EXEC ('CREATE PROCEDURE AddFirstNameProc
                    @firstName nvarchar(25)
                AS
                    DECLARE @id int

                    IF NOT EXISTS(SELECT 1 FROM FirstNames WHERE(@firstName=FirstName))
	                BEGIN
		                INSERT INTO FirstNames(FirstName) VALUES(@firstName);
		                SET @id=@@IDENTITY;
	                END

	                ELSE SET @id=(SELECT TOP 1 Id FROM FirstNames WHERE(@firstName=FirstName));

                RETURN @id;')");

            migrationBuilder.Sql(
                @"EXEC ('CREATE PROCEDURE AddPatronymicProc
                    @patronymic nvarchar(25)
                AS
                    DECLARE @id int

                    IF NOT EXISTS(SELECT 1 FROM Patronymics WHERE(@patronymic=Patronymic))
	                BEGIN
		                INSERT INTO Patronymics(Patronymic) VALUES(@patronymic);
		                SET @id=@@IDENTITY;
	                END

	                ELSE SET @id=(SELECT TOP 1 Id FROM Patronymics WHERE(@patronymic=Patronymic));

                RETURN @id;')");

            migrationBuilder.Sql(
                @"EXEC ('CREATE PROCEDURE AddDateOfBirthProc
                    @dateOfBirth date
                AS
                    DECLARE @id int

                    IF NOT EXISTS(SELECT 1 FROM DateOfBirth WHERE(@dateOfBirth=DateOfBirth))
	                BEGIN
		                INSERT INTO DateOfBirth(DateOfBirth) VALUES(@dateOfBirth);
		                SET @id=@@IDENTITY;
	                END

	                ELSE SET @id=(SELECT TOP 1 Id FROM DateOfBirth WHERE(@dateOfBirth=DateOfBirth));

                RETURN @id;')");

            migrationBuilder.Sql(
                @"EXEC ('CREATE PROCEDURE AddPersonalDataProc
                    @lastName nvarchar(25),
                    @firstName nvarchar(25),
                    @patronymic nvarchar(25),
                    @dateOfBirth date
                AS
                    DECLARE @lastNameId int, @firstNameId int, @patronymicId int, @dateOfBirthId int, @id int

                    EXEC @lastNameId = AddLastNameProc @lastName=@lastName;
	                EXEC @firstNameId = AddFirstNameProc @firstName=@firstName;
	                EXEC @patronymicId = AddPatronymicProc @patronymic=@patronymic;
	                EXEC @dateOfBirthId = AddDateOfBirthProc @dateOfBirth=@dateOfBirth;

                    IF(NOT EXISTS(SELECT * FROM PersonalData WHERE (LastNameId=@lastNameId AND FirstNameId=@firstNameId AND PatronymicId=@patronymicId AND DateOfBirthId=@dateOfBirthId)))
	                BEGIN
		                INSERT INTO PersonalData(LastNameId, FirstNameId, PatronymicId, DateOfBirthId) VALUES(@lastNameId, @firstNameId, @patronymicId, @dateOfBirthId);
		                SET @id = @@IDENTITY;
	                END

	                ELSE SET @id = (SELECT MIN(Id) FROM PersonalData WHERE (LastNameId=@lastNameId AND FirstNameId=@firstNameId AND PatronymicId=@patronymicId AND DateOfBirthId=@dateOfBirthId));

                RETURN @id;')");

            migrationBuilder.Sql(
                @"EXEC ('CREATE PROCEDURE AddPersonProc
                    @lastName nvarchar(25),
                    @firstName nvarchar(25),
                    @patronymic nvarchar(25),
                    @dateOfBirth date,
                    @id int out
                AS
                    DECLARE @personalDataId int

                    EXEC @personalDataId = AddPersonalDataProc @lastName=@lastName, @firstName=@firstName, @patronymic=@patronymic, @dateOfBirth=@dateOfBirth;

                    INSERT INTO Persons(PersonGuid) VALUES(NEWID());
                    SET @id = @@IDENTITY;

                    INSERT INTO Person2PersonalData(PersonId, PersonalDataId) Values(@id, @personalDataId)

                    RETURN @id;')"
                );

            migrationBuilder.Sql(
                @"EXEC ('CREATE VIEW [dbo].[PersonsView]
	                AS SELECT Persons.Id, Persons.PersonGuid, LastNames.LastName, FirstNames.FirstName, Patronymics.Patronymic, DateOfBirth.DateOfBirth
	                FROM ((Persons INNER JOIN Person2PersonalData ON Persons.Id=Person2PersonalData.PersonId)
		                INNER JOIN ((((PersonalData INNER JOIN LastNames ON LastNames.Id=PersonalData.LastNameId)
		                INNER JOIN FirstNames ON FirstNames.Id=PersonalData.FirstNameId) INNER JOIN Patronymics ON Patronymics.Id=PersonalData.PatronymicId)
		                INNER JOIN DateOfBirth ON DateOfBirth.Id=PersonalData.DateOfBirthId) ON PersonalData.Id=Person2PersonalData.PersonalDataId);')"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC ('DROP PROCEDURE AddLastNameProc')");
            migrationBuilder.Sql(@"EXEC ('DROP PROCEDURE AddFirstNameProc')");
            migrationBuilder.Sql(@"EXEC ('DROP PROCEDURE AddPatronymicProc')");
            migrationBuilder.Sql(@"EXEC ('DROP PROCEDURE AddDateOfBirthProc')");
            migrationBuilder.Sql(@"EXEC ('DROP PROCEDURE AddPersonalDataProc')");
            migrationBuilder.Sql(@"EXEC ('DROP PROCEDURE AddPersonProc')");
            migrationBuilder.Sql(@"EXEC ('DROP VIEW PersonsView')");
        }
    }
}
