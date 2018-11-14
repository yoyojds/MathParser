namespace ONP.MathParser.Context
{
	public class FunctionParam
	{
		public string Name { get; set; }
	}

    public interface IContext
    {
		void AddConst(string name, double value);
		double GetConst(string name);
		bool CheckConst(string name);
		void AddFunction(string name, FunctionDescriptor descriptor);
		FunctionDescriptor GetFunction(string name);
    }
}
