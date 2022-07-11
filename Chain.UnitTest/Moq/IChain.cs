using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq {
    public interface IChain {
        Task Invoke(List<string> accumulator);
    }
}
