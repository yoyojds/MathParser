using ONP.MathParser.Terms;

namespace ONP.MathParser.Parsers
{
	public interface ITermParser
    {
		MathParserTermResult Parse(string expression, int startPos);
	}
}
