using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens;
using MyVeggieGarden.Identity;
using MyVeggieGarden.Models.Account;
using MyVeggieGarden.Repository;
using MyVeggieGarden.Services;
using MyVeggieGarden.Web.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyVeggieGarden.Web
{
    public class Startup
    {
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // services.Configure<CloudinaryOptions>(Configuration.GetSection("CloudinaryOptions"));

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderItemRepository, PurchaseOrderItemRepository>();

            services.AddIdentityCore<ApplicationUserIdentity>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddUserStore<UserStore>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<ApplicationUserIdentity>>();

            services.AddControllers();
            services.AddCors();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer
                (
                    options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                            ClockSkew = System.TimeSpan.Zero
                        };
                    }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*
            else
            {
                app.UseExceptionHandler("/Error");
            }
            */
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            

            // Added
            app.ConfigureExceptionHandler();

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            }
            else
            {
                app.UseCors();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
                
                //endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
