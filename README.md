# README
![example workflow](https://github.com/alehkahnovich/chain/actions/workflows/dotnet.yml/badge.svg)
## Chain declaration
```c#
public interface IChain {
    Task Invoke(List<string> accumulator);
}

public sealed class ChainOne : IChain {
    private readonly IChain _next;
    public ChainOne(IChain next) => _next = next;

    public async Task Invoke(List<string> accumulator) {
        await _next.Invoke(accumulator);
        accumulator.Add(nameof(ChainOne));
    }
}

public sealed class ChainTwo : IChain {
    private readonly IChain _next;
    public ChainTwo(IChain next) => _next = next;
    public async Task Invoke(List<string> accumulator) {
        await _next.Invoke(accumulator);
        accumulator.Add(nameof(ChainTwo));
    }
}

public class ChainThree : IChain {
    public Task Invoke(List<string> accumulator) {
        accumulator.Add(nameof(ChainThree));
        return Task.CompletedTask;
    }
}
```

## Usage example
```c#
var provider = _collection.Chain<IChain, ChainOne>()
                .Next<ChainTwo>()
                .Next<ChainThree>()
                .Configure()
                .BuildServiceProvider();

var chain = provider.GetRequiredService<IChain>();
```
