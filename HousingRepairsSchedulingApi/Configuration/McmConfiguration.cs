namespace HousingRepairsSchedulingApi.Configuration;

using System;
using Flurl;

public class McmConfiguration
{
    public McmConfiguration(Url baseUrl, string username, string password)
    {
        this.BaseUrl = baseUrl;
        this.Username = username;
        this.Password = password;
    }

    public Url BaseUrl { get; }
    public string Password { get; }
    public string Username { get; }

    public static McmConfiguration FromEnv()
    {
        var username = Environment.GetEnvironmentVariable("MCM_USERNAME");
        var password = Environment.GetEnvironmentVariable("MCM_PASSWORD");
        var baseUrl = Environment.GetEnvironmentVariable("MCM_BASEURL");

        return new McmConfiguration(Url.Parse(baseUrl), username, password);
    }
}
