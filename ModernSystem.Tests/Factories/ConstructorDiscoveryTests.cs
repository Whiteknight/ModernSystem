using ModernSystem.Factories;
using NUnit.Framework;

namespace ModernSystem.Tests
{
    [TestFixture]
    public class ConstructorDiscoveryTests
    {
        private class DummyClassWithDefaultParameterlessConstructor
        {

        }

        [Test]
        public void CreateInstance_DefaultParameterlessConstructor()
        {
            var result = ConstructorDiscovery.CreateInstance(typeof(DummyClassWithDefaultParameterlessConstructor), null);
            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(DummyClassWithDefaultParameterlessConstructor), result);
        }

        private class ArgumentBaseClass
        {

        }

        private class ArgumentChildClass : ArgumentBaseClass
        {

        }

        private class DummyClassWithSingleArgConstructor
        {
            public bool WithChildClass { get; private set; }
            public bool WithParentClass { get; private set; }

            public DummyClassWithSingleArgConstructor(ArgumentBaseClass arg)
            {
                WithParentClass = true;
            }

            public DummyClassWithSingleArgConstructor(ArgumentChildClass arg)
            {
                WithChildClass = true;
            }

            public DummyClassWithSingleArgConstructor(ArgumentBaseClass arg1, ArgumentChildClass arg2)
            {
                WithChildClass = true;
                WithParentClass = true;
            }
        }

        [Test]
        public void CreateInstance_ParentClassConstructor()
        {
            var arg = new ArgumentBaseClass();
            var result = ConstructorDiscovery.CreateInstance(typeof(DummyClassWithSingleArgConstructor), arg);
            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(DummyClassWithSingleArgConstructor), result);

            DummyClassWithSingleArgConstructor typed = result as DummyClassWithSingleArgConstructor;
            Assert.True(typed.WithParentClass);
            Assert.False(typed.WithChildClass);
        }

        [Test]
        public void CreateInstance_ChildClassConstructor()
        {
            var arg = new ArgumentChildClass();
            var result = ConstructorDiscovery.CreateInstance(typeof(DummyClassWithSingleArgConstructor), arg);
            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(DummyClassWithSingleArgConstructor), result);

            var typed = result as DummyClassWithSingleArgConstructor;
            Assert.False(typed.WithParentClass);
            Assert.True(typed.WithChildClass);
        }

        [Test]
        public void CreateInstance_ParentAndChildClassConstructor()
        {
            var arg1 = new ArgumentChildClass();
            var arg2 = new ArgumentChildClass();
            var result = ConstructorDiscovery.CreateInstance(typeof(DummyClassWithSingleArgConstructor), arg1, arg2);
            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(DummyClassWithSingleArgConstructor), result);

            var typed = result as DummyClassWithSingleArgConstructor;
            Assert.True(typed.WithParentClass);
            Assert.True(typed.WithChildClass);
        }

        [Test]
        public void CreateInstance_ParentAndChildClassConstructor_Ordered()
        {
            var arg1 = new ArgumentBaseClass();
            var arg2 = new ArgumentChildClass();
            var result = ConstructorDiscovery.CreateInstance(typeof(DummyClassWithSingleArgConstructor), arg1, arg2);
            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(DummyClassWithSingleArgConstructor), result);

            var typed = result as DummyClassWithSingleArgConstructor;
            Assert.True(typed.WithParentClass);
            Assert.True(typed.WithChildClass);
        }

        [Test]
        public void CreateInstance_ParentAndChildClassConstructor_NotFound()
        {
            var arg1 = new ArgumentBaseClass();
            var arg2 = new ArgumentBaseClass();
            var result = ConstructorDiscovery.CreateInstance(typeof(DummyClassWithSingleArgConstructor), arg1, arg2);

            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(DummyClassWithSingleArgConstructor), result);

            var typed = result as DummyClassWithSingleArgConstructor;
            Assert.True(typed.WithParentClass);
            Assert.False(typed.WithChildClass);
        }
    }
}