using ONP.MathParser.Context;
using System;

namespace ONP.MathParser
{
	internal static class MathParserDefinitions
    {
		private static Func<double, double, double> _pow = (x, y) => Math.Pow(x, y);
		private static Func<double, double, double> _max = (x, y) => Math.Max(x, y);
		private static Func<double, double> _sin = (x) => Math.Sin(x);
		private static Func<double, double> _cos = (x) => Math.Cos(x);
		private static Func<double, double> _tan = (x) => Math.Tan(x);
		private static Func<double, double> _log = (x) => Math.Log(x);
		private static Func<double, double> _abs = (x) => Math.Abs(x);
		private static Func<double, double> _sqrt = (x) => Math.Sqrt(x);

		public static void AddPredefinedConsts(IContext context)
		{
			context.AddConst("pi", Math.PI);
			context.AddConst("e", Math.E);
		}

		public static void AddPredefinedFunctions(IContext context)
		{
			context.AddFunction("pow", new FunctionDescriptor
			{
				Function = _pow,
				ParamsNumber = 2
			});

			context.AddFunction("max", new FunctionDescriptor
			{
				Function = _max,
				ParamsNumber = 2
			});

			context.AddFunction("sin", new FunctionDescriptor
			{
				Function = _sin,
				ParamsNumber = 1
			});

			context.AddFunction("cos", new FunctionDescriptor
			{
				Function = _cos,
				ParamsNumber = 1
			});

			context.AddFunction("tan", new FunctionDescriptor
			{
				Function = _tan,
				ParamsNumber = 1
			});

			context.AddFunction("log", new FunctionDescriptor
			{
				Function = _log,
				ParamsNumber = 1
			});

			context.AddFunction("abs", new FunctionDescriptor
			{
				Function = _abs,
				ParamsNumber = 1
			});

			context.AddFunction("sqrt", new FunctionDescriptor
			{
				Function = _sqrt,
				ParamsNumber = 1
			});
		}
	}
}
