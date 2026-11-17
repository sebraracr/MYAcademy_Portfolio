using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYAcademy_Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class mig19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE HeroSections SET CvUrl = '/cv/Sara_Ebrar_Acar_CV-1.pdf'");
        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
