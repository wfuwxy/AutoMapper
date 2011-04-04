using System;
using System.Collections.Generic;
using AutoMapper.Configuration;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using System.Linq;

namespace AutoMapper.UnitTests
{
	namespace General
	{
		public class When_mapping_dto_with_a_missing_match : NonValidatingSpecBase
		{
		    private ConfigurationStore _store;

		    public class ModelObject
			{
			}

			public class ModelDto
			{
				public string SomePropertyThatDoesNotExistOnModel { get; set; }
			}

			protected override void Establish_context()
			{
			    _store = new ConfigurationStore(new MapperConfiguration(r =>
			    {
			        r.CreateMap<ModelObject, ModelDto>();
			    }));
			}

		    [Test]
			public void Should_map_successfully()
			{
			    var engine = new MappingEngine(_store);

                ModelDto dto = engine.Map<ModelObject, ModelDto>(new ModelObject());

				dto.ShouldNotBeNull();
			}
		}

		public class When_mapping_a_null_model : AutoMapperSpecBase
		{
			private ModelDto _result;

			public class ModelDto
			{
			}

			public class ModelObject
			{
			}

			protected override void Establish_context()
			{
                var store = new ConfigurationStore(new MapperConfiguration(r =>
                {
                    r.NullDestinationValues(false);
                    r.CreateMap<ModelObject, ModelDto>();
                }));

			    var engine = new MappingEngine(store);
                
                _result = engine.Map<ModelObject, ModelDto>(null);
			}

			[Test]
			public void Should_always_provide_a_dto()
			{
				_result.ShouldNotBeNull();
			}
		}

		public class When_mapping_a_dto_with_a_private_parameterless_constructor : AutoMapperSpecBase
		{
			private ModelDto _result;

			public class ModelObject
			{
				public string SomeValue { get; set; }
			}

			public class ModelDto
			{
				public string SomeValue { get; set; }

				private ModelDto()
				{
				}
			}

			protected override void Establish_context()
			{
                var store = new ConfigurationStore(new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>();
                }));

                var engine = new MappingEngine(store);

				var model = new ModelObject
				{
					SomeValue = "Some value"
				};

                _result = engine.Map<ModelObject, ModelDto>(model);
			}

			[Test]
			public void Should_map_the_dto_value()
			{
				_result.SomeValue.ShouldEqual("Some value");
			}
		}

		public class When_mapping_to_a_dto_string_property_and_the_dto_type_is_not_a_string : AutoMapperSpecBase
		{
			private ModelDto _result;

			public class ModelObject
			{
				public int NotAString { get; set; }
			}

			public class ModelDto
			{
				public string NotAString { get; set; }
			}

			protected override void Establish_context()
			{
				var model = new ModelObject
				{
					NotAString = 5
				};

                var store = new ConfigurationStore(new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>();
                }));

                var engine = new MappingEngine(store);

                _result = engine.Map<ModelObject, ModelDto>(model);
			}

			[Test]
			public void Should_use_the_ToString_value_of_the_unmatched_type()
			{
				_result.NotAString.ShouldEqual("5");
			}
		}

		public class When_mapping_dto_with_an_array_property : AutoMapperSpecBase
		{
			private ModelDto _result;

			public class ModelObject
			{
				public IEnumerable<int> GetSomeCoolValues()
				{
					return new[] { 4, 5, 6 };
				}
			}

			public class ModelDto
			{
				public string[] SomeCoolValues { get; set; }
			}

			protected override void Establish_context()
			{
				var model = new ModelObject();

                var config = new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>();
                });

                var engine = new MappingEngine(config.Build());

                _result = engine.Map<ModelObject, ModelDto>(model);
			}

			[Test]
			public void Should_map_the_collection_of_items_in_the_input_to_the_array()
			{
				_result.SomeCoolValues[0].ShouldEqual("4");
				_result.SomeCoolValues[1].ShouldEqual("5");
				_result.SomeCoolValues[2].ShouldEqual("6");
			}
		}

		public class When_mapping_a_dto_with_mismatched_property_types : NonValidatingSpecBase
		{
		    private MappingEngine _engine;

		    public class ModelObject
			{
				public string NullableDate { get; set; }
			}

			public class ModelDto
			{
				public DateTime NullableDate { get; set; }
			}

			protected override void Establish_context()
			{
			    var config = new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>();
                });

			    _engine = new MappingEngine(config.Build());
			}

		    [Test]
			public void Should_throw_a_mapping_exception()
			{
				var model = new ModelObject();
				model.NullableDate = new DateTime(2007, 8, 4).ToString();

                typeof(AutoMapperMappingException).ShouldBeThrownBy(() => _engine.Map<ModelObject, ModelDto>(model));
			}
		}

		public class When_mapping_an_array_of_model_objects : AutoMapperSpecBase
		{
			private ModelObject[] _model;
			private ModelDto[] _dto;

			public class ModelObject
			{
				public string SomeValue { get; set; }
			}

			public class ModelDto
			{
				public string SomeValue { get; set; }
			}

			protected override void Establish_context()
			{
                var config = new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>();
                });

			    var engine = new MappingEngine(config.Build());

				_model = new[] { new ModelObject { SomeValue = "First" }, new ModelObject { SomeValue = "Second" } };
                _dto = (ModelDto[])engine.Map(_model, typeof(ModelObject[]), typeof(ModelDto[]));
			}

			[Test]
			public void Should_create_an_array_of_ModelDto_objects()
			{
				_dto.Length.ShouldEqual(2);
			}

			[Test]
			public void Should_map_properties()
			{
                _dto.Any(d => d.SomeValue.Contains("First")).ShouldBeTrue();
                _dto.Any(d => d.SomeValue.Contains("Second")).ShouldBeTrue();
            }
		}

		public class When_mapping_a_List_of_model_objects : AutoMapperSpecBase
		{
			private List<ModelObject> _model;
			private ModelDto[] _dto;

			public class ModelObject
			{
				public string SomeValue { get; set; }
			}

			public class ModelDto
			{
				public string SomeValue { get; set; }
			}

			protected override void Establish_context()
			{
                var config = new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>();
                });

                var engine = new MappingEngine(config.Build());

				_model = new List<ModelObject> { new ModelObject { SomeValue = "First" }, new ModelObject { SomeValue = "Second" } };
				_dto = (ModelDto[])engine.Map(_model, typeof(ModelObject[]), typeof(ModelDto[]));
			}

			[Test]
			public void Should_create_an_array_of_ModelDto_objects()
			{
				_dto.Length.ShouldEqual(2);
			}

			[Test]
			public void Should_map_properties()
			{
                _dto.Any(d => d.SomeValue.Contains("First")).ShouldBeTrue();
                _dto.Any(d => d.SomeValue.Contains("Second")).ShouldBeTrue();
			}
		}

		public class When_mapping_a_nullable_type_to_non_nullable_type : AutoMapperSpecBase
		{
			private ModelObject _model;
			private ModelDto _dto;
		    private MappingEngine _engine;

		    public class ModelObject
			{
				public int? SomeValue { get; set; }
				public int? SomeNullableValue { get; set; }
			}

			public class ModelDto
			{
				public int SomeValue { get; set; }
				public int SomeNullableValue { get; set; }
			}

			protected override void Establish_context()
			{
			    var config = new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>();
                });

			    _engine = new MappingEngine(config.Build());
			}

		    protected override void Because_of()
            {
                _model = new ModelObject { SomeValue = 2 };
                _dto = _engine.Map<ModelObject, ModelDto>(_model);
            }

			[Test]
			public void Should_map_value_if_has_value()
			{
				_dto.SomeValue.ShouldEqual(2);
			}

			[Test]
			public void Should_not_set_value_if_null()
			{
				_dto.SomeNullableValue.ShouldEqual(0);
			}
		}

		public class When_mapping_a_non_nullable_type_to_a_nullable_type : AutoMapperSpecBase
		{
			private ModelObject _model;
			private ModelDto _dto;
		    private MappingEngine _engine;

		    public class ModelObject
			{
				public int SomeValue { get; set; }
				public int SomeOtherValue { get; set; }
			}

			public class ModelDto
			{
				public int? SomeValue { get; set; }
				public int? SomeOtherValue { get; set; }
			}

			protected override void Establish_context()
			{
                var config = new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>();
                });

                _engine = new MappingEngine(config.Build());

				_model = new ModelObject { SomeValue = 2 };
			}

			protected override void Because_of()
			{
				_dto = _engine.Map<ModelObject, ModelDto>(_model);
			}

			[Test]
			public void Should_map_value_if_has_value()
			{
				_dto.SomeValue.ShouldEqual(2);
			}

			[Test]
			public void Should_not_set_value_if_null()
			{
				_dto.SomeOtherValue.ShouldEqual(0);
			}

		}

        public class When_mapping_a_nullable_type_to_a_nullable_type : AutoMapperSpecBase
        {
            private ModelObject _model;
            private ModelDto _dto;
            private MappingEngine _engine;

            public class ModelObject
            {
                public int? SomeValue { get; set; }
                public int? SomeOtherValue { get; set; }
            }

            public class ModelDto
            {
                public int? SomeValue { get; set; }
                public int? SomeOtherValue2 { get; set; }
            }

            protected override void Establish_context()
            {
                var config = new MapperConfiguration(r =>
                {
                    r.CreateMap<ModelObject, ModelDto>()
                        .ForMember(dest => dest.SomeOtherValue2, opt => opt.MapFrom(src => src.SomeOtherValue));
                });

                _engine = new MappingEngine(config);

                _model = new ModelObject();
            }

            protected override void Because_of()
            {
                _dto = _engine.Map<ModelObject, ModelDto>(_model);
            }

            [Test]
            public void Should_map_value_if_has_value()
            {
                _dto.SomeValue.ShouldBeNull();
            }

            [Test]
            public void Should_not_set_value_if_null()
            {
                _dto.SomeOtherValue2.ShouldBeNull();
            }

        }
    }
}
