﻿using System;

using GroboContainer.Core;

namespace Tests
{
    public class TestClassWrapperCreator: IClassWrapperCreator
    {
        public Type Wrap(Type implementationType)
        {
            return null;
        }

        public object UnWrap(object instance)
        {
            return instance;
        }
    }
}