// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WeConsoleDI;
using Microsoft.Extensions.Options;

IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
var envs=Environment.GetEnvironmentVariables();
hostBuilder.ConfigureAppConfiguration((HostBuilderContext context, IConfigurationBuilder configBuilder) =>
{
    configBuilder.AddCommandLine(args);
    configBuilder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",optional:false,reloadOnChange:true)
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName.ToLower()}.json",optional:true,reloadOnChange:true)
            .AddEnvironmentVariables();
    ;
});
hostBuilder.ConfigureServices((HostBuilderContext context, IServiceCollection services) =>
{
    services.Configure<ProgramOptions>(context.Configuration.GetSection(ProgramOptions.PROGRAM_OPTIONS));
    services.AddTransient<Program>();
});


var app = hostBuilder.Build();
app.Services.GetRequiredService<Program>().Run(args);

partial class  Program
{
    private readonly ProgramOptions _options;
    public Program(IOptions<ProgramOptions> o) => this._options = o.Value;
    public void Run(string []args)
    {
        Console.WriteLine("HELLO");

    }
}