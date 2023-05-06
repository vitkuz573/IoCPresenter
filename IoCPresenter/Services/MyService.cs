using IoCPresenter.Abstractions;

namespace IoCPresenter.Services;

public class MyService : IMyService
{
    public string GetMessage()
    {
        return "Hello from MyService!";
    }
}
