using ONP.MathParser.Terms;
using System;

namespace ONP.MathParser.Parsers
{
	public class NumberTermParser : ITermParser
	{
		public MathParserTermResult Parse(string expression, int startPos)
		{
			var i = startPos;
			var dotCount = 0;

			while (i < expression.Length && (Char.IsDigit(expression[i]) || expression[i] == '.'))
			{
				if (expression[i] == '.')
				{
					dotCount++;

					if (dotCount > 1)
						throw new InvalidOperationException("Wrong number of dots in expression!");
				}

				i++;
			}

			var result = new MathParserTermResult
			{
				StartPos = startPos,
				EndPos = i,
				Term = expression.Substring(startPos, i - startPos),
				TermType = TermType.Number
			};

			return result;
		}
	}
}
