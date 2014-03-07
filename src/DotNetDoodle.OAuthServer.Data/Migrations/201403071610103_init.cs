namespace DotNetDoodle.OAuthServer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        LogoUrl = c.String(),
                        Namespace = c.String(),
                        Audience = c.String(),
                        TokenLifetime = c.Int(nullable: false),
                        AllowRefreshToken = c.Boolean(nullable: false),
                        RequireConsent = c.Boolean(nullable: false),
                        AllowRememberConsentDecision = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scopes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 150),
                        ApplicationId = c.Int(nullable: false),
                        DisplayName = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 1000),
                        IsEmphasized = c.Boolean(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 150),
                        Name = c.String(nullable: false, maxLength: 250),
                        ClientSecretHash = c.String(),
                        AllowedFlow = c.Byte(nullable: false),
                        AllowRefreshToken = c.Boolean(nullable: false),
                        RequireConsent = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientRedirectUris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(maxLength: 150),
                        Uri = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.StoredGrants",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 150),
                        ClientId = c.String(maxLength: 150),
                        ApplicationId = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Type = c.Byte(nullable: false),
                        CreateRefreshToken = c.Boolean(nullable: false),
                        RedirectUri = c.String(maxLength: 1000),
                        ExpiresOn = c.DateTimeOffset(nullable: false, precision: 7),
                        RefreshTokenExpiresOn = c.DateTimeOffset(precision: 7),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.StoredGrantResourceOwnerClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoredGrantId = c.String(nullable: false, maxLength: 150),
                        Type = c.String(nullable: false),
                        Value = c.String(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoredGrants", t => t.StoredGrantId)
                .Index(t => t.StoredGrantId);
            
            CreateTable(
                "dbo.ClientEntityScopeEntities",
                c => new
                    {
                        ClientEntity_Id = c.String(nullable: false, maxLength: 150),
                        ScopeEntity_Id = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.ClientEntity_Id, t.ScopeEntity_Id })
                .ForeignKey("dbo.Clients", t => t.ClientEntity_Id)
                .ForeignKey("dbo.Scopes", t => t.ScopeEntity_Id)
                .Index(t => t.ClientEntity_Id)
                .Index(t => t.ScopeEntity_Id);
            
            CreateTable(
                "dbo.StoredGrantEntityScopeEntities",
                c => new
                    {
                        StoredGrantEntity_Id = c.String(nullable: false, maxLength: 150),
                        ScopeEntity_Id = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.StoredGrantEntity_Id, t.ScopeEntity_Id })
                .ForeignKey("dbo.StoredGrants", t => t.StoredGrantEntity_Id)
                .ForeignKey("dbo.Scopes", t => t.ScopeEntity_Id)
                .Index(t => t.StoredGrantEntity_Id)
                .Index(t => t.ScopeEntity_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scopes", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.StoredGrantResourceOwnerClaims", "StoredGrantId", "dbo.StoredGrants");
            DropForeignKey("dbo.StoredGrants", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.StoredGrants", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.StoredGrantEntityScopeEntities", "ScopeEntity_Id", "dbo.Scopes");
            DropForeignKey("dbo.StoredGrantEntityScopeEntities", "StoredGrantEntity_Id", "dbo.StoredGrants");
            DropForeignKey("dbo.ClientEntityScopeEntities", "ScopeEntity_Id", "dbo.Scopes");
            DropForeignKey("dbo.ClientEntityScopeEntities", "ClientEntity_Id", "dbo.Clients");
            DropForeignKey("dbo.ClientRedirectUris", "ClientId", "dbo.Clients");
            DropIndex("dbo.StoredGrantEntityScopeEntities", new[] { "ScopeEntity_Id" });
            DropIndex("dbo.StoredGrantEntityScopeEntities", new[] { "StoredGrantEntity_Id" });
            DropIndex("dbo.ClientEntityScopeEntities", new[] { "ScopeEntity_Id" });
            DropIndex("dbo.ClientEntityScopeEntities", new[] { "ClientEntity_Id" });
            DropIndex("dbo.StoredGrantResourceOwnerClaims", new[] { "StoredGrantId" });
            DropIndex("dbo.StoredGrants", new[] { "ApplicationId" });
            DropIndex("dbo.StoredGrants", new[] { "ClientId" });
            DropIndex("dbo.ClientRedirectUris", new[] { "ClientId" });
            DropIndex("dbo.Scopes", new[] { "ApplicationId" });
            DropTable("dbo.StoredGrantEntityScopeEntities");
            DropTable("dbo.ClientEntityScopeEntities");
            DropTable("dbo.StoredGrantResourceOwnerClaims");
            DropTable("dbo.StoredGrants");
            DropTable("dbo.ClientRedirectUris");
            DropTable("dbo.Clients");
            DropTable("dbo.Scopes");
            DropTable("dbo.Applications");
        }
    }
}
