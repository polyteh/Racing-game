namespace RacingDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableplace : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarStats", "Place", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarStats", "Place", c => c.Int(nullable: false));
        }
    }
}
