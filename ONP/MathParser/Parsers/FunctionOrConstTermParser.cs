using ONP.MathParser.Context;
using ONP.MathParser.Terms;
using System;

namespace ONP.MathParser.Parsers
{
	/// <summary>
	/// Class used to parse function or const expression
	/// Function expression is a expression with parameter inside () at the end
	/// Const expression is a expression without parameter
	/// </summary>
	public class FunctionOrConstTermParser : ITermParser
	{
		public MathParserTermResult Parse(string expression, int startPos)
		{
			var i = startPos;
			var termType = TermType.Const;
			int numberOfParams = 0;

			MathParserTermResult internalTermResult = null;

			while (i < expression.Length && Char.IsLetter(expression[i]))
			{
				i++;
			}

			if (i < expression.Length && expression[i] == '(')
			{
				var subParser = new SubExpressionTermParser(true);
				internalTermResult = subParser.Parse(expression, i);
				numberOfParams = subParser.NumberOfParameters;

				termType = TermType.Function;
			}

			var term = expression.Substring(startPos, i - startPos);

			var result = new ComplexMathParserResult
			{
				StartPos = startPos,
				EndPos = internalTermResult != null ? internalTermResult.EndPos : i,
				Term = term,
				TermType = termType,
				InternalTerm = internalTermResult,
				NumberOfParams = numberOfParams
			};

			return result;
		}
	}
}
