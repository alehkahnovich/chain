using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq {
    public sealed class ChainOne : IChain {
        private readonly IChain _next;
        public ChainOne(IChain next) => _next = next;

        public async Task Invoke(List<string> accumulator) {
            await _next.Invoke(accumulator);
            accumulator.Add(nameof(ChainOne));
        }
    }
}
