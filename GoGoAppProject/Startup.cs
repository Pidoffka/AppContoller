using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors.Infrastructure;



using Owin;
using Microsoft.AspNetCore.Http.Connections;
using System.Web.Http;

namespace GoGoAppProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            //using var appContext = new AppContext();
            //appContext.Database.Migrate();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            //// ��������, ����� �� �������������� �������� ��� ��������� ������
                            //ValidateIssuer = true,
                            //// ������, �������������� ��������
                            //ValidIssuer = AuthOptions.ISSUER,

                            //// ����� �� �������������� ����������� ������
                            //ValidateAudience = true,
                            //// ��������� ����������� ������
                            //ValidAudience = AuthOptions.AUDIENCE,
                            //// ����� �� �������������� ����� �������������
                            //ValidateLifetime = true,

                            //// ��������� ����� ������������
                            //IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            //// ��������� ����� ������������
                            //ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddSignalR();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<SynchronizationHub>("/Synchronization",
                //    options =>
                //    {
                //        options.ApplicationMaxBufferSize = 64;
                //        options.TransportMaxBufferSize = 64;
                //        options.LongPolling.PollTimeout = System.TimeSpan.FromMinutes(1);
                //        options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                //    });
            });


        }
    }
}

