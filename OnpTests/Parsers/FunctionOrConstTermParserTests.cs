using ONP.MathParser.Parsers;
using System;
using Xunit;

namespace OnpTests.Parsers
{
	public class FunctionOrConstTermParserTests
    {
		[Fact]
		public void WhenExpressionConsistOnlyFromLetters_Then_ConstTermShouldBeReturned()
		{
			var sut = new FunctionOrConstTermParser();

			var result = sut.Parse("pi", 0);

			Assert.True(result.TermType == TermType.Const);
			Assert.True(result.Term == "pi");
		}

		[Theory]
		[InlineData("sin(x)", "sin", "x", 1)]
		[InlineData("max(7,2)", "max", "7,2", 2)]
		[InlineData("sin(max(7,2))", "sin", "max(7,2)", 1)]
		[InlineData("sin(max(7,2)+2.4*3)", "sin", "max(7,2)+2.4*3", 1)]
		public void WhenExpressionConsistFromLettersAndParenthesis_Then_FunctionTermShouldBeReturned(
			string expression, string expectedFunction, string expectedParameters, int numberOfParams)
		{
			var sut = new FunctionOrConstTermParser();

			var result = sut.Parse(expression, 0) as ComplexMathParserResult;

			Assert.True(result.TermType == TermType.Function);
			Assert.True(result.Term == expectedFunction);
			Assert.True(result.InternalTerm != null);
			Assert.True(result.InternalTerm.Term == expectedParameters);
			Assert.True(result.InternalTerm.TermType == TermType.Expression);
			Assert.True(result.NumberOfParams == numberOfParams);
		}

		[Theory]
		[InlineData("sin()")]
		public void WhenWrongFunctionExpressionIsPassed_Then_ExceptionShouldBeThrown(string expression)
		{
			var sut = new FunctionOrConstTermParser();

			Assert.Throws<InvalidOperationException>(() =>
			{
				var result = sut.Parse(expression, 0);
			});
		}
	}
}
