using ONP.MathParser;
using ONP.MathParser.Context;
using System;

namespace OnpConsole
{
	class Program
    {
        static void Main(string[] args)
        {
			IContext context = new Context();
			context.AddConst("a", 0.2);
			context.AddConst("b", 0.3);
			context.AddConst("c", -0.3);

			var mathParser = new MathParser(context);

			try
			{
				var result = mathParser.Calculate("2+3*4");

				if (result.IsValid)
					Console.WriteLine($"result = {result.Result}");
				else
					Console.WriteLine(String.Join(Environment.NewLine, result.Errors));
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
        }
    }
}
