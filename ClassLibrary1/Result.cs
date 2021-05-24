using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ClassLibrarySE
{
	public class Result
	{
		public Matrix F;
		public double E;
		public Matrix U;

		public Result(Matrix f, double e, Matrix u)
		{
			F = f;
			E = e;
			U = u;
		}
		public static Matrix FindMinE(List<Result> resultList)
		{
			double min = resultList[0].E;
			for (int i = 0; i < resultList.Count; i++)
			{
				if (min > resultList[i].E)
				{
					min = resultList[i].E;
				}

			}
			return resultList.Find(x => x.E == min).U;
		}
	}
}
