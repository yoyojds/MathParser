using ONP.Converter;
using ONP.MathParser.Context;
using ONP.MathParser.Parsers;
using ONP.MathParser.Terms;
using System;
using System.Collections.Generic;

namespace ONP.MathParser
{
	public class MathParser : IMathParser
	{
		private readonly IContext _context;
		private readonly OnpConverter _onpConverter;

		public IContext Context => _context;

		public MathParser(IContext context)
		{
			_context = context;
			_onpConverter = new OnpConverter(this, new TermResultStrategyFactory(context));

			MathParserDefinitions.AddPredefinedConsts(_context);
			MathParserDefinitions.AddPredefinedFunctions(_context);
		}

		public List<MathParserTermResult> Parse(string expression, bool allowParameters = false)
		{
			var terms = new List<MathParserTermResult>();

			if (String.IsNullOrWhiteSpace(expression))
				throw new ArgumentNullException("Expression cannot be empty!");

			var i = 0;

			while (i < expression.Length)
			{
				MathParserTermResult term = null;

				if (Char.IsDigit(expression[i]))
				{
					term = new NumberTermParser().Parse(expression, i);
				}
				else
				if (OperatorTermParser.IsOperator(expression[i]))
				{
					term = new OperatorTermParser().Parse(expression, i);
				}
				else
				if (Char.IsLetter(expression[i]))
				{
					term = new FunctionOrConstTermParser().Parse(expression, i);
				}
				else
				if (SubExpressionTermParser.IsOpenParenthesis(expression[i]))
				{
					term = new SubExpressionTermParser().Parse(expression, i);
				}
				else
				if (allowParameters && expression[i] == ',')
				{
					i++;
					continue;
				}

				if (term != null)
				{
					i = term.EndPos;

					terms.Add(term);
				}
				else
					throw new InvalidOperationException($"Wrong character '{expression[i]}' at [{i}]!");
			}

			return terms;
		}

		public CalculationResult Calculate(string expression)
		{
			return _onpConverter.Calculate(Parse(expression).ToArray());
		}
	}
}
