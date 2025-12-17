using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SelfLearning.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "train_master",
            columns: table => new
            {
                train_id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),

                train_no = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),

                train_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),

                last_updated = table.Column<DateTime>(
                    type: "timestamp with time zone",
                    nullable: false,
                    defaultValueSql: "now()"
                )
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_train_master", x => x.train_id);
            });

            migrationBuilder.CreateIndex(
                name: "IX_train_master_train_no",
                table: "train_master",
                column: "train_no",
                unique: true
            );

            migrationBuilder.CreateTable(
            name: "station_master",
            columns: table => new
            {
                station_id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),

                station_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),

                station_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_station_master", x => x.station_id);
            });

            migrationBuilder.CreateIndex(
                name: "IX_station_master_station_code",
                table: "station_master",
                column: "station_code",
                unique: true,
                filter: "\"station_code\" IS NOT NULL"
            );

            migrationBuilder.CreateTable(
            name: "route_stop",
            columns: table => new
            {
                route_stop_id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),

                train_id = table.Column<int>(type: "integer", nullable: false),

                station_id = table.Column<int>(type: "integer", nullable: false),

                seq_no = table.Column<int>(type: "integer", nullable: false),

                arrival_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),

                departure_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_route_stop", x => x.route_stop_id);

                table.ForeignKey(
                    name: "FK_route_stop_train_master_train_id",
                    column: x => x.train_id,
                    principalTable: "train_master",
                    principalColumn: "train_id",
                    onDelete: ReferentialAction.Restrict);

                table.ForeignKey(
                    name: "FK_route_stop_station_master_station_id",
                    column: x => x.station_id,
                    principalTable: "station_master",
                    principalColumn: "station_id",
                    onDelete: ReferentialAction.Restrict);
            });
            migrationBuilder.CreateIndex(
            name: "IX_route_stop_train_id_seq_no",
            table: "route_stop",
            columns: new[] { "train_id", "seq_no" },
            unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_route_stop_station_id",
                table: "route_stop",
                column: "station_id"
            );

            migrationBuilder.CreateTable(
            name: "passenger_travel_share",
            columns: table => new
            {
                share_id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),

                passenger_id = table.Column<int>(type: "integer", nullable: true),

                pnr = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),

                train_id = table.Column<int>(type: "integer", nullable: false),

                travel_date = table.Column<DateOnly>(type: "date", nullable: false),

                message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),

                created_at = table.Column<DateTime>(
                    type: "timestamp with time zone",
                    nullable: false,
                    defaultValueSql: "now()"
                )
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_passenger_travel_share", x => x.share_id);

                table.ForeignKey(
                    name: "FK_passenger_travel_share_train_master_train_id",
                    column: x => x.train_id,
                    principalTable: "train_master",
                    principalColumn: "train_id",
                    onDelete: ReferentialAction.Restrict);
            });
            migrationBuilder.CreateIndex(
                name: "IX_passenger_travel_share_train_id",
                table: "passenger_travel_share",
                column: "train_id"
            );

            migrationBuilder.CreateTable(
            name: "passenger_profile",
            columns: table => new
            {
                profile_id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),

                share_id = table.Column<int>(type: "integer", nullable: false),

                age = table.Column<int>(type: "integer", nullable: true),

                gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),

                interests = table.Column<string>(type: "text", nullable: true),

                travel_purpose = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),

                preference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),

                created_at = table.Column<DateTime>(
                    type: "timestamp with time zone",
                    nullable: false,
                    defaultValueSql: "now()"
                )
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_passenger_profile", x => x.profile_id);

                table.ForeignKey(
                    name: "FK_passenger_profile_passenger_travel_share_share_id",
                    column: x => x.share_id,
                    principalTable: "passenger_travel_share",
                    principalColumn: "share_id",
                    onDelete: ReferentialAction.Cascade);
            });
            migrationBuilder.CreateIndex(
                name: "IX_passenger_profile_share_id",
                table: "passenger_profile",
                column: "share_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("passenger_profile");
            migrationBuilder.DropTable("passenger_travel_share");
            migrationBuilder.DropTable("route_stop");
            migrationBuilder.DropTable("station_master");
            migrationBuilder.DropTable("train_master");
        }
    }
}
