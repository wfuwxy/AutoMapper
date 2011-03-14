using System;

namespace AutoMapper.Configuration
{
    public class MemberConfigurationExpression<TSource> : IMemberConfigurationExpression<TSource>
    {
        public void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter
        {
            throw new NotImplementedException();
        }

        public IFormatterCtorExpression<TValueFormatter> AddFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter
        {
            throw new NotImplementedException();
        }

        public IFormatterCtorExpression AddFormatter(Type valueFormatterType)
        {
            throw new NotImplementedException();
        }

        public void AddFormatter(IValueFormatter formatter)
        {
            throw new NotImplementedException();
        }

        public void NullSubstitute(object nullSubstitute)
        {
            throw new NotImplementedException();
        }

        public IResolverConfigurationExpression<TSource, TValueResolver> ResolveUsing<TValueResolver>() where TValueResolver : IValueResolver
        {
            throw new NotImplementedException();
        }

        public IResolverConfigurationExpression<TSource> ResolveUsing(Type valueResolverType)
        {
            throw new NotImplementedException();
        }

        public IResolutionExpression<TSource> ResolveUsing(IValueResolver valueResolver)
        {
            throw new NotImplementedException();
        }

        public void MapFrom<TMember>(Func<TSource, TMember> sourceMember)
        {
            throw new NotImplementedException();
        }

        public void Ignore()
        {
            throw new NotImplementedException();
        }

        public void SetMappingOrder(int mappingOrder)
        {
            throw new NotImplementedException();
        }

        public void UseDestinationValue()
        {
            throw new NotImplementedException();
        }

        public void UseValue<TValue>(TValue value)
        {
            throw new NotImplementedException();
        }

        public void UseValue(object value)
        {
            throw new NotImplementedException();
        }

        public void Condition(Func<TSource, bool> condition)
        {
            throw new NotImplementedException();
        }

        public void Condition(Func<ResolutionContext, bool> condition)
        {
            throw new NotImplementedException();
        }
    }
}