using ONP.MathParser;
using ONP.MathParser.Parsers;
using ONP.MathParser.Terms;
using ONP.MathParser.Terms.Decorators;
using System.Collections.Generic;

namespace ONP.Converter
{
	internal enum Associativity
	{
		Left,
		Right
	}

	internal class OnpOperator
	{
		public int Precedence { get; set; }
		public Associativity Associativity { get; set; }
	}

	public class OnpConverter
    {
		private readonly IMathParser _parser;
		private readonly IDictionary<string, OnpOperator> _onpOperators;
		private readonly ITermResultStrategyFactory _termResultStrategyFactory;

		public OnpConverter(IMathParser parser, ITermResultStrategyFactory termResultStrategyFactory)
		{
			_parser = parser;
			_termResultStrategyFactory = termResultStrategyFactory;

			_onpOperators = new Dictionary<string, OnpOperator>
			{
				{ "^", new OnpOperator { Associativity = Associativity.Right, Precedence = 4 } },
				{ "*", new OnpOperator { Associativity = Associativity.Left, Precedence = 3 } },
				{ "/", new OnpOperator { Associativity = Associativity.Left, Precedence = 3 } },
				{ "+", new OnpOperator { Associativity = Associativity.Left, Precedence = 2 } },
				{ "-", new OnpOperator { Associativity = Associativity.Left, Precedence = 2 } }
			};
		}

		public List<MathParserTermResult> ConvertTermsToOnp(MathParserTermResult[] terms)
		{
			Stack<MathParserTermResult> operators = new Stack<MathParserTermResult>();
			List<MathParserTermResult> onp = new List<MathParserTermResult>();

			foreach(var term in terms)
			{
				switch (term.TermType)
				{
					case TermType.Number:
					case TermType.Const:
						{
							onp.Add(term);
							break;
						}
					case TermType.Function:
						{
							var expression = (term as ComplexMathParserResult).InternalTerm.Term;

							var subTerms = _parser.Parse(expression, true);
							var result = ConvertTermsToOnp(subTerms.ToArray());

							onp.AddRange(result);
							onp.Add(term);
							break;
						}
					case TermType.Operator:
						{
							operators.TryPeek(out var top);

							while (top != null
								&& (top.TermType == TermType.Function
								|| (top.TermType == TermType.Operator && _onpOperators[top.Term].Precedence > _onpOperators[term.Term].Precedence)
								|| (top.TermType == TermType.Operator && _onpOperators[top.Term].Precedence == _onpOperators[term.Term].Precedence && _onpOperators[top.Term].Associativity == Associativity.Left)
									)
								&& top.TermType != TermType.Expression
								)
							{
								onp.Add(operators.Pop());
								operators.TryPeek(out top);
							}

							operators.Push(term);
							break;
						}
					case TermType.Expression:
						{
							var subTerms = _parser.Parse(term.Term);
							var result = ConvertTermsToOnp(subTerms.ToArray());

							onp.AddRange(result);
							break;
						}
				}
			}

			while (operators.TryPeek(out var top))
			{
				onp.Add(operators.Pop());
			}

			return onp;
		}

		public CalculationResult Calculate(MathParserTermResult[] terms)
		{
			var result = new CalculationResult();

			var onpTerms = ConvertTermsToOnp(terms);
			var stack = new Stack<double>();

			foreach (var term in onpTerms)
			{
				ITermResultStrategy strategy = _termResultStrategyFactory.GetStrategy(term.TermType);

				var validationResult = strategy.Validate(term);

				if (validationResult.IsValid)
				{
					var subResult = strategy.Execute(term, stack);

					stack.Push(subResult);
				} else
				{
					result.AddErrors(validationResult.Errors);

					return result;
				}
			}

			if (stack.Count > 1 || stack.Count == 0)
			{
				result.AddError("Expression is not valid math expression!");
				return result;
			}

			result.Result = stack.Pop();

			return result;
		}
    }
}
