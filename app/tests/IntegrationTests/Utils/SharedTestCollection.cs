using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyWebAPITemplate.Tests.IntegrationTests.Utils;

[CollectionDefinition("Test collection")]
public class SharedTestCollection : ICollectionFixture<CustomFactory>
{

}
