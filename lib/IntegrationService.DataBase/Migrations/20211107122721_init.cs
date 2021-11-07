using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationService.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageFormats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scheme = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageFormats", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MessageFormats",
                columns: new[] { "Id", "Name", "Scheme" },
                values: new object[] { 1000L, "Upackage", "{\r\n	\"definitions\": {},\r\n	\"$schema\": \"http://json-schema.org/draft-07/schema#\", \r\n	\"$id\": \"https://example.com/object1636286636.json\", \r\n	\"title\": \"Upackage format\", \r\n	\"type\": \"object\",\r\n	\"required\": [\r\n		\"integrationId\",\r\n		\"content\"\r\n	],\r\n	\"properties\": {\r\n		\"integrationId\": {\r\n			\"$id\": \"#root/integrationId\", \r\n			\"title\": \"Integrationid\", \r\n			\"type\": \"string\",\r\n			\"default\": \"\",\r\n			\"examples\": [\r\n				\"26b13510-d202-4734-bb12-10433a950f35\"\r\n			]\r\n		},\r\n		\"content\": {\r\n			\"$id\": \"#root/content\", \r\n			\"title\": \"Content\", \r\n			\"type\": \"string\",\r\n			\"default\": \"\",\r\n			\"examples\": [\r\n				\"@@JJKJHHU#####\"\r\n			]\r\n		}\r\n	}\r\n}" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageFormats");
        }
    }
}
