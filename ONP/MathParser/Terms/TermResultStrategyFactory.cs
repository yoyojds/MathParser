using ONP.MathParser.Context;
using ONP.MathParser.Parsers;
using ONP.MathParser.Terms.Decorators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.MathParser.Terms
{
	public interface ITermResultStrategyFactory
	{
		ITermResultStrategy GetStrategy(TermType type);
	}

	public class TermResultStrategyFactory : ITermResultStrategyFactory
	{
		private readonly IDictionary<TermType, ITermResultStrategy> _strategies;

		public TermResultStrategyFactory(IContext context)
		{
			_strategies = new Dictionary<TermType, ITermResultStrategy>();

			_strategies.Add(TermType.Number, new NumberTermResultStrategy(context));
			_strategies.Add(TermType.Const, new ConstTermResultStrategy(context));
			_strategies.Add(TermType.Function, new FunctionTermResultStrategy(context));
			_strategies.Add(TermType.Operator, new OperatorTermResultStrategy(context));
		}

		public ITermResultStrategy GetStrategy(TermType type)
		{
			return _strategies[type];
		}
	}
}
