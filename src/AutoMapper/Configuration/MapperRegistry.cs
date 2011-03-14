using System;
using System.Collections.Generic;
using Castle.Core;

namespace AutoMapper.Configuration
{
    public class MapperRegistry
    {
        private readonly IList<Action<MapperConfiguration>> _configurationActions = new List<Action<MapperConfiguration>>();

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            var expr = new MappingExpression<TSource, TDestination>();

            _configurationActions.Add(cfg =>
            {
                var map = new TypeMapConfiguration(typeof(TSource), typeof(TDestination));

                expr.Apply(map);
                
                cfg.AddTypeMap(map);
            });

            return expr;
        }

        public void Apply(MapperConfiguration mapperConfiguration)
        {
            _configurationActions.ForEach(action => action(mapperConfiguration));
        }
    }
}