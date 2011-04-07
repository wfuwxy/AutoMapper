using System;
using System.Linq.Expressions;

namespace AutoMapper.Configuration
{
    public class TypeMemberConfiguration
    {
        public IMapFromConfiguration SourceMemberConfiguration { get; private set; }
        public LambdaExpression DestinationMemberExpr { get; private set; }

        public IMemberAccessor CreateAccessor()
        {
            var memberInfo = ReflectionHelper.FindProperty(DestinationMemberExpr);
		    IMemberAccessor destProperty = memberInfo.ToMemberAccessor();

            return destProperty;
        }

        public void Apply(PropertyMap propertyMap)
        {
            if (SourceMemberConfiguration != null)
                SourceMemberConfiguration.Apply(propertyMap);
        }

        public void MapFrom<TSource, TDestination>(Func<TSource, TDestination> sourceMember)
        {
            SourceMemberConfiguration = new MapFromConfiguration<TSource, TDestination>(sourceMember);
        }

        public void DestinationMember(LambdaExpression destinationMember)
        {
            DestinationMemberExpr = destinationMember;
        }
    }

    public class MapFromConfiguration<TSource, TDestination> : IMapFromConfiguration
    {
        private readonly Func<TSource, TDestination> _sourceMember;

        public MapFromConfiguration(Func<TSource, TDestination> sourceMember)
        {
            _sourceMember = sourceMember;
        }

        public void Apply(PropertyMap propertyMap)
        {
            propertyMap.AssignCustomValueResolver(new DelegateBasedResolver<TSource, TDestination>(_sourceMember));
        }

        public Delegate SourceMember
        {
            get { return _sourceMember; }
        }
    }
}