namespace HousingRepairsSchedulingApi;

using Configuration;
using Gateways;
using Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UseCases;

public class Startup
{
    public Startup(IConfiguration configuration) => this.Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddTransient<IRetrieveAvailableAppointmentsUseCase, RetrieveAvailableAppointmentsUseCase>();
        services.AddTransient<IBookAppointmentUseCase, BookAppointmentUseCase>();

        services.AddJobCodesMapper("jobCodes.json");

        services.AddTransient<IAppointmentsGateway, McmAppointmentGateway>(sp =>
            {
                return new McmAppointmentGateway(sp.GetRequiredService<ILogger<McmAppointmentGateway>>(),
                    McmConfiguration.FromEnv(), sp.GetService<IJobCodesMapper>(),
                    new McmRequestFactory());
            }
        );

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HousingRepairsSchedulingApi", Version = "v1" });
        });

        services.AddHealthChecks();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HousingRepairsSchedulingApi v1"));
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers();
        });
    }
}
