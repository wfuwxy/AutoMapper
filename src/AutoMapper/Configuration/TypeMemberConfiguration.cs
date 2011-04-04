using System;
using System.Collections.Generic;

namespace AutoMapper.Configuration
{
    public class TypeMemberConfiguration<TSource>
    {
        public IMemberAccessor CreateAccessor()
        {
            return null;
        }

        public void Apply(PropertyMap propertyMap)
        {
        }

        public void MapFrom(Func<TSource, object> sourceMember)
        {
            _propertyMapActions.Add(pm => pm.AssignCustomValueResolver(new DelegateBasedResolver<TSource>(sourceMember)));
        }
    }
}