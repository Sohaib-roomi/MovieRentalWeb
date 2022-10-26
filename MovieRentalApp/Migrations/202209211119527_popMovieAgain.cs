namespace MovieRentalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class popMovieAgain : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies (Name, GenreId, DateAdded, ReleaseDate, numberInStock) VALUES ('Nightcrawler',6, 09-15-2022, 02-26-2022,100)");
        }
        
        public override void Down()
        {
        }
    }
}
