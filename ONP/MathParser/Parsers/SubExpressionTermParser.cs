using System;

namespace ONP.MathParser.Parsers
{
	public class SubExpressionTermParser : ITermParser
	{
		private readonly bool _isFunctionExpression = false;

		public int NumberOfParameters { get; private set; } = 1;

		public SubExpressionTermParser(bool isFunctionExpression = false)
		{
			_isFunctionExpression = isFunctionExpression;
		}

		public MathParserTermResult Parse(string expression, int startPos)
		{
			var i = startPos;
			var openedParenthesis = 0;

			if (expression[i] == '(')
			{
				i++;

				while (i < expression.Length)
				{
					if (expression[i] == '(')
						openedParenthesis++;
					else
					if (expression[i] == ')')
					{
						if (openedParenthesis > 0)
							openedParenthesis--;
						else break;
					}

					if (_isFunctionExpression && expression[i] == ',' && openedParenthesis == 0)
						NumberOfParameters++;

					i++;
				}

				if (openedParenthesis > 0)
					throw new InvalidOperationException("No matching ')'!");

				if (i == expression.Length || expression[i] != ')')
					throw new InvalidOperationException("Expression should be ended with ')'!");
			}

			if (startPos + 1 == i)
				throw new InvalidOperationException($"Subexpression is empty!");

			var result = new ComplexMathParserResult
			{
				StartPos = startPos + 1,
				EndPos = i + 1,
				Term = expression.Substring(startPos + 1, i - startPos - 1),
				TermType = TermType.Expression
			};

			return result;
		}

		public static bool IsOpenParenthesis(char sign)
		{
			return sign == '(';
		}
	}
}
