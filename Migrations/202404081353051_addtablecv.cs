namespace Job_websit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablecv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "Cv", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplyForJobs", "Cv");
        }
    }
}
