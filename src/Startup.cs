using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Preoff.Entity;
using Autofac;
using log4net.Repository;
using log4net.Config;
using log4net;
using Preoff.Repository;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace Preoff
{
    /// <summary>
    /// 入口
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Logrepository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Logrepository, new FileInfo("log4net.config"));
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        public static ILoggerRepository Logrepository { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                 builder => builder.WithOrigins().AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });
            
            #endregion

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            //services.AddEntityFrameworkSqlServer().AddDbContext<CoreTestContext>(opetions => opetions.UseSqlServer(Configuration.GetConnectionString("ConnDBString")));
            services.AddEntityFrameworkSqlServer().AddDbContext<PreoffContext>(opetions => opetions.UseSqlServer(Configuration.GetConnectionString("ConnDBString"),b=>b.UseRowNumberForPaging()));

            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings",jwtSettings);

            services.AddAuthentication(Options=>{
                Options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o=>{
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
                //o.SecurityTokenValidators.Clear();
                //o.SecurityTokenValidators.Add(new YanTokenValidator());
                //o.Events = new JwtBearerEvents()
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var token = context.Request.Headers["token"];
                //        context.Token = token.FirstOrDefault();
                //        return Task.CompletedTask;
                //    }

                //};

            });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("SuperAdminOnly", policy => policy.RequireClaim("SuperAdminOnly"));
            //});

            //services.AddUnitOfWork<CoreTestContext>();
            //services.AddScoped(typeof(IDemoService), typeof(DemoService));

            

            services.AddMvc();
            // 注入的实现ISwaggerProvider使用默认设置
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Api接口"
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Preoff.xml");
                c.IncludeXmlComments(xmlPath);
            });
     }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api接口");
            });

            app.UseMvc();
            app.UseStaticFiles();
            string xxx = Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory().ToString()), @"corewebapi");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory().ToString()), @"corewebapi")),
                RequestPath = new PathString("/Upload")
            });

            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<AircRepository>().As<IAircRepository>();
            builder.RegisterType<AirLoadRepository>().As<IAirLoadRepository>();
            builder.RegisterType<CameraRepository>().As<ICameraRepository>();
            builder.RegisterType<CameraTypeRepository>().As<ICameraTypeRepository>();
            builder.RegisterType<EventRepository>().As<IEventRepository>();
            builder.RegisterType<ExecTaskRepository>().As<IExecTaskRepository>();
            builder.RegisterType<DivisionRepository>().As<IDivisionRepository>();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>();
            builder.RegisterType<UnitRepository>().As<IUnitRepository>();
            builder.RegisterType<FireStationDataRepository>().As<IFireStationDataRepository>();
            builder.RegisterType<HotsPotsRepository>().As<IHotsPotsRepository>();
            builder.RegisterType<PlaceRepository>().As<IPlaceRepository>();
            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
