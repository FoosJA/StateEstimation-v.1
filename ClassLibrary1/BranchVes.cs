using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ClassLibrarySE
{
	/// <summary>
	/// Весовые коэффициенты ТМ ветвей
	/// </summary>
	public class BranchVes
	{
		/// <summary>
		/// Номер ветви
		/// </summary>
		public int NumberBranch { get; set; }

		/// <summary>
		/// Весовой коэффициент активной мощности в начале ветви
		/// </summary>
		public double VesP { get; set; }

		/// <summary>
		/// Весовой коэффициент реактивной мощности в начале ветви
		/// </summary>
		public double VesQ { get; set; }

		/// <summary>
		/// Весовой коэффициент активной мощности в конце ветви
		/// </summary>
		public double VesI { get; set; }

		/// <summary>
		/// Весовой коэффициент реактивной мощности в конце ветви
		/// </summary>
		public double VesSigma { get; set; }

		/////// <summary>
		/////// Деление строки
		/////// </summary>
		/////// <param name="line"></param>
		////private void Division(string line)
		////{
		////	string[] parts = line.Split(';');
		////	NumberBranch = Convert.ToInt32(parts[0]);
		////	VesPnach = Convert.ToDouble(parts[1]);
		////	VesQnach = Convert.ToDouble(parts[2]);
		////	VesPkon = Convert.ToDouble(parts[3]);
		////	VesQkon = Convert.ToDouble(parts[4]);
		////}

		/////// <summary>
		/////// Чтение файла весовых коэффициентов ветвей
		/////// </summary>
		/////// <param name="filename">Путь к файлу</param>
		/////// <returns>Список весовых коэффициентов</returns>
		////public static List<BranchVes> ReadFileBranchVes(string filename)
		////{
		////	List<BranchVes> branchVesList = new List<BranchVes>();
		////	using (StreamReader file_csv = new StreamReader(filename))
		////	{
		////		string line;
		////		while ((line = file_csv.ReadLine()) != null)
		////		{
		////			BranchVes branchVes = new BranchVes();
		////			branchVes.Division(line);
		////			branchVesList.Add(branchVes);
		////		}
		////	}
		////	return branchVesList;
		////}

		public static List<BranchVes> GetVes(List<Branch> branches, List<Node> nodes)
		{
			List<BranchVes> branchVesList = new List<BranchVes>();
			for (int i=0; i<branches.Count;i++)
			{
				BranchVes branchVes = new BranchVes();
				branchVes.NumberBranch = branches[i].NumberBranch;
				var numbNodeNach=branches[i].Node_nach;
				var branchU=nodes.Find(x => x.NumberNode == numbNodeNach).U_nom;
				if (branchU > 220)
				{
					branchVes.VesP= 15;
					branchVes.VesQ = 15;
					branchVes.VesI = 15;
					branchVes.VesSigma = 15;
				}
				else
				{
					branchVes.VesP = 10;
					branchVes.VesQ = 10;
					branchVes.VesI = 10;
					branchVes.VesSigma = 10;
				}
				var numbNodeKon = branches[i].Node_kon;
				branchU = nodes.Find(x => x.NumberNode == numbNodeKon).U_nom;
				if (branchU > 220)
				{
					branchVes.VesP = 15;
					branchVes.VesQ = 15;
					branchVes.VesI = 15;
					branchVes.VesSigma = 15;
				}
				else
				{
					branchVes.VesP = 10;
					branchVes.VesQ = 10;
					branchVes.VesI = 10;
					branchVes.VesSigma = 10;
				}
				branchVesList.Add(branchVes);
			}
			return branchVesList;
		}
	}
}
