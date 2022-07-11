using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq.Disposable {
    public sealed class DisposableChainOne : IDisposableChain {
        private List<string> _accumulator;
        private readonly IDisposableChain _next;
        public DisposableChainOne(IDisposableChain next) => _next = next;

        public void Dispose() => _accumulator.Add(nameof(DisposableChainOne));

        public async Task Invoke(List<string> accumulator) {
            _accumulator = accumulator;
            await _next.Invoke(accumulator);
        }
    }
}