using Chain.Extensions;
using Chain.UnitTest.Moq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;

namespace Chain.UnitTest {
    public class WhenResolvingChain {
        private IServiceCollection _collection;
        [SetUp]
        public void Setup() => _collection = new ServiceCollection();

        [TearDown]
        public void TearDown() => _collection.Clear();

        [Test]
        public void ShouldInvokeInCorrectOrder() {
            //Arrange.
            var accumulator = new List<string>();
            var provider = _collection.Chain<IChain, ChainOne>()
                .Next<ChainTwo>()
                .Next<ChainThree>()
                .Configure()
                .BuildServiceProvider();

            var chain = provider.GetRequiredService<IChain>();

            //Act.
            chain.Invoke(accumulator);

            //Assert.
            Assert.That(accumulator, Is.EqualTo(new[] { nameof(ChainThree), nameof(ChainTwo), nameof(ChainOne) }));
        }
    }
}