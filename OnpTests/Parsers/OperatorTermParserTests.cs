using ONP.MathParser.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OnpTests.Parsers
{
    public class OperatorTermParserTests
    {
		[Theory]
		[InlineData("+")]
		[InlineData("-")]
		[InlineData("/")]
		[InlineData("*")]
		[InlineData("^")]
		public void WhenExpressionIsPassed_Then_ItShoulBeOperator(string expression)
		{
			var sut = new OperatorTermParser();

			var result = sut.Parse(expression, 0);

			Assert.True(result.TermType == TermType.Operator);
			Assert.True(result.Term == expression);
		}

		[Fact]
		public void WhenWrongOperatorExpressionIsPassed_Then_ExceptionShouldBeThrown()
		{
			var sut = new OperatorTermParser();

			Assert.Throws<InvalidOperationException>(() =>
			{
				var result = sut.Parse("&", 0);
			});
		}
	}
}
