using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq.Disposable {
    public sealed class DisposableChainTwo : IDisposableChain {
        private List<string> _accumulator;
        public void Dispose() => _accumulator.Add(nameof(DisposableChainTwo));

        public Task Invoke(List<string> accumulator) {
            _accumulator = accumulator;
            return Task.CompletedTask;
        }
    }
}