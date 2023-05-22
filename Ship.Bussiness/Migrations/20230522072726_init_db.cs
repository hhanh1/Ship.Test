using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ship.Bussiness.Migrations
{
    public partial class init_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Velocity = table.Column<double>(type: "double precision", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ports",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Latitude", "Longitude", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("117d495d-e3e3-4cd7-a543-814eae32a08e"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4043), 18.525324779000002, -72.284319377000003, "Port 9", null, null },
                    { new Guid("27823f1a-85d0-4260-9e97-3ebba3d9e90b"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(3953), 18.531520565000001, -72.339592944000003, "Port 0", null, null },
                    { new Guid("68e7d233-a727-43f9-87fb-5383f6390037"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4040), 18.552284645, -72.282348534999997, "Port 8", null, null },
                    { new Guid("7ef940bd-0736-480e-bb33-4d8239774da0"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4033), 18.628338124999999, -72.253912600000007, "Port 5", null, null },
                    { new Guid("93516d1a-d299-4884-b018-037d36d06e40"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4009), 18.574607041, -72.251296693, "Port 1", null, null },
                    { new Guid("9f9e7e11-f863-4ca7-84f1-47aa237c14c7"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4029), 18.614120378999999, -72.252213038999997, "Port 4", null, null },
                    { new Guid("a2a267ef-db40-451f-aad7-f1921319172c"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4021), 18.562286259, -72.329204395000005, "Port 3", null, null },
                    { new Guid("a42e4ee7-1fee-493a-87d1-38d4cdb9a12f"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4038), 18.536163824999999, -72.306846309999997, "Port 7", null, null },
                    { new Guid("ca3db14b-4d78-4b04-b683-da1dad29b681"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4011), 18.516792489, -72.323154474000006, "Port 2", null, null },
                    { new Guid("cd3ab506-2a73-4204-8f61-479c4f71fcc9"), null, new DateTime(2023, 5, 22, 7, 27, 26, 259, DateTimeKind.Utc).AddTicks(4036), 18.556417154000002, -72.233779733000006, "Port 6", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ports");

            migrationBuilder.DropTable(
                name: "Ships");
        }
    }
}
