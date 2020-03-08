using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace W2contacts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateWebHostBuilder(args).Build();

            using (var scope = builder.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ContactsContext>();

                    // DBmigration Class checks if DB is already seeded. If not, it gets data from a legacy API and populate the databse. 
                    var myTask = DbMigration.Initialize(context);
                    var result = myTask.Result;

                    //DbMigration.Initialize(context);
                } catch (Exception ex) {

                    Console.WriteLine(ex.Message);
                }
            }
            builder.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
