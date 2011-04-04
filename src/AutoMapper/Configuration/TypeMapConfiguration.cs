using System;
using System.Collections.Generic;

namespace AutoMapper.Configuration
{
    public class MappingOptions : IMappingOptions
    {
        public INamingConvention SourceMemberNamingConvention { get; set; }
        public INamingConvention DestinationMemberNamingConvention { get; set; }
        public Func<string, string> SourceMemberNameTransformer { get; set; }
        public Func<string, string> DestinationMemberNameTransformer { get; set; }
    }

    public class TypeMapConfiguration
    {
        private readonly Type _sourceType;
        private readonly Type _destinationType;
        private readonly IList<TypeMemberConfiguration> _memberConfigs = new List<TypeMemberConfiguration>();

        public TypeMapConfiguration(Type sourceType, Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
        }

        public TypeMap Build(ITypeMapFactory factory, IMappingOptions mappingOptions)
        {
            var typeMap = factory.CreateTypeMap(_sourceType, _destinationType, mappingOptions);

            foreach (var memberConfig in _memberConfigs)
            {
                var accessor = memberConfig.CreateAccessor();

                var propertyMap = typeMap.FindOrCreatePropertyMapFor(accessor);

                memberConfig.Apply(propertyMap);
            }
            
            return typeMap;
        }

        public void AddMemberConfguration(TypeMemberConfiguration typeMemberConfiguration)
        {
            _memberConfigs.Add(typeMemberConfiguration);
        }
    }
}