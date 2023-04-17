using System;

namespace GroboContainer.Core;

public record ServiceDescriptor(Type AbstractionType, Type ImplementationType, bool AutoImplementation, bool HasManyImplementations);
