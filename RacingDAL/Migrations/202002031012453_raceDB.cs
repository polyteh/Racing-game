namespace RacingDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class raceDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RacingCarId = c.Int(nullable: false),
                        RaceId = c.Int(nullable: false),
                        Place = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Races", t => t.RaceId, cascadeDelete: true)
                .ForeignKey("dbo.RacingCars", t => t.RacingCarId, cascadeDelete: true)
                .Index(t => t.RacingCarId)
                .Index(t => t.RaceId);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarStats", "RacingCarId", "dbo.RacingCars");
            DropForeignKey("dbo.CarStats", "RaceId", "dbo.Races");
            DropIndex("dbo.CarStats", new[] { "RaceId" });
            DropIndex("dbo.CarStats", new[] { "RacingCarId" });
            DropTable("dbo.Races");
            DropTable("dbo.CarStats");
        }
    }
}
