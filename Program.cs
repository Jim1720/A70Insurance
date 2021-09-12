using A70Insurance.Models;
using A70Insurance.StyleFeature;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace A70Insurance
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredSessionStorage(); 

            Env.url = builder.Configuration.GetValue<String>("UrlPrefix");

            Env.usingStyles = builder.Configuration.GetValue<String>("UseStyles");

            builder.Services.AddScoped<IScreenStyleFactory, ScreenStyleFactory>();

            builder.Services.AddScoped<IScreenStyleList, ScreenStyleList>();

            builder.Services.AddScoped<IScreenStyleManager, ScreenStyleManager>();

            builder.Services.AddScoped<IEditDate, EditDate>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
