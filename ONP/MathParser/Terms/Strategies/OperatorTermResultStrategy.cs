using ONP.MathParser.Context;
using ONP.MathParser.Parsers;
using System;
using System.Collections.Generic;

namespace ONP.MathParser.Terms.Decorators
{
	public class OperatorTermResultStrategy : ITermResultStrategy
	{
		private readonly IContext _context;

		public OperatorTermResultStrategy(IContext context)
		{
			_context = context;
		}

		public double Execute(MathParserTermResult term, params object[] parameters)
		{
			if (parameters.Length != 1)
				throw new InvalidOperationException("Operator needs at least 1 parameters to execute!");

			var stack = parameters[0] as Stack<double>;

			if (stack.Count < 2)
				throw new InvalidOperationException($"Operator '{term.Term}' needs 2 parameters to execute!");

			var number1 = stack.Pop();
			var number2 = stack.Pop();

			switch (term.Term)
			{
				case "+":
					return number1 + number2;
				case "-":
					return number2 - number1;
				case "*":
					return  number1 * number2;
				case "/":
					return number2 / number1;
				case "^":
					return Math.Pow(number2, number1);
				default:
					throw new InvalidOperationException("Probably wrong operation has been applied!");
			}
		}

		public MathParserTermValidationResult Validate(MathParserTermResult term)
		{
			var result = new MathParserTermValidationResult();

			if (!IsOperator(term.Term[0]))
				result.AddError($"'{term.Term}' was not recognized as valid operator!");

			return result;
		}

		public static bool IsOperator(char sign)
		{
			return "+-/*^".Contains(sign.ToString());
		}
	}
}
