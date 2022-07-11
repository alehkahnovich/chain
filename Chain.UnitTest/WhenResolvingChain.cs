using Chain.Extensions;
using Chain.UnitTest.Moq.Disposable;
using Chain.UnitTest.Moq.Simple;
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

            //Act.
            var chain = provider.GetRequiredService<IChain>();

            chain.Invoke(accumulator);

            //Assert.
            Assert.That(accumulator, Is.EqualTo(new[] { nameof(ChainOne), nameof(ChainTwo), nameof(ChainThree) }));
        }


        [Test]
        public void ShouldNotCreateCircularDependencies() {
            //Arrange.
            var accumulator = new List<string>();
            var provider = _collection.Chain<IChain, ChainOne>()
                .Next<ChainTwo>()
                .Next<ChainCircular>()
                .Configure()
                .BuildServiceProvider();
            
            //Act.
            var chain = provider.GetRequiredService<IChain>();

            chain.Invoke(accumulator);

            //Assert.
            Assert.That(accumulator, Is.EqualTo(new[] { nameof(ChainOne), nameof(ChainTwo), nameof(ChainCircular) }));
        }

        [Test]
        public void ShouldBeDisposable() {
            //Arrange.
            var accumulator = new List<string>();
            var provider = _collection.Chain<IDisposableChain, DisposableChainOne>(ServiceLifetime.Scoped)
                .Next<DisposableChainTwo>()
                .Configure()
                .BuildServiceProvider();

            //Act.
            using (var scope = provider.CreateScope()) {

                var chain = scope.ServiceProvider.GetRequiredService<IDisposableChain>();

                chain.Invoke(accumulator);
            }

            //Assert.
            Assert.That(accumulator, Is.EqualTo(new[] { nameof(DisposableChainOne), nameof(DisposableChainTwo) }));
        }
    }
}