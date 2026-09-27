using Microsoft.EntityFrameworkCore;
using WatchArchive.Server.Models;
using WatchArchive.Server.Models.Attributes;

namespace WatchArchive.Server.Data;

public class WatchArchiveDbContext(DbContextOptions<WatchArchiveDbContext> options)
    : DbContext(options)
{
    public required DbSet<User> Users { get; set; }

    public required DbSet<UserSession> UserSessions { get; set; }

    public required DbSet<RatedEntry> RatedEntries { get; set; }

    public required DbSet<Thumbnail> Thumbnails { get; set; }

    public required DbSet<Category> Categories { get; set; }

    public required DbSet<CategoryEntry> CategoryEntries { get; set; }

    public required DbSet<Tag> Tags { get; set; }

    public required DbSet<TagEntry> TagEntries { get; set; }

    public required DbSet<StarRatingAttribute> StarRatingAttributes { get; set; }

    public required DbSet<ShortTextAttribute> ShortTextAttributes { get; set; }

    public required DbSet<LongTextAttribute> LongTextAttributes { get; set; }

    public required DbSet<NumberAttribute> NumberAttributes { get; set; }

    public required DbSet<DateAttribute> DateAttributes { get; set; }

    public required DbSet<BooleanAttribute> BooleanAttributes { get; set; }

    public required DbSet<UrlAttribute> UrlAttributes { get; set; }

    public required DbSet<SingleSelectAttribute> SingleSelectAttributes { get; set; }

    public required DbSet<SelectOption> SelectOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enable Postgres citext extension
        modelBuilder.HasPostgresExtension("citext");

        // Seed data
        PopulateUsers(modelBuilder);
        PopulateCategories(modelBuilder);
    }

    private static void PopulateUsers(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasData(
                new User()
                {
                    Id = Guid.Parse("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1"),
                    Username = "taoge",
                    PasswordHash =
                        "AQAAAAIAAYagAAAAEMqEhn8w5pG58XMyEwggQhdvq8r3S2fDfulLzyclwjjs4ZF1VdJnLJ7YDj9+c67VMQ==",
                    CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 1, 1), DateTimeKind.Utc),
                }
            );
    }

    private static void PopulateCategories(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Category>()
            .HasData([
                new Category()
                {
                    Id = Guid.Parse("db9b5c46-2f5f-4fd4-8e86-8a7cf34458ed"),
                    Name = "Movie",
                    UserId = Guid.Parse("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1"),
                    User = null!,
                },
                new Category()
                {
                    Id = Guid.Parse("777f65af-997b-4aff-bf13-dc15738e3efe"),
                    Name = "TV Show",
                    UserId = Guid.Parse("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1"),
                    User = null!,
                },
                new Category()
                {
                    Id = Guid.Parse("ff336c33-fcd6-48c6-b0ee-aef243cff897"),
                    Name = "Anime",
                    UserId = Guid.Parse("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1"),
                    User = null!,
                },
                new Category()
                {
                    Id = Guid.Parse("81668a4a-412d-4aa0-8ec1-4c11509b51bc"),
                    Name = "Documentary",
                    UserId = Guid.Parse("34bd8f63-1ec7-47ec-9f7f-c5a9646369d1"),
                    User = null!,
                },
            ]);
    }
}
