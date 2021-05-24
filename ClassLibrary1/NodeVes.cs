using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLibrarySE
{
	/// <summary>
	/// Весовые коэффициенты узлов
	/// </summary>
	public class NodeVes
	{
		/// <summary>
		/// Номер узла
		/// </summary>
		private int numberNode;

		/// <summary>
		/// Номер /узла
		/// </summary>
		public int NumberNode
		{
			get
			{
				return numberNode;
			}
			set
			{
				try
				{
					numberNode = value;
				}
				catch (FormatException)
				{
					numberNode = 0;
				}
			}
		}


		/// <summary>
		/// Весовой коэф напряжения
		/// </summary>
		public double VesU { get; set; }

		/// <summary>
		/// Весовой коэф активной мощности
		/// </summary>
		public double VesP { get; set; }
		
		/// <summary>
		/// Весовой коэф реактивной мощности
		/// </summary>
		public double VesQ { get; set; }

		/// <summary>
		/// Напряжение узла БР
		/// </summary>
		private double ubr;

		/// <summary>
		/// Напряжение узла БР
		/// </summary>
		public double Ubr
		{
			get
			{
				return ubr;
			}
			set
			{
				try
				{
					ubr = value;
				}
				catch (FormatException)
				{
					ubr = 0;
				}
			}
		}

		/// <summary>
		/// Угол вектора напряжения
		/// </summary>
		private double delta;

		/// <summary>
		/// Угол вектора напряжений
		/// </summary>
		public double Delta
		{
			get
			{
				return delta;
			}
			set
			{
				try
				{
					delta = value;
				}
				catch (FormatException)
				{
					delta = 0;
				}
			}
		}

		/// <summary>
		/// Псевдоизмерение активной мощности
		/// </summary>
		public double Ppi { get; set; }
		
		/// <summary>
		/// Псевдоизмерение реактивной мощности
		/// </summary>
		public double Qpi { get; set; }
		
		/// <summary>
		/// Деление строки
		/// </summary>
		/// <param name="line"></param>
		private void Division(string line)
		{
			string[] parts = line.Split(';');
			if (Int32.TryParse(parts[0], out numberNode))
			{
				NumberNode = numberNode;
			}
			else
			{
				NumberNode = 0;
			}
			if (Double.TryParse(parts[1], out ubr))
			{
				Ubr = ubr;
			}
			else
			{
				Ubr = 0;
			}
			try
			{
				if (Double.TryParse(parts[2], out delta))
				{
					Delta = delta;

				}
				else
				{
					Delta = 0;
				}
			}
			catch
			{
				Delta = 0;
			}
		}

		/// <summary>
		/// Чтение из файла
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static List<NodeVes> ReadFileNodeVes(string filename)
		{
			List<NodeVes> nodeVesList = new List<NodeVes>();
			using (StreamReader file_csv = new StreamReader(filename))
			{
				string line;
				while ((line = file_csv.ReadLine()) != null)
				{
					NodeVes nodeVes = new NodeVes();
					nodeVes.Division(line.Replace(".",","));
					nodeVesList.Add(nodeVes);
				}

			}
			return nodeVesList;
		}
		/// <summary>
		/// Автоматическое заполнение весовых коэффициентов
		/// </summary>
		/// <param name="nodes"></param>
		/// <returns></returns>
		public static List<NodeVes> GetVes(List<Node> nodes)
		{
			List<NodeVes> nodeVesList=new List<NodeVes>();
			for (int i=0; i< nodes.Count; i++)
			{
				NodeVes nodeVes = new NodeVes();

				nodeVes.NumberNode=nodes[i].NumberNode;
				if (nodes[i].U_nom>=500)
					nodeVes.VesU = 3;
				else
					nodeVes.VesU = 2;
				//if (nodes[i].Type == 3)
				//{
				//	nodeVes.VesP = 100;
				//	nodeVes.VesQ = 100;
				//}
				//else
				//{
					nodeVes.VesP = 10;
					nodeVes.VesQ = 10;
				//}

				nodeVesList.Add(nodeVes);
			}
			return nodeVesList;

		}

		public void GetPi(List<Node_TM> node_TMs)
		{
			Ppi = node_TMs.Find(x => x.NumberNode == NumberNode).P;
			Qpi = node_TMs.Find(x => x.NumberNode == NumberNode).Q;

		}
	}
}
