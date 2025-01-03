using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Services;
using MantenanceProjetASPNET6.Services_User;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace MantenanceProjetASPNET6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GestionConcourCoreDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));

            builder.Services.AddTransient<ISearch3Service, Search3ServiceImp>();
            builder.Services.AddTransient<ICorbeil3Service, Corbeil3ServiceImp>();
            builder.Services.AddTransient<Services.ISelectionService, SelectionServiceImp>();
            builder.Services.AddTransient<IPreselectionService, PreselectionServiceImp>();
            builder.Services.AddTransient<ICandidatService, CandidatServiceImp>();
            builder.Services.AddTransient<Services.IEpreuveService, Services.EpreuveServiceImp>();
            builder.Services.AddTransient<Services_User.IEpreuveService, Services_User.EpreuveServiceImp>();
            builder.Services.AddScoped<IEnregistrementService, EnregistrementServiceImp>();
            builder.Services.AddScoped<ICorrectionService, CorrectionServiceImp>();
            builder.Services.AddTransient<IStatistiqueService, StatistiqueServiceImpl>();
            builder.Services.AddTransient<IFiche, FicheImp>();
            builder.Services.AddTransient<IIndexService, IndexServiceImp>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddSession();

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
            app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Landing}/{action=Index}/{id?}");

            //Rotativa.AspNetCore.RotativaConfiguration.Setup(app.Environment);

            app.Run();
        }
    }
}
