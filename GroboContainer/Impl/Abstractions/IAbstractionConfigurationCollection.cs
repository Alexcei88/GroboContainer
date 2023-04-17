using System;
using System.Collections.Generic;

namespace GroboContainer.Impl.Abstractions
{
    public interface IAbstractionConfigurationCollection
    {
        IAbstractionConfiguration Get(Type abstractionType);
        void Add(Type abstractionType, IAbstractionConfiguration abstractionConfiguration);
        IAbstractionConfiguration[] GetAll();
        Dictionary<Type, IAbstractionConfiguration> GetAllTypes();
    }
}