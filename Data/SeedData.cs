using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AutoPartsContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            // Создание ролей
            string[] roles = { "Administrator", "Manager", "Customer" };
            
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            // Создание администратора
            var adminEmail = "admin@autoparts.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Администратор",
                    LastName = "Системы",
                    Address = "Административный адрес",
                    EmailConfirmed = true
                };
                
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
            
            // Создание менеджера
            var managerEmail = "manager@autoparts.com";
            var managerUser = await userManager.FindByEmailAsync(managerEmail);
            
            if (managerUser == null)
            {
                managerUser = new ApplicationUser
                {
                    UserName = managerEmail,
                    Email = managerEmail,
                    FirstName = "Менеджер",
                    LastName = "Магазина",
                    Address = "Рабочий адрес",
                    EmailConfirmed = true
                };
                
                var result = await userManager.CreateAsync(managerUser, "Manager123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(managerUser, "Manager");
                }
            }
            
            // Добавление тестовых данных
            await AddTestData(context);
        }
        
        private static async Task AddTestData(AutoPartsContext context)
        {
            // Добавление категорий
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Двигатель", Description = "Запчасти для двигателя" },
                    new Category { Name = "Тормозная система", Description = "Тормозные колодки, диски" },
                    new Category { Name = "Подвеска", Description = "Амортизаторы, пружины" },
                    new Category { Name = "Электрика", Description = "Аккумуляторы, генераторы" },
                    new Category { Name = "Кузовные детали", Description = "Бамперы, фары, зеркала" }
                );
                await context.SaveChangesAsync();
            }
            
            // Добавление производителей
            if (!context.Manufacturers.Any())
            {
                context.Manufacturers.AddRange(
                    new Manufacturer { Name = "Bosch", Country = "Германия", Description = "Немецкий производитель автокомплектующих" },
                    new Manufacturer { Name = "KYB", Country = "Япония", Description = "Японский производитель амортизаторов" },
                    new Manufacturer { Name = "Brembo", Country = "Италия", Description = "Итальянский производитель тормозных систем" },
                    new Manufacturer { Name = "Mann-Filter", Country = "Германия", Description = "Производитель фильтров" },
                    new Manufacturer { Name = "NGK", Country = "Япония", Description = "Производитель свечей зажигания" }
                );
                await context.SaveChangesAsync();
            }
            
            // Добавление товаров
            if (!context.Parts.Any())
            {
                var categories = await context.Categories.ToListAsync();
                var manufacturers = await context.Manufacturers.ToListAsync();
                
                context.Parts.AddRange(
                    new Part
                    {
                        Name = "Тормозные колодки передние",
                        Article = "TP12345",
                        ManufacturerId = manufacturers[2].ManufacturerId,
                        CategoryId = categories[1].CategoryId,
                        Price = 4500.00m,
                        StockQuantity = 25,
                        Description = "Высококачественные тормозные колодки для передних колес"
                    },
                    new Part
                    {
                        Name = "Амортизатор передний",
                        Article = "AP67890",
                        ManufacturerId = manufacturers[1].ManufacturerId,
                        CategoryId = categories[2].CategoryId,
                        Price = 3200.00m,
                        StockQuantity = 15,
                        Description = "Газомасляный амортизатор для передней подвески"
                    },
                    new Part
                    {
                        Name = "Воздушный фильтр",
                        Article = "AF54321",
                        ManufacturerId = manufacturers[3].ManufacturerId,
                        CategoryId = categories[0].CategoryId,
                        Price = 1200.00m,
                        StockQuantity = 50,
                        Description = "Воздушный фильтр салонный"
                    },
                    new Part
                    {
                        Name = "Свеча зажигания",
                        Article = "SG98765",
                        ManufacturerId = manufacturers[4].ManufacturerId,
                        CategoryId = categories[0].CategoryId,
                        Price = 450.00m,
                        StockQuantity = 100,
                        Description = "Иридиевая свеча зажигания"
                    },
                    new Part
                    {
                        Name = "Фара передняя левая",
                        Article = "FL11223",
                        ManufacturerId = manufacturers[0].ManufacturerId,
                        CategoryId = categories[4].CategoryId,
                        Price = 8500.00m,
                        StockQuantity = 10,
                        Description = "Передняя фара для левой стороны"
                    }
                );
                await context.SaveChangesAsync();
            }
            
            // Добавление тестового заказа
            if (!context.Orders.Any())
            {
                var user = await context.Users.FirstOrDefaultAsync();
                var parts = await context.Parts.Take(2).ToListAsync();
                
                if (user != null && parts.Count >= 2)
                {
                    var order = new Order
                    {
                        UserId = user.Id,
                        OrderDate = DateTime.Now.AddDays(-3),
                        Status = "Доставлен",
                        ShippingAddress = "г. Москва, ул. Ленина, д. 10",
                        PhoneNumber = "+79991234567",
                        TotalAmount = parts[0].Price + parts[1].Price
                    };
                    
                    context.Orders.Add(order);
                    await context.SaveChangesAsync();
                    
                    // Добавление позиций заказа
                    context.OrderItems.AddRange(
                        new OrderItem
                        {
                            OrderId = order.OrderId,
                            PartId = parts[0].PartId,
                            Quantity = 1,
                            UnitPrice = parts[0].Price
                        },
                        new OrderItem
                        {
                            OrderId = order.OrderId,
                            PartId = parts[1].PartId,
                            Quantity = 2,
                            UnitPrice = parts[1].Price
                        }
                    );
                    
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}