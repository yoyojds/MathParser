using ONP.MathParser.Parsers;
using System;
using Xunit;

namespace OnpTests.Parsers
{
	public class SubexpressionTermParserTests
    {
		[Theory]
		[InlineData("(2+(4.5))", "2+(4.5)")]
		[InlineData("((sin(3.77/sqrt(cos(1.53*pi))*4.787))+2.77*(0.19^pi))", "(sin(3.77/sqrt(cos(1.53*pi))*4.787))+2.77*(0.19^pi)")]
		public void WhenSubexpressionIsPassed_Then_CorrecTermIsCreated(string expression, string expectedTerm)
		{
			var sut = new SubExpressionTermParser();

			var result = sut.Parse(expression, 0);

			Assert.True(result.TermType == TermType.Expression);
			Assert.True(result.Term == expectedTerm);
		}

		[Theory]
		[InlineData("((2+(4.5))")]
		[InlineData("()")]
		public void WhenSubexpressionIsEmptyOrHasNotMatchingParenthesis_Then_ExceptionShouldBeThrown(string expression)
		{
			var sut = new SubExpressionTermParser();

			Assert.Throws<InvalidOperationException>(() =>
			{
				sut.Parse(expression, 0);
			});
		}
	}
}
