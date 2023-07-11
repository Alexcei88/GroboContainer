using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using GroboContainer.Impl.Abstractions.AutoConfiguration;

namespace GroboContainer.Impl.Abstractions
{
    public class AbstractionConfigurationCollection : IAbstractionConfigurationCollection
    {
        public AbstractionConfigurationCollection(IAutoAbstractionConfigurationFactory factory)
        {
            createByType = factory.CreateByType;
        }

        public IAbstractionConfiguration Get(Type abstractionType)
        {
            return cache.GetOrAdd(abstractionType, createByType);
        }

        public void Add(Type abstractionType, IAbstractionConfiguration abstractionConfiguration)
        {
            if (!cache.TryAddOrUpdate(abstractionType, abstractionConfiguration, c => c.GetImplementations().Length == 0))
                throw new InvalidOperationException($"Container is already configured for type {abstractionType}");
        }

        public IAbstractionConfiguration[] GetAll()
            => cache.Values.ToArray();

        public Dictionary<Type, IAbstractionConfiguration> GetAllTypes()
            => cache.ToDictionary(c => c.Key, c=> c.Value);

        public void Replace(Type abstractionType, IAbstractionConfiguration abstractionConfiguration)
        {
            if (!cache.TryAddOrUpdate(abstractionType, abstractionConfiguration, c => c.GetImplementations().Length > 0))
                throw new InvalidOperationException($"Container is already configured for type {abstractionType}");
        }

        private readonly Func<Type, IAbstractionConfiguration> createByType;
        private readonly ConcurrentDictionary<Type, IAbstractionConfiguration> cache = new();
    }
}