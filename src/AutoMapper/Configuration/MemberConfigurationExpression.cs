using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoMapper.Configuration
{
    public class MemberConfigurationExpression<TSource> : IMemberConfigurationExpression<TSource>
    {
        private readonly LambdaExpression _destinationMember;
        private readonly Action<IMemberConfigurationExpression<TSource>> _memberOptions;
        private readonly IList<Action<TypeMemberConfiguration<TSource>>> _memberActions = new List<Action<TypeMemberConfiguration>>();

        public MemberConfigurationExpression(LambdaExpression destinationMember, Action<IMemberConfigurationExpression<TSource>> memberOptions)
        {
            _destinationMember = destinationMember;
            _memberOptions = memberOptions;
        }

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
            _memberActions.Add(cfg => cfg.MapFrom(sourceMember));
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

        public void Apply(TypeMemberConfiguration typeMemberConfiguration)
        {
            _memberOptions(this);
        }
    }
}