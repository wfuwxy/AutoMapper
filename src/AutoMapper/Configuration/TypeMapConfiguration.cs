using System;

namespace AutoMapper.Configuration
{
    public class TypeMapConfiguration
    {
        private readonly Type _sourceType;
        private readonly Type _destinationType;

        public TypeMapConfiguration(Type sourceType, Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
        }

        public TypeMap Build()
        {
            return new TypeMap(new TypeInfo(_sourceType), new TypeInfo(_destinationType));
        }
    }
}