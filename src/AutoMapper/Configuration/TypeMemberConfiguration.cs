using System;

namespace AutoMapper.Configuration
{
    public class TypeMemberConfiguration<TSource>
    {
        public IMapFromConfiguration SourceMemberConfiguration { get; private set; }

        public IMemberAccessor CreateAccessor()
        {
            return null;
        }

        public void Apply(PropertyMap propertyMap)
        {
            if (SourceMemberConfiguration != null)
                SourceMemberConfiguration.Apply(propertyMap);
        }

        public void MapFrom<TDestination>(Func<TSource, TDestination> sourceMember)
        {
            SourceMemberConfiguration = new MapFromConfiguration<TDestination>(sourceMember);
        }

        private class MapFromConfiguration<TDestination> : IMapFromConfiguration
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
}