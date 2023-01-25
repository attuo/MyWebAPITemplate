using MyWebAPITemplate.Tests.SharedComponents.Factories;
using Xunit;

namespace MyWebAPITemplate.Tests.FunctionalTests.Utils;

[CollectionDefinition("Test collection")]
public class SharedTestCollection : ICollectionFixture<InitializationFactory>
{

}
