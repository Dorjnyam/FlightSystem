using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aircraft",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AircraftCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    AircraftType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TotalSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircraft", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    WorkStationId = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PassportNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Nationality = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AircraftId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeatNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Row = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Column = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Class = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsWindowSeat = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsAisleSeat = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsEmergencyExit = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Aircraft_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNumber = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    AircraftId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartureAirport = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ArrivalAirport = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ScheduledDeparture = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ScheduledArrival = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActualDeparture = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ActualArrival = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    GateNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    CheckinOpenTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckinCloseTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BoardingTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByEmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Aircraft_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Employees_CreatedByEmployeeId",
                        column: x => x.CreatedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightPassengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    PassengerId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookingReference = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsCheckedIn = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CheckinTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CheckinByEmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    SpecialRequests = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    BaggageInfo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightPassengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightPassengers_Employees_CheckinByEmployeeId",
                        column: x => x.CheckinByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightPassengers_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightPassengers_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightPassengerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeatId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AssignedByEmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatAssignments_Employees_AssignedByEmployeeId",
                        column: x => x.AssignedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeatAssignments_FlightPassengers_FlightPassengerId",
                        column: x => x.FlightPassengerId,
                        principalTable: "FlightPassengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatAssignments_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoardingPasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightPassengerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeatAssignmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    BoardingPassCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    QRCode = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    BarcodeData = table.Column<string>(type: "TEXT", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IssuedByEmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    BoardingTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    BoardingByEmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsBoardingComplete = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Gate = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardingPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardingPasses_Employees_BoardingByEmployeeId",
                        column: x => x.BoardingByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardingPasses_Employees_IssuedByEmployeeId",
                        column: x => x.IssuedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardingPasses_FlightPassengers_FlightPassengerId",
                        column: x => x.FlightPassengerId,
                        principalTable: "FlightPassengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardingPasses_SeatAssignments_SeatAssignmentId",
                        column: x => x.SeatAssignmentId,
                        principalTable: "SeatAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_AircraftCode",
                table: "Aircraft",
                column: "AircraftCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_AircraftType",
                table: "Aircraft",
                column: "AircraftType");

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_IsActive",
                table: "Aircraft",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_BoardingByEmployeeId",
                table: "BoardingPasses",
                column: "BoardingByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_BoardingPassCode",
                table: "BoardingPasses",
                column: "BoardingPassCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_FlightPassengerId",
                table: "BoardingPasses",
                column: "FlightPassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_IsBoardingComplete",
                table: "BoardingPasses",
                column: "IsBoardingComplete");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_IssuedAt",
                table: "BoardingPasses",
                column: "IssuedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_IssuedByEmployeeId",
                table: "BoardingPasses",
                column: "IssuedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_SeatAssignmentId",
                table: "BoardingPasses",
                column: "SeatAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeCode",
                table: "Employees",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IsActive",
                table: "Employees",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Role",
                table: "Employees",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengers_BookingReference",
                table: "FlightPassengers",
                column: "BookingReference");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengers_CheckinByEmployeeId",
                table: "FlightPassengers",
                column: "CheckinByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengers_CheckinTime",
                table: "FlightPassengers",
                column: "CheckinTime");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengers_FlightId_PassengerId",
                table: "FlightPassengers",
                columns: new[] { "FlightId", "PassengerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengers_IsCheckedIn",
                table: "FlightPassengers",
                column: "IsCheckedIn");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengers_PassengerId",
                table: "FlightPassengers",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AircraftId",
                table: "Flights",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CreatedByEmployeeId",
                table: "Flights",
                column: "CreatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FlightNumber",
                table: "Flights",
                column: "FlightNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ScheduledDeparture",
                table: "Flights",
                column: "ScheduledDeparture");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Status",
                table: "Flights",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_Email",
                table: "Passengers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_Nationality",
                table: "Passengers",
                column: "Nationality");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_PassportNumber",
                table: "Passengers",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_Type",
                table: "Passengers",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAssignments_AssignedAt",
                table: "SeatAssignments",
                column: "AssignedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAssignments_AssignedByEmployeeId",
                table: "SeatAssignments",
                column: "AssignedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAssignments_FlightPassengerId",
                table: "SeatAssignments",
                column: "FlightPassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAssignments_IsActive",
                table: "SeatAssignments",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAssignments_SeatId_FlightPassengerId",
                table: "SeatAssignments",
                columns: new[] { "SeatId", "FlightPassengerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_AircraftId",
                table: "Seats",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_AircraftId_SeatNumber",
                table: "Seats",
                columns: new[] { "AircraftId", "SeatNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_Class",
                table: "Seats",
                column: "Class");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_IsActive",
                table: "Seats",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardingPasses");

            migrationBuilder.DropTable(
                name: "SeatAssignments");

            migrationBuilder.DropTable(
                name: "FlightPassengers");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Aircraft");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
