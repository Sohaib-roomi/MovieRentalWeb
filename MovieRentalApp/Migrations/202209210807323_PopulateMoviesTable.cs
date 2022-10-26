namespace MovieRentalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMoviesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies (Name, GenreId, DateAdded, ReleaseDate, NumberInStock) VALUES ('Lord Of the Rings',3, 09-15-2022, 02-26-2022,50)");
            Sql("INSERT INTO Movies (Name, GenreId, DateAdded, ReleaseDate, NumberInStock) VALUES ('Die Hard!',1, 09-15-2022, 02-26-2022,50)");
            Sql("INSERT INTO Movies (Name, GenreId, DateAdded, ReleaseDate, NumberInStock) VALUES ('Don''t Breathe',5, 09-15-2022, 02-26-2022,50)");
            Sql("INSERT INTO Movies (Name, GenreId, DateAdded, ReleaseDate, numberInStock) VALUES ('The Lion King!',6, 09-15-2022, 02-26-2022,50)");
        }
        
        public override void Down()
        {
        }
    }
}
