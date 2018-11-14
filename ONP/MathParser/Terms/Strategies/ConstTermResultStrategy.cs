using ONP.MathParser.Context;
using ONP.MathParser.Parsers;
using System;

namespace ONP.MathParser.Terms.Decorators
{
	class ConstTermResultStrategy : ITermResultStrategy
	{
		private readonly IContext _context;

		public ConstTermResultStrategy(IContext context)
		{
			_context = context;
		}

		public double Execute(MathParserTermResult term, params object[] parameters)
		{
			return _context.GetConst(term.Term);
		}

		public MathParserTermValidationResult Validate(MathParserTermResult term)
		{
			var result = new MathParserTermValidationResult();

			if (!_context.CheckConst(term.Term))
				result.AddError($"Const with name {term.Term} was not declared!");

			return result;
		}
	}
}
