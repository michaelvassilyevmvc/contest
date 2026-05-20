using contest.CompetitionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace contest.CompetitionService.Data;

public class DbInitializer
{
    public static void DbInit(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        SeedData(scope.ServiceProvider.GetService<CompetitionDbContext>());
    }

    private static void SeedData(CompetitionDbContext context)
    {
        context.Database.Migrate();
        if (context.Competitions.Any())
        {
            Console.WriteLine("Already have data - no need  to seed.");
            return;
        }

        var venue1Id = Guid.NewGuid();
        var venue2Id = Guid.NewGuid();
        var venue3Id = Guid.NewGuid();
        var venue4Id = Guid.NewGuid();
        var venue5Id = Guid.NewGuid();

        var venues = new List<Venue>
        {
            new()
            {
                Id = venue1Id,
                Name = "Центральный стадион",
                Capacity = 12000,
                VenueType = VenueType.OpenAir,
                Address = new Address
                {
                    City = "Караганда",
                    Street = "пр. Бухар-Жырау 55"
                }
            },

            new()
            {
                Id = venue2Id,
                Name = "Дворец спорта Жастар",
                Capacity = 5000,
                VenueType = VenueType.Building,
                Address = new Address
                {
                    City = "Караганда",
                    Street = "ул. Ерубаева 48"
                }
            },

            new()
            {
                Id = venue3Id,
                Name = "Теннисный центр",
                Capacity = 2000,
                VenueType = VenueType.Building,
                Address = new Address
                {
                    City = "Караганда",
                    Street = "ул. Муканова 17"
                }
            },

            new()
            {
                Id = venue4Id,
                Name = "Олимпийский бассейн",
                Capacity = 3500,
                VenueType = VenueType.Building,
                Address = new Address
                {
                    City = "Караганда",
                    Street = "ул. Ермекова 101"
                }
            },

            new()
            {
                Id = venue5Id,
                Name = "Шахматный клуб",
                Capacity = 300,
                VenueType = VenueType.Building,
                Address = new Address
                {
                    City = "Караганда",
                    Street = "ул. Абдирова 22"
                }
            }
        };
        context.Venues.AddRange(venues);

        var competitions = new List<Competition>
        {
            new()
            {
                Title = "Чемпионат города по футболу",
                SportType = "Football",
                StartDate = new DateTime(2026, 6, 10, 10, 0, 0),
                EndDate = new DateTime(2026, 6, 10, 18, 0, 0),
                TicketPrice = 2500,
                VenueId = venue1Id
            },

            new()
            {
                Title = "Открытый турнир по боксу",
                SportType = "Boxing",
                StartDate = new DateTime(2026, 6, 12, 12, 0, 0),
                EndDate = new DateTime(2026, 6, 12, 20, 0, 0),
                TicketPrice = 3500,
                VenueId = venue2Id
            },

            new()
            {
                Title = "Кубок города по баскетболу",
                SportType = "Basketball",
                StartDate = new DateTime(2026, 6, 15, 9, 0, 0),
                EndDate = new DateTime(2026, 6, 15, 19, 0, 0),
                TicketPrice = 2000,
                VenueId = venue2Id
            },

            new()
            {
                Title = "Первенство по плаванию",
                SportType = "Swimming",
                StartDate = new DateTime(2026, 6, 18, 8, 0, 0),
                EndDate = new DateTime(2026, 6, 18, 16, 0, 0),
                TicketPrice = 1800,
                VenueId = venue4Id
            },

            new()
            {
                Title = "Городской турнир по шахматам",
                SportType = "Chess",
                StartDate = new DateTime(2026, 6, 20, 10, 0, 0),
                EndDate = new DateTime(2026, 6, 20, 17, 0, 0),
                TicketPrice = 1000,
                VenueId = venue5Id
            },

            new()
            {
                Title = "Соревнования по лёгкой атлетике",
                SportType = "Athletics",
                StartDate = new DateTime(2026, 6, 22, 9, 0, 0),
                EndDate = new DateTime(2026, 6, 22, 18, 0, 0),
                TicketPrice = 3000,
                VenueId = venue1Id
            },

            new()
            {
                Title = "Турнир по волейболу",
                SportType = "Volleyball",
                StartDate = new DateTime(2026, 6, 24, 11, 0, 0),
                EndDate = new DateTime(2026, 6, 24, 19, 0, 0),
                TicketPrice = 2200,
                VenueId = venue2Id
            },

            new()
            {
                Title = "Открытое первенство по теннису",
                SportType = "Tennis",
                StartDate = new DateTime(2026, 6, 27, 10, 0, 0),
                EndDate = new DateTime(2026, 6, 27, 18, 0, 0),
                TicketPrice = 2700,
                VenueId = venue3Id
            },

            new()
            {
                Title = "Городской турнир по дзюдо",
                SportType = "Judo",
                StartDate = new DateTime(2026, 6, 29, 9, 0, 0),
                EndDate = new DateTime(2026, 6, 29, 17, 0, 0),
                TicketPrice = 3200,
                VenueId = venue2Id
            },

            new()
            {
                Title = "Чемпионат по настольному теннису",
                SportType = "Table Tennis",
                StartDate = new DateTime(2026, 7, 2, 10, 0, 0),
                EndDate = new DateTime(2026, 7, 2, 16, 0, 0),
                TicketPrice = 1500,
                VenueId = venue3Id
            }
        };
        context.Competitions.AddRange(competitions);
        context.SaveChanges();
    }
}