using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WeddingPlatform.Models;
using WeddingPlatform.Data;
using Microsoft.Extensions.DependencyInjection;

namespace WeddingPlatform
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();  // Add this line

            // Configure HTTPS
            builder.Services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 7235;
            });

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            // Initialize Database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    
                    await context.Database.EnsureCreatedAsync();
                    
                    // Seed initial admin user if not exists
                    if (!context.Users.Any())
                    {
                        var adminRole = new IdentityRole("Admin");
                        await roleManager.CreateAsync(adminRole);

                        var adminUser = new User
                        {
                            UserName = "admin@wedding.com",
                            Email = "admin@wedding.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            LastName = "User",
                            CreatedAt = DateTime.UtcNow,
                            IsAdmin = true,
                            WeddingSites = new List<WeddingSite>()
                        };

                        await userManager.CreateAsync(adminUser, "Admin123!");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            await app.RunAsync();
        }
    }
}
