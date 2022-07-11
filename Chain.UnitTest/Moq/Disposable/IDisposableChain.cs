using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chain.UnitTest.Moq.Disposable {
    public interface IDisposableChain : IDisposable {
        Task Invoke(List<string> accumulator);
    }
}