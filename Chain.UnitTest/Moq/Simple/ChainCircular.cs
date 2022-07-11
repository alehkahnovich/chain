using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq.Simple {
    public sealed class ChainCircular : IChain {
        private readonly IChain _next;

        public ChainCircular(IChain next) => _next = next;

        public async Task Invoke(List<string> accumulator) {
            accumulator.Add(nameof(ChainCircular));
            if (_next != null) await _next.Invoke(accumulator);
        }
    }
}