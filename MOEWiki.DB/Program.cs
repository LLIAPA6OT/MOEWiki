﻿using MOEWiki.DB.Models;

public class Program
{
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();

    // EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
        => services.AddDbContext<ApplicationContext>();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}
//BccC1ofJZIRVYwz9