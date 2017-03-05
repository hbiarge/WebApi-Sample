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
                "cine.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        DurationInMinutes = c.Int(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        ScreenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("cine.Screens", t => t.ScreenId, cascadeDelete: true)
                .Index(t => t.ScreenId);
            
            CreateTable(
                "session.SessionSeats",
                c => new
                    {
                        SessionId = c.Int(nullable: false),
                        SeatId = c.Int(nullable: false),
                        Sold = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.SessionId, t.SeatId })
                .ForeignKey("session.Sessions", t => t.SessionId)
                .ForeignKey("cine.Seats", t => t.SeatId)
                .Index(t => t.SessionId)
                .Index(t => t.SeatId);
            
            CreateTable(
                "session.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScreenId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("cine.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("cine.Screens", t => t.ScreenId, cascadeDelete: true)
                .Index(t => t.ScreenId)
                .Index(t => t.FilmId);
            
            CreateTable(
                "session.Tickets",
                c => new
                    {
                        SessionId = c.Int(nullable: false),
                        SeatId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.SessionId, t.SeatId })
                .ForeignKey("session.SessionSeats", t => new { t.SessionId, t.SeatId })
                .Index(t => new { t.SessionId, t.SeatId });
            
            CreateTable(
                "dbo.CinemaFilms",
                c => new
                    {
                        Cinema_Id = c.Int(nullable: false),
                        Film_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Cinema_Id, t.Film_Id })
                .ForeignKey("cine.Cinemas", t => t.Cinema_Id, cascadeDelete: true)
                .ForeignKey("cine.Films", t => t.Film_Id, cascadeDelete: true)
                .Index(t => t.Cinema_Id)
                .Index(t => t.Film_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("cine.Screens", "CinemaId", "cine.Cinemas");
            DropForeignKey("cine.Seats", "ScreenId", "cine.Screens");
            DropForeignKey("session.SessionSeats", "SeatId", "cine.Seats");
            DropForeignKey("session.Tickets", new[] { "SessionId", "SeatId" }, "session.SessionSeats");
            DropForeignKey("session.SessionSeats", "SessionId", "session.Sessions");
            DropForeignKey("session.Sessions", "ScreenId", "cine.Screens");
            DropForeignKey("session.Sessions", "FilmId", "cine.Films");
            DropForeignKey("dbo.CinemaFilms", "Film_Id", "cine.Films");
            DropForeignKey("dbo.CinemaFilms", "Cinema_Id", "cine.Cinemas");
            DropIndex("dbo.CinemaFilms", new[] { "Film_Id" });
            DropIndex("dbo.CinemaFilms", new[] { "Cinema_Id" });
            DropIndex("session.Tickets", new[] { "SessionId", "SeatId" });
            DropIndex("session.Sessions", new[] { "FilmId" });
            DropIndex("session.Sessions", new[] { "ScreenId" });
            DropIndex("session.SessionSeats", new[] { "SeatId" });
            DropIndex("session.SessionSeats", new[] { "SessionId" });
            DropIndex("cine.Seats", new[] { "ScreenId" });
            DropIndex("cine.Screens", new[] { "CinemaId" });
            DropTable("dbo.CinemaFilms");
            DropTable("session.Tickets");
            DropTable("session.Sessions");
            DropTable("session.SessionSeats");
            DropTable("cine.Seats");
            DropTable("cine.Screens");
            DropTable("cine.Films");
            DropTable("cine.Cinemas");
        }
    }
}
