using Blazorcrud.Shared.Models;
using Bogus;

namespace Blazorcrud.Server.Models;

public class DataGenerator
{
    public static void SeederStores(AppDbContext appDbContext)
    {
        if (!(appDbContext.StoresDb.Any()))
        {
            Console.WriteLine($"Server.Model.DataGenerator.Initialize: Seeder Stores");
            var testRegisters = new Faker<Blazorcrud.Shared.Models.Store>()
                .RuleFor(st => st.Name, f => f.Name.FirstName());

            var regSeeder = testRegisters.Generate(25);

            foreach (Blazorcrud.Shared.Models.Store reg in regSeeder)
            {
                appDbContext.StoresDb.Add(reg);
            }
            appDbContext.SaveChanges();
        }
    }

    public static void SeederCategories(AppDbContext appDbContext)
    {
        if (!(appDbContext.CategoriesDb.Any()))
        {
            Console.WriteLine($"Server.Model.DataGenerator.Initialize: Seeder Categories");
            var testRegisters = new Faker<Blazorcrud.Shared.Models.Category>()
                .RuleFor(cat => cat.Name, f => f.Name.FirstName());

            var regSeeder = testRegisters.Generate(25);

            foreach (Blazorcrud.Shared.Models.Category reg in regSeeder)
            {
                appDbContext.CategoriesDb.Add(reg);
            }
            appDbContext.SaveChanges();
        }
    }

    public static void SeederProducts(AppDbContext appDbContext)
    {
        if (!(appDbContext.ProductsDb.Any()))
        {
            Console.WriteLine($"Server.Model.DataGenerator.SeederProducts: Generating Products...");

            var categories = appDbContext.CategoriesDb.ToList();
            var testRegisters = new Faker<Blazorcrud.Shared.Models.Product>()
                .RuleFor(pr => pr.Title, f => f.Name.FirstName());

            var regSeeder = testRegisters.Generate(25);

            foreach (Blazorcrud.Shared.Models.Product reg in regSeeder)
            {
                appDbContext.ProductsDb.Add(reg);
            }
            appDbContext.SaveChanges();
        }
    }

    public static void SeederPeople(AppDbContext appDbContext)
    {
        if (!(appDbContext.People.Any()))
        {
            Console.WriteLine($"Server.Model.DataGenerator.Initialize: Seeder Stores");
            var testAddresses = new Faker<Blazorcrud.Shared.Models.Address>()
                .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.State())
                .RuleFor(a => a.ZipCode, f => f.Address.ZipCode());

            var testPeople = new Faker<Blazorcrud.Shared.Models.Person>()
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.Gender, f => f.PickRandom<Gender>())
                .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Addresses, f => testAddresses.Generate(2).ToList());

            var people = testPeople.Generate(25);

            foreach (Blazorcrud.Shared.Models.Person p in people)
            {
                appDbContext.People.Add(p);
            }
            appDbContext.SaveChanges();
        }

    }

    public static void SeederUploads(AppDbContext appDbContext)
    {
        if (!(appDbContext.Uploads.Any()))
        {
            string jsonRecord = @"[{""FirstName"": ""Tim"",""LastName"": ""Bucktooth"",""Gender"": 1,""PhoneNumber"": ""717-211-3211"",
                    ""Addresses"": [{""Street"": ""415 McKee Place"",""City"": ""Pittsburgh"",""State"": ""Pennsylvania"",""ZipCode"": ""15140""
                    },{ ""Street"": ""315 Gunpowder Road"",""City"": ""Mechanicsburg"",""State"": ""Pennsylvania"",""ZipCode"": ""17101""  }]}]";
            var testUploads = new Faker<Upload>()
                .RuleFor(u => u.FileName, u => u.Lorem.Word() + ".json")
                .RuleFor(u => u.UploadTimestamp, u => u.Date.Past(1, DateTime.Now))
                .RuleFor(u => u.ProcessedTimestamp, u => u.Date.Future(1, DateTime.Now))
                .RuleFor(u => u.FileContent, Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonRecord)));
            var uploads = testUploads.Generate(8);

            foreach (Upload u in uploads)
            {
                appDbContext.Uploads.Add(u);
            }
            appDbContext.SaveChanges();
        }
    }

    public static void SeederUsers(AppDbContext appDbContext)
    {
        if (!(appDbContext.Users.Any()))
        {
            var testUsers = new Faker<User>()
                .RuleFor(u => u.FirstName, u => u.Name.FirstName())
                .RuleFor(u => u.LastName, u => u.Name.LastName())
                .RuleFor(u => u.Username, u => u.Internet.UserName())
                .RuleFor(u => u.Password, u => u.Internet.Password());
            var users = testUsers.Generate(4);

            User customUser = new User()
            {
                FirstName = "Terry",
                LastName = "Smith",
                Username = "admin",
                Password = "admin"
            };

            users.Add(customUser);

            foreach (User u in users)
            {
                u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.Password);
                u.Password = "**********";
                appDbContext.Users.Add(u);
            }
            appDbContext.SaveChanges();
        }
    }

    public static void Initialize(AppDbContext appDbContext)
    {
        Randomizer.Seed = new Random(32321);
        appDbContext.Database.EnsureDeleted();
        appDbContext.Database.EnsureCreated();

        // Seeders
        DataGenerator.SeederStores(appDbContext);
        DataGenerator.SeederCategories(appDbContext);
        DataGenerator.SeederProducts(appDbContext);
        DataGenerator.SeederPeople(appDbContext);
        DataGenerator.SeederUploads(appDbContext);
        DataGenerator.SeederUsers(appDbContext);
    }
}