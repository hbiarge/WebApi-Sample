namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "cine.Cinemas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "cine.Screens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CinemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("cine.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .Index(t => t.CinemaId);
            
            CreateTable(
                "cine.Seats",
                c => new
                    {
                        ScreenId = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScreenId, t.Row, t.Number })
                .ForeignKey("cine.Screens", t => t.ScreenId, cascadeDelete: true)
                .Index(t => t.ScreenId);
            
            CreateTable(
                "cine.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        DurationInMinutes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "session.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScreenId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("cine.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("cine.Screens", t => t.ScreenId, cascadeDelete: true)
                .Index(t => t.ScreenId)
                .Index(t => t.FilmId);
            
            CreateTable(
                "session.SessionSeats",
                c => new
                    {
                        SessionId = c.Int(nullable: false),
                        SeatRow = c.Int(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        SeatScreenId = c.Int(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Ticket = c.Guid(),
                    })
                .PrimaryKey(t => new { t.SessionId, t.SeatRow, t.SeatNumber })
                .ForeignKey("cine.Seats", t => new { t.SeatScreenId, t.SeatRow, t.SeatNumber }, cascadeDelete: true)
                .ForeignKey("session.Sessions", t => t.SessionId)
                .Index(t => t.SessionId)
                .Index(t => new { t.SeatScreenId, t.SeatRow, t.SeatNumber });
            
            CreateTable(
                "cine.FilmCinemas",
                c => new
                    {
                        Film_Id = c.Int(nullable: false),
                        Cinema_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_Id, t.Cinema_Id })
                .ForeignKey("cine.Films", t => t.Film_Id, cascadeDelete: true)
                .ForeignKey("cine.Cinemas", t => t.Cinema_Id, cascadeDelete: true)
                .Index(t => t.Film_Id)
                .Index(t => t.Cinema_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("session.SessionSeats", "SessionId", "session.Sessions");
            DropForeignKey("session.SessionSeats", new[] { "SeatScreenId", "SeatRow", "SeatNumber" }, "cine.Seats");
            DropForeignKey("session.Sessions", "ScreenId", "cine.Screens");
            DropForeignKey("session.Sessions", "FilmId", "cine.Films");
            DropForeignKey("cine.FilmCinemas", "Cinema_Id", "cine.Cinemas");
            DropForeignKey("cine.FilmCinemas", "Film_Id", "cine.Films");
            DropForeignKey("cine.Screens", "CinemaId", "cine.Cinemas");
            DropForeignKey("cine.Seats", "ScreenId", "cine.Screens");
            DropIndex("cine.FilmCinemas", new[] { "Cinema_Id" });
            DropIndex("cine.FilmCinemas", new[] { "Film_Id" });
            DropIndex("session.SessionSeats", new[] { "SeatScreenId", "SeatRow", "SeatNumber" });
            DropIndex("session.SessionSeats", new[] { "SessionId" });
            DropIndex("session.Sessions", new[] { "FilmId" });
            DropIndex("session.Sessions", new[] { "ScreenId" });
            DropIndex("cine.Seats", new[] { "ScreenId" });
            DropIndex("cine.Screens", new[] { "CinemaId" });
            DropTable("cine.FilmCinemas");
            DropTable("session.SessionSeats");
            DropTable("session.Sessions");
            DropTable("cine.Films");
            DropTable("cine.Seats");
            DropTable("cine.Screens");
            DropTable("cine.Cinemas");
        }
    }
}
