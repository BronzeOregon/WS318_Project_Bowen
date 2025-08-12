using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WS318_Project_Bowen
{
    internal class DatabaseDbContext : DbContext
    {


        private readonly string DbConnection = @"G:\\School_C#\\WS318_Project_Bowen\\Database1.mdf";
        private string DbBaseDomain = AppDomain.CurrentDomain.BaseDirectory;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = Database1; Trusted_Connection = True;",
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                            );
                    });
                //optionsBuilder.UseSqlServer(DbConnection);
                Debug.WriteLine("Connection Successful.");
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
                       

        }

    }
}
