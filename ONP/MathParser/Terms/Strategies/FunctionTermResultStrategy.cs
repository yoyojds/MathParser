using ONP.MathParser.Context;
using ONP.MathParser.Parsers;
using System;
using System.Collections.Generic;

namespace ONP.MathParser.Terms.Decorators
{
	public class FunctionTermResultStrategy : ITermResultStrategy
	{
		private readonly IContext _context;

		public FunctionTermResultStrategy(IContext context)
		{
			_context = context;
		}

		public double Execute(MathParserTermResult term, params object[] parameters)
		{
			if (parameters.Length != 1)
				throw new InvalidOperationException("Function needs at least 1 parameters to execute!");

			var stack = parameters[0] as Stack<double>;

			var descriptor = _context.GetFunction(term.Term);
			var i = descriptor.ParamsNumber;
			object[] functionParams = new object[descriptor.ParamsNumber];

			while (i > 0)
			{
				if (stack.TryPop(out var number))
					functionParams[--i] = number;
			}

			return (double)descriptor.Function.DynamicInvoke(functionParams);
		}

		public MathParserTermValidationResult Validate(MathParserTermResult term)
		{
			var result = new MathParserTermValidationResult();

			var parameters = (term as ComplexMathParserResult).NumberOfParams;
			var expectedParameters = _context.GetFunction(term.Term).ParamsNumber;

			if (parameters != expectedParameters)
				result.AddError($"Function {term.Term} expects {expectedParameters} but got {parameters}!");

			return result;
		}
	}
}
