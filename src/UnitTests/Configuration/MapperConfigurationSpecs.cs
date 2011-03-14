using System.Linq;
using AutoMapper.Configuration;
using Machine.Specifications;

namespace AutoMapper.UnitTests.Configuration
{
    namespace MapperConfigurationSpecs
    {
        [Subject("Basic configuration")]
        public class when_configuring_two_flat_types
        {
            static MapperConfiguration _configuration;
            static ConfigurationStore _store;
            static MappingEngine _engine;

            public class Source
            {
                public int Value { get; set; }
            }

            public class Destination
            {
                public int Value { get; set; }
            }

            Establish context = () =>
            {
                _configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Source, Destination>();
                });
            };

            Because of = () =>
            {
                _store = _configuration.Build();
                _engine = new MappingEngine(_store);
            };

            It should_record_the_type_map = () =>
            {
                _configuration.TypeMaps.Count().ShouldEqual(1);
            };

            It should_build_the_type_maps = () =>
            {
                _store.ShouldNotBeNull();
                _store.GetAllTypeMaps().Length.ShouldEqual(1);
            };
        }
    }
}