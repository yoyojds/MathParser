using ONP.MathParser;
using ONP.MathParser.Context;
using System;
using Xunit;

namespace OnpTests
{
	public class MathParserTests
    {
		[Theory]
		[InlineData("2", 2)]
		[InlineData("0.45", 0.45)]
		[InlineData("2+0.45", 2.45)]
		[InlineData("10/2", 5)]
		[InlineData("7.12*0.078", 0.55536)]
		[InlineData("2+3*2", 8)]
		[InlineData("2*(2+3)", 10)]
		[InlineData("(7-2)/(10-8)", 2.5)]
		[InlineData("pi", 3.1415926535897931)]
		[InlineData("sin(0.7*pi)", 0.80901699437494745)]
		[InlineData("4.3+((0.12+3)*(4.12^(2*sin(pi/5))))", 20.781989173821533)]
		[InlineData("(pi*0.854+(0.7/(2.221*(pi-8))))", 2.6180483846941058)]
		[InlineData("sin(pow(max(2,3),b))", 0.94762984568814)]
		[InlineData("0.3+sqrt((sin(3.77/sqrt(cos(1.53*pi)+7.1)*4.787))+2.77*(0.19^pi)+0.33)", 1.1807568391571832)]
		public void WhenExpressionIsCorrect_Then_ResultIsReturned(string expression, double result)
		{
			var context = new Context();
			context.AddConst("b", 0.2);

			var mathParser = new MathParser(context);

			var calculationResult = mathParser.Calculate(expression);

			Assert.True(calculationResult.IsValid);
			Assert.True(calculationResult.Result == result);
		}

		[Theory]
		[InlineData("()")]
		[InlineData(")+(")]
		public void WhenExpressionIsNotCorrect_Then_InvalidOperationExceptionShouldBeThrown(string expression)
		{
			var context = new Context();

			var mathParser = new MathParser(context);

			Assert.Throws<InvalidOperationException>(() =>
			{
				mathParser.Parse(expression);
			});
		}
	}
}
