namespace MovieRentalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageProptoCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ImageURL");
        }
    }
}
