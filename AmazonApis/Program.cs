using AmazonApis.Errors;
using AmazonApis.Extensions;
using AmazonApis.Helper;
using AmazonApis.MiddleWare;
using AmazonCore.Interfaces.Repository;
using AmazonRepository.Data;
using AmazonRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat_Core.Entities;

namespace AmazonApis
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ApplicationService(builder.Configuration);
            builder.Services.UseSwaggerDoc();


            var app = builder.Build();

            #region Update Database

            await UpdateDataBaseFunc.UpdateDataBase(app);

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ErrorHandlingMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
