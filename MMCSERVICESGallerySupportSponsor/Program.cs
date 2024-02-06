

using Application.Interfaces;
using Application.Mapper;
using Application.SupportsCQRS.Queries;
using Infrastrecture.Db;
using Infrastrecture.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MMCGallerySupportSponsor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			var config = builder.Configuration;
			// Add services to the container.

			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}
			).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = config["JwtSettings:Issuer"],
					ValidAudience = config["JwtSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(
					 Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true
				};
			});

			builder.Services.AddAuthorization();
			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DbContextApplication>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("dbconnection")));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(GetSupportQueryHandler).Assembly));
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();

            app.Run();
        }
    }
}