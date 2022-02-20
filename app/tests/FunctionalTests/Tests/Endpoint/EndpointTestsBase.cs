using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MyWebAPITemplate.Tests.FunctionalTests.Utils;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Tests.Endpoint;

public abstract class EndpointTestsBase : IClassFixture<TestFixture>
{
    private const string BASE_ADDRESS = "https://localhost:5001/"; // TODO: This should not be hard coded

    public HttpClient Client { get; init; }

    public EndpointTestsBase(TestFixture fixture)
    {
        Client = fixture.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri(BASE_ADDRESS)
        });
    }
}