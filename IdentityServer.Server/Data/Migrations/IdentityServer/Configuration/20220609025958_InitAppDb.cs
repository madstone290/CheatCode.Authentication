using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Server.Data.Migrations.IdentityServer.Configuration
{
    public partial class InitAppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ApiResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AllowedAccessTokenSigningAlgorithms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ApiResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ApiScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Emphasize = table.Column<bool>(type: "bit", nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ApiScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProtocolType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(type: "bit", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(type: "bit", nullable: false),
                    AllowRememberConsent = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(type: "bit", nullable: false),
                    RequirePkce = table.Column<bool>(type: "bit", nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(type: "bit", nullable: false),
                    RequireRequestObject = table.Column<bool>(type: "bit", nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(type: "bit", nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(type: "bit", nullable: false),
                    BackChannelLogoutUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(type: "bit", nullable: false),
                    AllowOfflineAccess = table.Column<bool>(type: "bit", nullable: false),
                    IdentityTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    AllowedIdentityTokenSigningAlgorithms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccessTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(type: "int", nullable: false),
                    ConsentLifetime = table.Column<int>(type: "int", nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    RefreshTokenUsage = table.Column<int>(type: "int", nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(type: "bit", nullable: false),
                    RefreshTokenExpiration = table.Column<int>(type: "int", nullable: false),
                    AccessTokenType = table.Column<int>(type: "int", nullable: false),
                    EnableLocalLogin = table.Column<bool>(type: "bit", nullable: false),
                    IncludeJwtId = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(type: "bit", nullable: false),
                    ClientClaimsPrefix = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserSsoLifetime = table.Column<int>(type: "int", nullable: true),
                    UserCodeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(type: "int", nullable: false),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__IdentityResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Emphasize = table.Column<bool>(type: "bit", nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__IdentityResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ApiResourceClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ApiResourceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ApiResourceClaims___IdentityServerConfiguration__ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "__IdentityServerConfiguration__ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ApiResourceProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ApiResourceProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ApiResourceProperties___IdentityServerConfiguration__ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "__IdentityServerConfiguration__ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ApiResourceScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ApiResourceScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ApiResourceScopes___IdentityServerConfiguration__ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "__IdentityServerConfiguration__ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ApiResourceSecrets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ApiResourceSecrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ApiResourceSecrets___IdentityServerConfiguration__ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "__IdentityServerConfiguration__ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ApiScopeClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ApiScopeClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ApiScopeClaims___IdentityServerConfiguration__ApiScopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "__IdentityServerConfiguration__ApiScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ApiScopeProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ApiScopeProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ApiScopeProperties___IdentityServerConfiguration__ApiScopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "__IdentityServerConfiguration__ApiScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientClaims___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientCorsOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientCorsOrigins", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientCorsOrigins___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientGrantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrantType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientGrantTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientGrantTypes___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientIdPRestrictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientIdPRestrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientIdPRestrictions___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientPostLogoutRedirectUris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostLogoutRedirectUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientPostLogoutRedirectUris", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientPostLogoutRedirectUris___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientProperties___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientRedirectUris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RedirectUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientRedirectUris", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientRedirectUris___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientScopes___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__ClientSecrets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__ClientSecrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__ClientSecrets___IdentityServerConfiguration__Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "__IdentityServerConfiguration__Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__IdentityResourceClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityResourceId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__IdentityResourceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__IdentityResourceClaims___IdentityServerConfiguration__IdentityResources_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "__IdentityServerConfiguration__IdentityResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerConfiguration__IdentityResourceProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityResourceId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerConfiguration__IdentityResourceProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK___IdentityServerConfiguration__IdentityResourceProperties___IdentityServerConfiguration__IdentityResources_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "__IdentityServerConfiguration__IdentityResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ApiResourceClaims_ApiResourceId",
                table: "__IdentityServerConfiguration__ApiResourceClaims",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ApiResourceProperties_ApiResourceId",
                table: "__IdentityServerConfiguration__ApiResourceProperties",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ApiResources_Name",
                table: "__IdentityServerConfiguration__ApiResources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ApiResourceScopes_ApiResourceId",
                table: "__IdentityServerConfiguration__ApiResourceScopes",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ApiResourceSecrets_ApiResourceId",
                table: "__IdentityServerConfiguration__ApiResourceSecrets",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ApiScopeClaims_ScopeId",
                table: "__IdentityServerConfiguration__ApiScopeClaims",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ApiScopeProperties_ScopeId",
                table: "__IdentityServerConfiguration__ApiScopeProperties",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ApiScopes_Name",
                table: "__IdentityServerConfiguration__ApiScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientClaims_ClientId",
                table: "__IdentityServerConfiguration__ClientClaims",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientCorsOrigins_ClientId",
                table: "__IdentityServerConfiguration__ClientCorsOrigins",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientGrantTypes_ClientId",
                table: "__IdentityServerConfiguration__ClientGrantTypes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientIdPRestrictions_ClientId",
                table: "__IdentityServerConfiguration__ClientIdPRestrictions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientPostLogoutRedirectUris_ClientId",
                table: "__IdentityServerConfiguration__ClientPostLogoutRedirectUris",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientProperties_ClientId",
                table: "__IdentityServerConfiguration__ClientProperties",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientRedirectUris_ClientId",
                table: "__IdentityServerConfiguration__ClientRedirectUris",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__Clients_ClientId",
                table: "__IdentityServerConfiguration__Clients",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientScopes_ClientId",
                table: "__IdentityServerConfiguration__ClientScopes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__ClientSecrets_ClientId",
                table: "__IdentityServerConfiguration__ClientSecrets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__IdentityResourceClaims_IdentityResourceId",
                table: "__IdentityServerConfiguration__IdentityResourceClaims",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__IdentityResourceProperties_IdentityResourceId",
                table: "__IdentityServerConfiguration__IdentityResourceProperties",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerConfiguration__IdentityResources_Name",
                table: "__IdentityServerConfiguration__IdentityResources",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ApiResourceClaims");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ApiResourceProperties");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ApiResourceScopes");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ApiResourceSecrets");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ApiScopeClaims");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ApiScopeProperties");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientClaims");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientCorsOrigins");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientGrantTypes");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientIdPRestrictions");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientPostLogoutRedirectUris");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientProperties");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientRedirectUris");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientScopes");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ClientSecrets");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__IdentityResourceClaims");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__IdentityResourceProperties");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ApiResources");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__ApiScopes");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__Clients");

            migrationBuilder.DropTable(
                name: "__IdentityServerConfiguration__IdentityResources");
        }
    }
}
