using System.Drawing;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Models;

namespace Application.Jobs;

 public class JobWithData
{
    [TickerFunction("CleanUp")]
    public void CleanUp(TickerFunctionContext<Point> ticker)
    {
        Console.WriteLine($"///////////////////////////////////////X: {ticker.Request.X}, Y: {ticker.Request.Y}");

    }
}
