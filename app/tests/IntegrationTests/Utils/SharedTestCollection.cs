using MyWebAPITemplate.Tests.SharedComponents.Factories;
using Xunit;

namespace MyWebAPITemplate.Tests.IntegrationTests.Utils;

[CollectionDefinition("Test collection")]
public class SharedTestCollection : ICollectionFixture<InitializationFactory>
{

}