global using DCSoft.WinForm2WASM;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using MWGAWinFormDemo;

[assembly: System.Reflection.AssemblyMetadata("IsTrimmable", "false")]

namespace DCSoft.WinForm2WASM
{
    [System.Runtime.Versioning.SupportedOSPlatform("browser")]
    internal class Prograss
    {
        public static async Task Main(string[] args)
        {
            // 1. 构建极简宿主（仅初始化核心服务，不含 RootComponent）
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var host = builder.Build();
            var jsRuntime = host.Services.GetRequiredService<IJSRuntime>() as JSInProcessRuntime;
            await MWGAEngine.Start(jsRuntime);
            await host.RunAsync();
        }
    }
}