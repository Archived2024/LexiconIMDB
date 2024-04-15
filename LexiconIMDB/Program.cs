using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LexiconIMDB.Data;
using LexiconIMDB.Services;
namespace LexiconIMDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LexiconIMDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LexiconIMDBContext") ?? throw new InvalidOperationException("Connection string 'LexiconIMDBContext' not found.")));

            builder.Services.AddScoped<IGenreSelectListService, GenreSelectListService>();

            //builder.Services.AddSingleton -> Samma instans för hela applikationens livslängd
            //builder.Services.AddScoped ->  Samma instans för hela vårt request
            //builder.Services.Transient -> Ny instans så fort någon behöver servicen. 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Movie/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
