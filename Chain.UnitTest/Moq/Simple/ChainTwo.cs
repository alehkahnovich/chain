using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq.Simple {
    public sealed class ChainTwo : IChain {
        private readonly IChain _next;
        public ChainTwo(IChain next) => _next = next;
        public async Task Invoke(List<string> accumulator) {
            accumulator.Add(nameof(ChainTwo));
            await _next.Invoke(accumulator);
        }
    }
}
