using Microsoft.EntityFrameworkCore.Migrations;

namespace AssociationWebApp.Migrations
{
    public partial class mtom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "AK_Company",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Association",
                table: "Association");

            migrationBuilder.DropIndex(
                name: "AK_Associated",
                table: "Associated");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Company",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "cnpj",
                table: "Company",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Associated",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                table: "Associated",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 11);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Association",
                table: "Association",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Association_associatedId",
                table: "Association",
                column: "associatedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Association",
                table: "Association");

            migrationBuilder.DropIndex(
                name: "IX_Association_associatedId",
                table: "Association");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Company",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "cnpj",
                table: "Company",
                unicode: false,
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Associated",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                table: "Associated",
                unicode: false,
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Association",
                table: "Association",
                columns: new[] { "associatedId", "companyId", "id" });

            migrationBuilder.CreateIndex(
                name: "AK_Company",
                table: "Company",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Associated",
                table: "Associated",
                column: "cpf",
                unique: true);
        }
    }
}
