using ONP.MathParser.Context;
using ONP.MathParser.Parsers;
using System;

namespace ONP.MathParser.Terms.Decorators
{
	public class NumberTermResultStrategy : ITermResultStrategy
	{
		private readonly IContext _context;

		public NumberTermResultStrategy(IContext context)
		{
			_context = context;
		}

		public double Execute(MathParserTermResult term, params object[] parameters)
		{
			Double.TryParse(
				term.Term, System.Globalization.NumberStyles.AllowDecimalPoint,
				System.Globalization.NumberFormatInfo.InvariantInfo,
				out var number);

			return number;
		}

		public MathParserTermValidationResult Validate(MathParserTermResult term)
		{
			var result = new MathParserTermValidationResult();

			if (String.IsNullOrWhiteSpace(term.Term))
			{
				result.AddError("Term should not be empty!");
			}

			if (!Double.TryParse(term.Term, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo, out var number))
				result.AddError($"Cannot convert {term.Term} to number!");

			return result;
		}
	}
}
