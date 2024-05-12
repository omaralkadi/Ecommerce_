using AmazonApis.Errors;
using AmazonCore;
using AmazonCore.Entities.Identity;
using AmazonCore.Interfaces.Repository;
using AmazonCore.Services;
using AmazonRepository.Data;
using AmazonRepository.Data.Identity;
using AmazonRepository.Repository;
using AmazonService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

namespace AmazonApis.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection ApplicationService(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());


            Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (context) =>
                {
                    var Errors = context.ModelState.Where(e => e.Value.Errors.Count() > 0).
                    SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                    var ValidationErrorRespone = new ApiValidationErrorReponse()
                    {
                        Errors = Errors
                    };
                    return new BadRequestObjectResult(ValidationErrorRespone);

                };
            });

            Services.AddSingleton<IConnectionMultiplexer>(e =>
            {
                var Connection = Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });

            Services.AddScoped<IBasketRepository, BasketRepository>();

            Services.AddDbContext<IdentityDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            Services.AddIdentity<AppUser, IdentityRole>().
                AddEntityFrameworkStores<IdentityDataContext>();


            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
                AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidateAudience= true,
                        ValidAudience = Configuration["JWT:Audience"],
                        ValidateLifetime= true,
                        ValidateIssuerSigningKey= true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:key"]))

                };
                }); //userManager / SingInManager / RoleManager

            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IOrderService, OrderService>();
            return Services;
        }

    }
}
