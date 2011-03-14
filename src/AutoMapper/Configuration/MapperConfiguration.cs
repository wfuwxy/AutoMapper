using System;
using System.Collections.Generic;

namespace AutoMapper.Configuration
{
    public class MapperConfiguration
    {
        private readonly IList<TypeMapConfiguration> _typeMaps = new List<TypeMapConfiguration>();

        public MapperConfiguration(Action<MapperRegistry> initializationExpression)
        {
            var registry = new MapperRegistry();

            initializationExpression(registry);

            registry.Apply(this);
        }

        public IEnumerable<TypeMapConfiguration> TypeMaps
        {
            get { return _typeMaps; }
        }

        public void AddTypeMap(TypeMapConfiguration typeMap)
        {
            _typeMaps.Add(typeMap);
        }

        public ConfigurationStore Build()
        {
            return new ConfigurationStore(this);
        }
    }
}