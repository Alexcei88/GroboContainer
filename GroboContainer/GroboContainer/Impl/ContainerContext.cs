using System;

using GroboContainer.Core;
using GroboContainer.Impl.Abstractions;
using GroboContainer.Impl.Abstractions.AutoConfiguration;
using GroboContainer.Impl.ClassCreation;
using GroboContainer.Impl.Implementations;
using GroboContainer.New;

namespace GroboContainer.Impl
{
    public class ContainerContext : IContainerContext
    {
        public ContainerContext(IContainerConfiguration configuration, IClassWrapperCreator classWrapperCreator)
        {
            typesHelper = new TypesHelper();

            var funcHelper = new FuncHelper();
            FuncBuilder = new FuncBuilder();
            var classCreator = new ClassCreator(funcHelper);
            var constructorSelector = new ConstructorSelector();
            CreationContext = new CreationContext(classCreator, constructorSelector, classWrapperCreator);

            var implementationTypesCollection = new ImplementationTypesCollection(configuration, typesHelper);
            var implementationCache = new ImplementationCache();
            abstractionsCollection = new AbstractionsCollection(implementationTypesCollection, implementationCache); //g
            var implementationConfigurationCache = new ImplementationConfigurationCache(); //l
            var factory = new AutoAbstractionConfigurationFactory(typesHelper, abstractionsCollection,
                                                                  implementationConfigurationCache);
            AbstractionConfigurationCollection = new AbstractionConfigurationCollection(factory);
            AbstractionConfigurationCollection.Add(typeof(IContainer),
                                                   new StupidAbstractionConfiguration(
                                                       new ContainerImplementationConfiguration()));
        }

        #region IContainerContext Members

        public IFuncBuilder FuncBuilder { get; private set; } //s, no state

        public ICreationContext CreationContext { get; private set; } //s, no statexx
        public IAbstractionConfigurationCollection AbstractionConfigurationCollection { get; private set; }

        public IContainerContext MakeChildContext()
        {
            throw new NotSupportedException("Childs");
        }

        #endregion

        private readonly IAbstractionsCollection abstractionsCollection;
        private readonly ITypesHelper typesHelper;
    }
}