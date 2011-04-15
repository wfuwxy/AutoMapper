using System;

namespace AutoMapper.Configuration
{
    public interface IMapFromConfiguration
    {
        void Apply(PropertyMap propertyMap);
        Delegate SourceMember { get; }
    }
}