namespace HousingRepairsSchedulingApi;

using System;
using System.ServiceModel;
using Configuration;
using Gateways;
using Helpers;
using HousingRepairsOnline.Authentication.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UseCases;

public class Startup
{
    private const string HousingRepairsSchedulingApiIssuerId = "Housing Management System Api";

    public Startup(IConfiguration configuration) => this.Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHousingRepairsOnlineAuthentication(HousingRepairsSchedulingApiIssuerId);

        services.AddControllers();
        services.AddTransient<IRetrieveAvailableAppointmentsUseCase, RetrieveAvailableAppointmentsUseCase>();
        services.AddTransient<IBookAppointmentUseCase, BookAppointmentUseCase>();

        this.ConfigureOptions(services);


        services.AddJobCodesMapper("jobCodes.json");

        services.AddTransient<IAppointmentsGateway, McmAppointmentGateway>(sp =>
            {
                // var drsOptions = sp.GetRequiredService<IOptions<DrsOptions>>();
                // var appointmentSearchTimeSpanInDays = drsOptions.Value.SearchTimeSpanInDays;
                // var appointmentLeadTimeInDays = drsOptions.Value.AppointmentLeadTimeInDays;
                // var maximumNumberOfRequests = drsOptions.Value.MaximumNumberOfRequests;
                return new McmAppointmentGateway(McmConfiguration.FromEnv(), sp.GetService<IJobCodesMapper>(),
                    new McmRequestFactory());
            }
        );

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HousingRepairsSchedulingApi", Version = "v1" });
            c.AddJwtSecurityScheme();
        });

        // var address = Configuration.GetSection(nameof(DrsOptions))[DrsOptionsApiAddressConfigurationKey];
        // var addressHost = new Uri(address).Host;
        services.AddHealthChecks();
        //     .AddTcpHealthCheck(options => options.AddHost(addressHost, 80), name: "DRS Host TCP Ping");
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

        //app.UseHttpsRedirection();

        app.UseRouting();

        // app.UseSentryTracing();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers().RequireAuthorization();
        });
    }

    private void ConfigureOptions(IServiceCollection services)
    {
    }

    private static HttpBindingBase CreateBinding(Uri uri)
    {
        HttpBindingBase binding;
        var uriScheme = uri.Scheme;

        if (uriScheme == Uri.UriSchemeHttps)
        {
            binding = new BasicHttpsBinding();
        }
        else if (uriScheme == Uri.UriSchemeHttp)
        {
            binding = new BasicHttpBinding();
        }
        else
        {
            throw new NotSupportedException($"Unsupported URI scheme '{uriScheme}' used by '{uri}'");
        }

        return binding;
    }
}
