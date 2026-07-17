using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Users', 'RoleId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Users] ADD [RoleId] int NOT NULL CONSTRAINT [DF_Users_RoleId] DEFAULT 0;
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.Permissions', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Permissions] (
        [Id] int NOT NULL IDENTITY(1,1),
        [Code] nvarchar(100) NOT NULL,
        [Name] nvarchar(150) NOT NULL,
        [Description] nvarchar(500) NULL,
        [Module] nvarchar(100) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] int NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
    );
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.Roles', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Roles] (
        [Id] int NOT NULL IDENTITY(1,1),
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] int NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Users] (
        [Id] int NOT NULL IDENTITY(1,1),
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        [UserName] nvarchar(100) NOT NULL,
        [Email] nvarchar(256) NOT NULL,
        [PasswordHash] nvarchar(500) NOT NULL,
        [PhoneNumber] nvarchar(20) NULL,
        [EmailConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
        [PhoneNumberConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
        [LastLoginDate] datetime2 NULL,
        [RoleId] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] int NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.RolePermissions', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[RolePermissions] (
        [Id] int NOT NULL IDENTITY(1,1),
        [RoleId] int NOT NULL,
        [PermissionId] int NOT NULL,
        CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RolePermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE NO ACTION
    );
END;
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Permissions_Code' AND object_id = OBJECT_ID('dbo.Permissions'))
BEGIN
    CREATE UNIQUE INDEX [IX_Permissions_Code] ON [dbo].[Permissions] ([Code]);
END;
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_RolePermissions_PermissionId' AND object_id = OBJECT_ID('dbo.RolePermissions'))
BEGIN
    CREATE INDEX [IX_RolePermissions_PermissionId] ON [dbo].[RolePermissions] ([PermissionId]);
END;
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_RolePermissions_RoleId_PermissionId' AND object_id = OBJECT_ID('dbo.RolePermissions'))
BEGIN
    CREATE UNIQUE INDEX [IX_RolePermissions_RoleId_PermissionId] ON [dbo].[RolePermissions] ([RoleId], [PermissionId]);
END;
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Roles_Name' AND object_id = OBJECT_ID('dbo.Roles'))
BEGIN
    CREATE UNIQUE INDEX [IX_Roles_Name] ON [dbo].[Roles] ([Name]);
END;
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Users_Email' AND object_id = OBJECT_ID('dbo.Users'))
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Email] ON [dbo].[Users] ([Email]);
END;
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Users_UserName' AND object_id = OBJECT_ID('dbo.Users'))
BEGIN
    CREATE UNIQUE INDEX [IX_Users_UserName] ON [dbo].[Users] ([UserName]);
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID(N'dbo.RolePermissions', N'U') IS NOT NULL DROP TABLE [dbo].[RolePermissions];");
            migrationBuilder.Sql(@"IF OBJECT_ID(N'dbo.Permissions', N'U') IS NOT NULL DROP TABLE [dbo].[Permissions];");
            migrationBuilder.Sql(@"IF OBJECT_ID(N'dbo.Roles', N'U') IS NOT NULL DROP TABLE [dbo].[Roles];");
            migrationBuilder.Sql(@"IF OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL DROP TABLE [dbo].[Users];");
        }
    }
}
