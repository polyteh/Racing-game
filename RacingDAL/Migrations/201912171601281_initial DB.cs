namespace RacingDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Engines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HP = c.Int(nullable: false),
                        Turbine = c.Boolean(nullable: false),
                        Manufacurer = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brakes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EffecientKoef = c.Int(nullable: false),
                        Manufacurer = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RacingCars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BrakeId = c.Int(nullable: false),
                        EngineId = c.Int(nullable: false),
                        SuspentionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brakes", t => t.BrakeId, cascadeDelete: true)
                .ForeignKey("dbo.Engines", t => t.EngineId, cascadeDelete: true)
                .ForeignKey("dbo.Suspentions", t => t.SuspentionId, cascadeDelete: true)
                .Index(t => t.BrakeId)
                .Index(t => t.EngineId)
                .Index(t => t.SuspentionId);
            
            CreateTable(
                "dbo.Suspentions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RigidityKoef = c.Int(nullable: false),
                        Manufacurer = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RacingCars", "SuspentionId", "dbo.Suspentions");
            DropForeignKey("dbo.RacingCars", "EngineId", "dbo.Engines");
            DropForeignKey("dbo.RacingCars", "BrakeId", "dbo.Brakes");
            DropIndex("dbo.RacingCars", new[] { "SuspentionId" });
            DropIndex("dbo.RacingCars", new[] { "EngineId" });
            DropIndex("dbo.RacingCars", new[] { "BrakeId" });
            DropTable("dbo.Suspentions");
            DropTable("dbo.RacingCars");
            DropTable("dbo.Brakes");
            DropTable("dbo.Engines");
        }
    }
}
