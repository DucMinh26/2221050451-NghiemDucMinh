
using Bogus;
using BT_NET.Models.Entities;
namespace BT_NET.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            
            if (!context.Books.Any())
            {
                var Categories = new[]
                {
                    "Programming",
                    "Database",
                    "AI",
                    "Networking",
                    "DevOps",
                    "Cyber Security",
                    "Cloud Computing"
                };

                var faker = new Faker<Book>()
                    .RuleFor(x => x.ISBN,
                        f => $"978-{f.Random.Number(1000, 9999)}")

                    .RuleFor(x => x.Title,
                        f => f.Commerce.ProductName())

                    .RuleFor(x => x.Author,
                        f => f.Name.FullName())

                    .RuleFor(x => x.Publisher,
                        f => f.Company.CompanyName())

                    .RuleFor(x => x.PublishYear,
                        f => f.Random.Int(2010, 2025))

                    .RuleFor(x => x.Price, f => f.Random.Decimal(100000, 1000000))
                    .RuleFor(x => x.Quantity, f => f.Random.Int(1, 100))
                    .RuleFor(x => x.Category, f => f.PickRandom(Categories))
                    .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
                    .RuleFor(x => x.CreateDate, f => f.Date.Recent(365))
                    .RuleFor(x => x.IsAvailable, f => f.Random.Bool());

                var books = faker.Generate(500);
                context.Books.AddRange(books);
                context.SaveChanges();
            }

            if (!context.sinhViens.Any())
            {
                var danhSachLop = new[] { "K17-KTPM", "K18-HTTT", "K17-KHMT", "K19-MMT", "K18-ATTT" };

                var sinhVienFaker = new Faker<SinhVien>("vi")
                    .RuleFor(s => s.MaSV, f => "SV" + f.Random.Number(10000, 99999).ToString())
                    .RuleFor(s => s.HoTen, f => f.Name.FullName())
                    .RuleFor(s => s.Lop, f => f.PickRandom(danhSachLop))
                    .RuleFor(s => s.DiemTB, f => Math.Round(f.Random.Double(0, 10), 1));

                var sinhViens = sinhVienFaker.Generate(500);

                context.sinhViens.AddRange(sinhViens);
                context.SaveChangesAsync();
            }
        }
    }
}