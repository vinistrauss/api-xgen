using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            // services.AddCors ();

            services.AddCors (options => {
                // options.AddPolicy (MyAllowSpecificOrigins,
                //     builder => {
                //         builder.WithOrigins ("http://3.83.242.185:3000")
                //             .AllowAnyHeader ()
                //             .AllowAnyMethod ();
                //     });

                options.AddDefaultPolicy (
                    builder => {
                        builder.WithOrigins ("http://localhost:3000",
                                "http://3.83.242.185:3000")
                            .AllowAnyHeader ()
                            .AllowAnyMethod ();
                    });
            });

            services.AddDbContext<ClientContext> (options => {
                options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection"));
            });
            services.AddScoped<IEFCoreRepository, EFCoreRepository> ();

            services.AddControllers ().AddNewtonsoftJson (options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseCors (option => option.AllowAnyOrigin ());

            // app.UseCors (MyAllowSpecificOrigins);

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}