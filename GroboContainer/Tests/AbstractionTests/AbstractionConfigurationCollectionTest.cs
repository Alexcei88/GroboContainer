using System;
using GroboContainer.Impl.Abstractions;
using GroboContainer.Impl.Abstractions.AutoConfiguration;
using NUnit.Framework;
using Rhino.Mocks;
using TestCore;

namespace Tests.AbstractionTests
{
    public class AbstractionConfigurationCollectionTest : CoreTestBase
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();
            factory = GetMock<IAutoAbstractionConfigurationFactory>();
            configurationCollection = new AbstractionConfigurationCollection(factory);
        }

        #endregion

        private IAutoAbstractionConfigurationFactory factory;
        private AbstractionConfigurationCollection configurationCollection;

        [Test]
        public void TestAdd()
        {
            var configuration = NewMock<IAbstractionConfiguration>();
            configurationCollection.Add(typeof (int), configuration);
            Assert.AreSame(configuration, configurationCollection.Get(typeof (int)));
            Assert.AreSame(configuration, configurationCollection.Get(typeof (int)));
            RunMethodWithException<InvalidOperationException>(
                () => configurationCollection.Add(typeof (int), configuration),
                "��� System.Int32 ��� ���������������");
        }

        [Test]
        public void TestBig()
        {
            var configurationIntShort = NewMock<IAbstractionConfiguration>();
            var configurationIntInt = NewMock<IAbstractionConfiguration>();
            var configurationLong = NewMock<IAbstractionConfiguration>();
            configurationCollection.Add(typeof (short), configurationIntShort);
            factory.Expect(f => f.CreateByType(typeof (int))).Return(configurationIntInt);
            Assert.AreSame(configurationIntInt, configurationCollection.Get(typeof (int)));
            Assert.AreSame(configurationIntShort, configurationCollection.Get(typeof (short)));

            //factory.ExpectCreateByType(typeof (long), new[] {"a"}, configurationLong);
            factory.Expect(f => f.CreateByType(typeof (long))).Return(configurationLong);
            Assert.AreSame(configurationLong, configurationCollection.Get(typeof (long)));
        }

        [Test]
        public void TestCreate()
        {
            var configuration = NewMock<IAbstractionConfiguration>();
            factory.Expect(f => f.CreateByType(typeof (int))).Return(configuration);
            Assert.AreSame(configuration, configurationCollection.Get(typeof (int)));
            Assert.AreSame(configuration, configurationCollection.Get(typeof (int)));
        }

        [Test]
        public void TestGetAll()
        {
            CollectionAssert.IsEmpty(configurationCollection.GetAll());

            var configuration1 = NewMock<IAbstractionConfiguration>();
            var configuration2 = NewMock<IAbstractionConfiguration>();
            configurationCollection.Add(typeof (string), null); //hack, ���� �� != null
            configurationCollection.Add(typeof (int), configuration1);
            configurationCollection.Add(typeof (long), configuration2);

            CollectionAssert.AreEquivalent(new[] {null, configuration1, configuration2},
                                           configurationCollection.GetAll());
        }
    }
}