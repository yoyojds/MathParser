using ONP.MathParser.Context;
using ONP.MathParser.Parsers;
using System.Collections.Generic;

namespace ONP.MathParser
{
	public interface IMathParser
    {
		IContext Context { get; }

		List<MathParserTermResult> Parse(string expression, bool allowParameters = false);
		CalculationResult Calculate(string expression);
	}
}
