using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq {
    public class ChainThree : IChain {
        public Task Invoke(List<string> accumulator) {
            accumulator.Add(nameof(ChainThree));
            return Task.CompletedTask;
        }
    }
}