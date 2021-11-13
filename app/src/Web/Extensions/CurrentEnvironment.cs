using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MyWebAPITemplate.Source.Web.Extensions;

public static class HostingEnvironmentExtensions
{
    public static bool IsLocalDocker(this IWebHostEnvironment environment)
    {
        return environment.IsEnvironment(RunningEnvironment.LocalDocker.Name);
    }

    public static bool IsQA(this IWebHostEnvironment environment)
    {
        return environment.IsEnvironment(RunningEnvironment.QA.Name);
    }
}

//public class CurrentEnvironment
//{
//    private RunningEnvironment Environment { get; }
//    public string Name { get; }

//    public CurrentEnvironment(RunningEnvironment runningEnvironment)
//    {
//        Environment = runningEnvironment ?? throw new ArgumentNullException(nameof(runningEnvironment));
//        Name = runningEnvironment.Name;
//    }

//    public bool IsLocal() => Environment.Name.Equals(RunningEnvironment.Development) || Environment.Name.Equals(RunningEnvironment.LocalDocker);
//    //public RunningEnvironment GetCurrentEnvironment() => CurrentEnvironment;
//}
