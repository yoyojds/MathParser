using ONP.MathParser.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OnpTests.Parsers
{
    public class NumberTermParserTests
    {
		[Theory]
		[InlineData("12231")]
		[InlineData("1900.213")]
		[InlineData("00.213")]
		public void WhenExpressionIsNumber_Then_NumberTermIsReturned(string expression)
		{
			var sut = new NumberTermParser();

			var result = sut.Parse(expression, 0);

			Assert.True(result.TermType == TermType.Number);
			Assert.True(result.Term == expression);
		}

		[Theory]
		[InlineData("123.7.9")]
		public void WhenWrongNumberExpressionIsPassed_Then_ExceptionShouldBeThrown(string expression)
		{
			var sut = new NumberTermParser();

			Assert.Throws<InvalidOperationException>(() =>
			{
				var result = sut.Parse(expression, 0);
			});
		}
	}
}
