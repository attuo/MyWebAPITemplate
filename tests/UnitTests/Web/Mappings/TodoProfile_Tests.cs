using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyWebAPITemplate.Web.Mappings;
using Xunit;

namespace MyWebAPITemplate.UnitTests.Web.Mappings
{
    public class TodoProfile_Tests
    {
        private readonly IMapper _mapper;

        public TodoProfile_Tests()
        {
            _mapper = new MapperConfiguration(cfg => 
            { 
                // Add all profiles here
                cfg.AddProfile<TodoProfile>(); 
            }).CreateMapper();
        }

        [Fact]
        public void Profile_Configurations_Valid() 
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
