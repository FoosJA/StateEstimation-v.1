using System;

namespace ClassLibrarySE
{
	/// <summary>
	/// Уравнения узловых напряжений
	/// </summary>
	public class PowerSystemEquations
	{
		//public static double F_Pbranch(double Vi, double Vj, double gii, double gij, double bij, double delta_i, double delta_j)
		//{
		//	double Pij = Vi * Vi * (gii) - Math.Abs(Vi) * Math.Abs(Vj) * (gij * Math.Cos(delta_i - delta_j) - bij * Math.Sin(delta_i - delta_j));
		//	return Pij;
		//}
		//public static double F_Qbranch(double Vi, double Vj, double bii, double gij, double bij, double delta_i, double delta_j)
		//{
		//	double Qij = Vi * Vi * (bii) - Math.Abs(Vi) * Math.Abs(Vj) * (gij * Math.Sin(delta_i - delta_j) + bij * Math.Cos(delta_i - delta_j));
		//	return Qij;
		//}
		//public static double FP_Vnode(double Vj, double gij, double bij, double delta_i, double delta_j)
		//{
		//	double FV = Math.Abs(Vj) * (gij * Math.Cos(delta_i - delta_j) - bij * Math.Sin(delta_i - delta_j));
		//	return FV;
		//}
		//public static double FP_Znode(double gii, double gij)
		//{
		//	double Z = gii + gij;
		//	return Z;
		//}
		//public static double FQ_Vnode(double Vj, double gij, double bij, double delta_i, double delta_j)
		//{
		//	double FV = Math.Abs(Vj) * (gij * Math.Sin(delta_i - delta_j) + bij * Math.Cos(delta_i - delta_j));
		//	return FV;
		//}
		//public static double JQ_Vnode(double Vj, double gij, double bij, double delta_i, double delta_j)
		//{
		//	double FV = Math.Abs(Vj) * (-gij * Math.Cos(delta_i - delta_j) + bij * Math.Sin(delta_i - delta_j));
		//	return FV;
		//}
		//public static double FQ_Znode(double bii, double bij)
		//{
		//	double Z = bii + bij;
		//	return Z;
		//}
		public static double FC_branch(double Vi, double Vj, double gii, double bii, double gij, double bij, double delta_i, double delta_j)
		{
			return (Vi * (gii) - Vj * (gij * Math.Cos(delta_i - delta_j) - bij * Math.Sin(delta_i - delta_j))) / Math.Sqrt(3);
		}
		public static double FD_branch(double Vi, double Vj, double gii, double bii, double gij, double bij, double delta_i, double delta_j)
		{
			return (Vi * (bii) - Vj * (gij * Math.Sin(delta_i - delta_j) + bij * Math.Cos(delta_i - delta_j))) / Math.Sqrt(3);
		}
		public static double FA_branch(double gij, double bij, double delta_i, double delta_j)
		{
			return (bij * Math.Sin(delta_i - delta_j) - gij * Math.Cos(delta_i - delta_j));
		}
		public static double FB_branch(double gij, double bij, double delta_i, double delta_j)
		{
			return (-bij * Math.Cos(delta_i - delta_j) - gij * Math.Sin(delta_i - delta_j));
		}

	}
}
