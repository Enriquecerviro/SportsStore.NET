﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    #region partial class Initial : Migration

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class Initial : Migration
    {
        /// <summary>
        /// <para>
        /// Builds the operations that will migrate the database 'up'.
        /// </para>
        /// <para>
        /// That is, builds the operations that will take the database from the state left in by the
        /// previous migration so that it is up-to-date with regard to this migration.
        /// </para>
        /// <para>
        /// This method must be overridden in each class the inherits from <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Migration" />.
        /// </para>
        /// </summary>
        /// <param name="migrationBuilder">The <see cref="T:Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder" /> that will build the operations.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });
        }

        /// <summary>
        /// <para>
        /// Builds the operations that will migrate the database 'down'.
        /// </para>
        /// <para>
        /// That is, builds the operations that will take the database from the state left in by
        /// this migration so that it returns to the state that it was in before this migration was applied.
        /// </para>
        /// <para>
        /// This method must be overridden in each class the inherits from <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Migration" /> if
        /// both 'up' and 'down' migrations are to be supported. If it is not overridden, then calling it
        /// will throw and it will not be possible to migrate in the 'down' direction.
        /// </para>
        /// </summary>
        /// <param name="migrationBuilder">The <see cref="T:Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder" /> that will build the operations.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }

    #endregion partial class Initial : Migration
}