using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq.Simple {
    public interface IChain {
        Task Invoke(List<string> accumulator);
    }
}