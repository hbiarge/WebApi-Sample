namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactor : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CinemaFilms", newName: "FilmCinemas");
            DropPrimaryKey("dbo.FilmCinemas");
            AddPrimaryKey("dbo.FilmCinemas", new[] { "Film_Id", "Cinema_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.FilmCinemas");
            AddPrimaryKey("dbo.FilmCinemas", new[] { "Cinema_Id", "Film_Id" });
            RenameTable(name: "dbo.FilmCinemas", newName: "CinemaFilms");
        }
    }
}
