using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CVI.Infrastructure.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ALARM_NOTIFICATIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlarmType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PonPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ONTNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClearTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClearDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALARM_NOTIFICATIONS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CL_CLIENT_STATUS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsCommentMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_CLIENT_STATUS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CL_CONCLUSIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_CONCLUSIONS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CL_LOCALISATIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_LOCALISATIONS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CL_SUB_CAUSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_SUB_CAUSES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "INT_INTERVENTION_PMS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PMName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INT_INTERVENTION_PMS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "INT_INTERVENTION_TYPES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterventionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INT_INTERVENTION_TYPES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NOT_EQUIPEMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SrvId = table.Column<int>(type: "int", nullable: false),
                    NomNro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PonPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomPm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumONT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ONTPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdOSS = table.Column<int>(type: "int", nullable: false),
                    LinkStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOT_EQUIPEMENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PH_PHOTO_RESULTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientNumber = table.Column<int>(type: "int", nullable: false),
                    DownClientNumber = table.Column<int>(type: "int", nullable: false),
                    NOKClientNumber = table.Column<int>(type: "int", nullable: false),
                    AverageDownTime = table.Column<long>(type: "bigint", nullable: false),
                    LowerTo20DownNumber = table.Column<int>(type: "int", nullable: false),
                    GreaterTo20DownNumber = table.Column<int>(type: "int", nullable: false),
                    OperationStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SwitchedOffNumber = table.Column<int>(type: "int", nullable: false),
                    ListDGOFF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PH_PHOTO_RESULTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "REF_OPERATORS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REF_OPERATORS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "REF_USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REF_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LOC_SUB_CAUSES",
                columns: table => new
                {
                    LocalizationsID = table.Column<int>(type: "int", nullable: false),
                    SubCausesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOC_SUB_CAUSES", x => new { x.LocalizationsID, x.SubCausesID });
                    table.ForeignKey(
                        name: "FK_LOC_SUB_CAUSES_CL_LOCALISATIONS_LocalizationsID",
                        column: x => x.LocalizationsID,
                        principalTable: "CL_LOCALISATIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LOC_SUB_CAUSES_CL_SUB_CAUSES_SubCausesID",
                        column: x => x.SubCausesID,
                        principalTable: "CL_SUB_CAUSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INT_INTERVENTIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterventionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InterventionPto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnloadedCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterventionTypeId = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MutualisationPointId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INT_INTERVENTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INT_INTERVENTIONS_INT_INTERVENTION_PMS_MutualisationPointId",
                        column: x => x.MutualisationPointId,
                        principalTable: "INT_INTERVENTION_PMS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INT_INTERVENTIONS_INT_INTERVENTION_TYPES_InterventionTypeId",
                        column: x => x.InterventionTypeId,
                        principalTable: "INT_INTERVENTION_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INT_INTERVENTIONS_REF_OPERATORS_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "REF_OPERATORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INT_INTERVENTIONS_REF_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "REF_USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CL_CLIENT_RESULTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SrvID = table.Column<int>(type: "int", nullable: false),
                    ONTPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInfos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstCallComent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDown = table.Column<bool>(type: "bit", nullable: false),
                    StatusFirstCallId = table.Column<int>(type: "int", nullable: true),
                    LocalizationId = table.Column<int>(type: "int", nullable: true),
                    ConclusionId = table.Column<int>(type: "int", nullable: true),
                    SubCauseId = table.Column<int>(type: "int", nullable: true),
                    InterventionId = table.Column<int>(type: "int", nullable: false),
                    ClientStep = table.Column<int>(type: "int", nullable: false),
                    IdOSS = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_CLIENT_RESULTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CL_CLIENT_RESULTS_CL_CLIENT_STATUS_StatusFirstCallId",
                        column: x => x.StatusFirstCallId,
                        principalTable: "CL_CLIENT_STATUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CL_CLIENT_RESULTS_CL_CONCLUSIONS_ConclusionId",
                        column: x => x.ConclusionId,
                        principalTable: "CL_CONCLUSIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CL_CLIENT_RESULTS_CL_LOCALISATIONS_LocalizationId",
                        column: x => x.LocalizationId,
                        principalTable: "CL_LOCALISATIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CL_CLIENT_RESULTS_CL_SUB_CAUSES_SubCauseId",
                        column: x => x.SubCauseId,
                        principalTable: "CL_SUB_CAUSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CL_CLIENT_RESULTS_INT_INTERVENTIONS_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "INT_INTERVENTIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PH_PHOTOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterventionId = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    PhotoResultID = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PH_PHOTOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PH_PHOTOS_INT_INTERVENTIONS_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "INT_INTERVENTIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PH_PHOTOS_PH_PHOTO_RESULTS_PhotoResultID",
                        column: x => x.PhotoResultID,
                        principalTable: "PH_PHOTO_RESULTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PH_PHOTOS_REF_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "REF_USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PH_ALARMS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlarmType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ONTNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClearTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhotoId = table.Column<int>(type: "int", nullable: true),
                    ClientResultID = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PH_ALARMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PH_ALARMS_CL_CLIENT_RESULTS_ClientResultID",
                        column: x => x.ClientResultID,
                        principalTable: "CL_CLIENT_RESULTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PH_ALARMS_PH_PHOTOS_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "PH_PHOTOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ALARM_NOTIFICATIONS",
                columns: new[] { "ID", "Address", "AlarmType", "ClearDateTime", "ClearTime", "CreationDate", "NotificationId", "ONTNr", "PonPath", "StartDateTime", "StartTime" },
                values: new object[,]
                {
                    { 1, "OLT3404:0-0-1-4", "INACT", new DateTime(2021, 3, 8, 11, 44, 47, 0, DateTimeKind.Unspecified), "2021-03-08 11:44:47.0000000", new DateTime(2021, 3, 8, 13, 58, 36, 591, DateTimeKind.Unspecified).AddTicks(2869), "AMS:379039455", "0", "OLT3404-LT1-PON4", new DateTime(2021, 3, 8, 11, 44, 17, 0, DateTimeKind.Unspecified), "2021-03-08 11:44:17.0000000" },
                    { 2, "OLT3404:0-0-1-4", "INACT", new DateTime(2021, 3, 8, 11, 44, 47, 0, DateTimeKind.Unspecified), "2021-03-08 11:44:47.0000000", new DateTime(2021, 3, 8, 13, 58, 36, 591, DateTimeKind.Unspecified).AddTicks(2869), "AMS:379040690", "1", "OLT3404-LT1-PON4", new DateTime(2021, 3, 8, 11, 44, 17, 0, DateTimeKind.Unspecified), "2021-03-08 11:44:17.0000000" },
                    { 3, "OLT3404:0-0-1-4", "INACT", new DateTime(2021, 3, 8, 11, 44, 47, 0, DateTimeKind.Unspecified), "2021-03-08 11:44:47.0000000", new DateTime(2021, 3, 8, 13, 58, 36, 591, DateTimeKind.Unspecified).AddTicks(2869), "AMS:379040690", "2", "OLT3404-LT1-PON4", new DateTime(2021, 3, 8, 11, 44, 17, 0, DateTimeKind.Unspecified), "2021-03-08 11:44:17.0000000" },
                    { 4, "OLT3404:0-0-1-4", "INACT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2021, 3, 8, 13, 58, 36, 591, DateTimeKind.Unspecified).AddTicks(2869), "AMS:379040690", "3", "OLT3404-LT1-PON4", new DateTime(2021, 3, 8, 11, 44, 17, 0, DateTimeKind.Unspecified), "2021-03-08 11:44:17.0000000" },
                    { 5, "OLT3404:0-0-1-4", "INACT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2021, 3, 8, 13, 58, 36, 591, DateTimeKind.Unspecified).AddTicks(2869), "AMS:379040690", "4", "OLT3404-LT1-PON4", new DateTime(2021, 3, 8, 11, 44, 17, 0, DateTimeKind.Unspecified), "2021-03-08 11:44:17.0000000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CL_CLIENT_RESULTS_ConclusionId",
                table: "CL_CLIENT_RESULTS",
                column: "ConclusionId");

            migrationBuilder.CreateIndex(
                name: "IX_CL_CLIENT_RESULTS_InterventionId",
                table: "CL_CLIENT_RESULTS",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_CL_CLIENT_RESULTS_LocalizationId",
                table: "CL_CLIENT_RESULTS",
                column: "LocalizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CL_CLIENT_RESULTS_StatusFirstCallId",
                table: "CL_CLIENT_RESULTS",
                column: "StatusFirstCallId");

            migrationBuilder.CreateIndex(
                name: "IX_CL_CLIENT_RESULTS_SubCauseId",
                table: "CL_CLIENT_RESULTS",
                column: "SubCauseId");

            migrationBuilder.CreateIndex(
                name: "IX_INT_INTERVENTIONS_InterventionTypeId",
                table: "INT_INTERVENTIONS",
                column: "InterventionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_INT_INTERVENTIONS_MutualisationPointId",
                table: "INT_INTERVENTIONS",
                column: "MutualisationPointId");

            migrationBuilder.CreateIndex(
                name: "IX_INT_INTERVENTIONS_OperatorId",
                table: "INT_INTERVENTIONS",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_INT_INTERVENTIONS_UserId",
                table: "INT_INTERVENTIONS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LOC_SUB_CAUSES_SubCausesID",
                table: "LOC_SUB_CAUSES",
                column: "SubCausesID");

            migrationBuilder.CreateIndex(
                name: "IX_PH_ALARMS_ClientResultID",
                table: "PH_ALARMS",
                column: "ClientResultID");

            migrationBuilder.CreateIndex(
                name: "IX_PH_ALARMS_PhotoId",
                table: "PH_ALARMS",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PH_PHOTOS_InterventionId",
                table: "PH_PHOTOS",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_PH_PHOTOS_PhotoResultID",
                table: "PH_PHOTOS",
                column: "PhotoResultID");

            migrationBuilder.CreateIndex(
                name: "IX_PH_PHOTOS_UserID",
                table: "PH_PHOTOS",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALARM_NOTIFICATIONS");

            migrationBuilder.DropTable(
                name: "LOC_SUB_CAUSES");

            migrationBuilder.DropTable(
                name: "NOT_EQUIPEMENTS");

            migrationBuilder.DropTable(
                name: "PH_ALARMS");

            migrationBuilder.DropTable(
                name: "CL_CLIENT_RESULTS");

            migrationBuilder.DropTable(
                name: "PH_PHOTOS");

            migrationBuilder.DropTable(
                name: "CL_CLIENT_STATUS");

            migrationBuilder.DropTable(
                name: "CL_CONCLUSIONS");

            migrationBuilder.DropTable(
                name: "CL_LOCALISATIONS");

            migrationBuilder.DropTable(
                name: "CL_SUB_CAUSES");

            migrationBuilder.DropTable(
                name: "INT_INTERVENTIONS");

            migrationBuilder.DropTable(
                name: "PH_PHOTO_RESULTS");

            migrationBuilder.DropTable(
                name: "INT_INTERVENTION_PMS");

            migrationBuilder.DropTable(
                name: "INT_INTERVENTION_TYPES");

            migrationBuilder.DropTable(
                name: "REF_OPERATORS");

            migrationBuilder.DropTable(
                name: "REF_USERS");
        }
    }
}
