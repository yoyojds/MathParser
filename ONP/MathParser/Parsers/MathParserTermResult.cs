namespace ONP.MathParser.Parsers
{
	public enum TermType
	{
		Number,
		Operator,
		Function,
		Const,
		Expression
	}

	public class MathParserTermResult
	{
		public int StartPos { get; set; }

		public int EndPos { get; set; }

		public string Term { get; set; }

		public TermType TermType { get; set; }

		public override string ToString()
		{
			return $"{TermType} => {Term}";
		}
	}

	public class ComplexMathParserResult : MathParserTermResult
	{
		public MathParserTermResult InternalTerm { get; set; }

		public int NumberOfParams { get; set; }
	}
}
