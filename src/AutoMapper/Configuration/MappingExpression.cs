using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoMapper.Configuration
{
    public class MappingExpression<TSource, TDestination> : IMappingExpression<TSource, TDestination>
    {
        private readonly IList<Action<TypeMapConfiguration>> _configActions = new List<Action<TypeMapConfiguration>>();

        public IMappingExpression<TSource, TDestination> ForMember(Expression<Func<TDestination, object>> destinationMember, Action<IMemberConfigurationExpression<TSource>> memberOptions)
        {
            _configActions.Add(cfg =>
            {
                var expr = new MemberConfigurationExpression<TSource>(destinationMember);

                memberOptions(expr);

                var memberCfg = new TypeMemberConfiguration();

                expr.Apply(memberCfg);

                cfg.AddMemberConfguration(memberCfg);
            });

            return this;
        }

        public IMappingExpression<TSource, TDestination> ForMember(string name, Action<IMemberConfigurationExpression<TSource>> memberOptions)
        {
            throw new NotImplementedException();
        }

        public void ForAllMembers(Action<IMemberConfigurationExpression<TSource>> memberOptions)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> Include<TOtherSource, TOtherDestination>() where TOtherSource : TSource where TOtherDestination : TDestination
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> WithProfile(string profileName)
        {
            throw new NotImplementedException();
        }

        public void ConvertUsing(Func<TSource, TDestination> mappingFunction)
        {
            throw new NotImplementedException();
        }

        public void ConvertUsing(ITypeConverter<TSource, TDestination> converter)
        {
            throw new NotImplementedException();
        }

        public void ConvertUsing<TTypeConverter>() where TTypeConverter : ITypeConverter<TSource, TDestination>
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> beforeFunction)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> afterFunction)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> ConstructUsing(Func<TSource, TDestination> ctor)
        {
            throw new NotImplementedException();
        }

        public void Apply(TypeMapConfiguration typeMapConfiguration)
        {
            foreach (var configAction in _configActions)
            {
                configAction(typeMapConfiguration);
            }
        }
    }
}