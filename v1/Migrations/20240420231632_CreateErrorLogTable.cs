using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace v1.Migrations
{
    /// <inheritdoc />
    public partial class CreateErrorLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE error_log (
                    id SERIAL PRIMARY KEY,
                    message TEXT NOT NULL,
                    stack_trace TEXT,
                    log_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );
            ");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS ErrorLog;");
        }
    }
}
