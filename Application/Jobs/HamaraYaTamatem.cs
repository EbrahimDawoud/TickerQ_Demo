using TickerQ.Utilities.Base;

namespace Application.Jobs;

public class HamaraYaTamatem
{
    [TickerFunction(functionName:"hamra", "*/1 * * * *")]
    public void Hamara()
    {
        Console.WriteLine("*******************************Say 7a7a");
    }
}
