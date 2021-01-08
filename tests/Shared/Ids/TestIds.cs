using System;

namespace MyWebAPITemplate.Tests.UnitTests.Shared.Ids
{

    public static class TestIds
    {
        /// <summary>
        /// For example for getting and updating
        /// </summary>
        public static Guid NormalUsageId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        /// <summary>
        /// For example for deleting
        /// </summary>
        public static Guid OtherUsageId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        /// <summary>
        /// For example not found
        /// </summary>
        public static Guid NonUsageId = Guid.Parse("99999999-9999-9999-9999-999999999999");

    }
}
