using System.Collections.Generic;

namespace ONP.MathParser.Terms
{
	public class MathParserTermValidationResult
	{
		private readonly List<string> _errors;

		public bool IsValid { get; private set; }

		public string[] Errors => _errors.ToArray();

		public MathParserTermValidationResult()
		{
			_errors = new List<string>();
			IsValid = true;
		}

		public void AddError(string errorMessage)
		{
			AddErrors(new[] { errorMessage });
		}

		public void AddErrors(string[] errors)
		{
			_errors.AddRange(errors);
			IsValid = false;
		}
	}
}
