using System;

namespace ONP.MathParser.Parsers
{
	public class OperatorTermParser : ITermParser
	{
		public MathParserTermResult Parse(string expression, int startPos)
		{
			if (!IsOperator(expression[startPos]))
				throw new InvalidOperationException($"{expression[startPos]} is not valid operator!");

			var result = new MathParserTermResult
			{
				StartPos = startPos,
				EndPos = startPos + 1,
				Term = expression[startPos].ToString(),
				TermType = TermType.Operator
			};

			return result;
		}

		public static bool IsOperator(char sign)
		{
			string pattern = "+-/*^";

			return pattern.Contains(sign.ToString());
		}
	}
}
