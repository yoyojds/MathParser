using ONP.MathParser.Parsers;

namespace ONP.MathParser.Terms.Decorators
{
	public interface ITermResultStrategy
    {
		MathParserTermValidationResult Validate(MathParserTermResult term);
		double Execute(MathParserTermResult term, params object[] parameters);
	}
}
