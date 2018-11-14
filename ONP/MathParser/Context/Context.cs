using System;
using System.Collections.Generic;

namespace ONP.MathParser.Context
{
	public class FunctionDescriptor
	{
		public int ParamsNumber { get; set; }
		public Delegate Function { get; set; }
	}

	public class Context : IContext
	{
		private readonly IDictionary<string, double> _consts;
		private readonly IDictionary<string, FunctionDescriptor> _functions;

		public Context()
		{
			_consts = new Dictionary<string, double>();
			_functions = new Dictionary<string, FunctionDescriptor>();
		}

		public void AddConst(string name, double value)
		{
			_consts.TryAdd(name, value);
		}

		public void AddFunction(string name, FunctionDescriptor descriptor)
		{
			_functions.TryAdd(name, descriptor);
		}

		public bool CheckConst(string name)
		{
			return _consts.TryGetValue(name, out double value);
		}

		public double GetConst(string name)
		{
			if (!_consts.TryGetValue(name, out double value))
				throw new InvalidOperationException($"Const with name {name} was not declared!");

			return value;
		}

		public FunctionDescriptor GetFunction(string name)
		{
			if (!_functions.TryGetValue(name, out var result))
				throw new InvalidOperationException($"Function with name {name} was not declared!");

			return result;
		}
	}
}
