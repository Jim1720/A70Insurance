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
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredSessionStorage();

            Env.url = builder.Configuration.GetValue<String>("UrlPrefix");

            Env.usingStyles = builder.Configuration.GetValue<String>("UseStyles");

            const string yes = "Y";

            Env.usingStay = builder.Configuration.GetValue<string>("UseStay") == yes;

            Env.usingFocus = builder.Configuration.GetValue<string>("UseFocus") == yes;

            Env.usingNav = builder.Configuration.GetValue<string>("UseNav") == yes;

            Env.usingActions = builder.Configuration.GetValue<string>("UseActions") == yes;


            builder.Services.AddScoped<IScreenStyleFactory, ScreenStyleFactory>();

            builder.Services.AddScoped<IScreenStyleList, ScreenStyleList>();

            builder.Services.AddScoped<IScreenStyleManager, ScreenStyleManager>();

            builder.Services.AddScoped<IEditDate, EditDate>();

            builder.Services.AddScoped<IActionOperations, ActionOperations>();

            builder.Services.AddScoped<IFocusedClaim, FocusedClaim>();

            builder.Services.AddScoped<IHistorySettings, HistorySettings>();


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
