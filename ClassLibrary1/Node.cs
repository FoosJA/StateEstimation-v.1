using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ClassLibrarySE
{
	/// <summary>
	/// Параметры схемы замещения узлов
	/// </summary>
	public class Node
	{
		/// <summary>
		/// Словарь типов
		/// </summary>
		private Dictionary<int, string> _keyType = new Dictionary<int, string>
		{
			[0] = "База",
			[1] = "Ген.",
			[2] = "Нагр.",
			[3] = "Сет."
		};
        #region Parametrs
        /// <summary>
        /// Тип узла
        /// </summary>
        private int type;

		/// <summary>
		/// Тип узла
		/// </summary>
		public int Type
		{
            get { return type; }
            set { type = value; }
        }
		/// <summary>
		/// Номер узла
		/// </summary>
		private int numberNode;

		/// <summary>
		/// Номер /узла
		/// </summary>
		public int NumberNode
		{
            get { return numberNode; }
            set { numberNode = value; }
        }

        /// <summary>
        /// Номинальное напряжение узла
        /// </summary>
        private double u_nom;

		/// <summary>
		/// Номинальное напряжение узла
		/// </summary>
		public double U_nom
        {
            get { return u_nom; }
            set { u_nom = value; }
        }
        /// <summary>
        /// собственная реактивная проводимость узла
        /// </summary>
        private double b;
		/// <summary>
		/// собственная реактивная проводимость узла
		/// </summary>
		public double B
        {
            get { return b; }
            set { b = value ; }
        }
        double _nullValue = -9999;
#endregion
        
        /// <summary>
        /// Разделение строки на части
        /// </summary>
        /// <param name="line"></param>
        private void Division(string line)
		{
			string[] parts = line.Split(';');
            Type = Int32.Parse(parts[0]); 
            NumberNode = Int32.Parse(parts[1]);
            try { U_nom = Double.Parse(parts[2]); }
            catch { U_nom = 0; }
            try { B = Double.Parse(parts[3]); }//в мкСМ
            catch { B = 0; }            
		}

		/*public static List<Node> ReadRastrFile(ASTRALib.ITable tableNode)
		{
			List<Node> nodeList = new List<Node>();

			ASTRALib.ICol tip = tableNode.Cols.Item("tip");
			ASTRALib.ICol ny = tableNode.Cols.Item("ny");
			ASTRALib.ICol uhom = tableNode.Cols.Item("uhom");
			ASTRALib.ICol bsh = tableNode.Cols.Item("bsh");
			for (int i = 0; i < tableNode.Size - 1; i++)
			{
				Node node = new Node();
				node.Type = tip.ZN[i];
				node.NumberNode = ny.ZN[i];
				node.U_nom = uhom.ZN[i];
				node.B = bsh.ZN[i];
			}

			return nodeList;
		}*/

		/// <summary>
		/// Чтение из файла УЗЛЫ
		/// </summary>
		/// <param name="filename">Путь к файлу</param>
		/// <returns>Список узлов</returns>
		public static List<Node> ReadFileNode(string filename)
		{
			List<Node> nodeList = new List<Node>();
			using (StreamReader file_csv = new StreamReader(filename))
			{
				string line;
				while ((line = file_csv.ReadLine()) != null)
				{
					Node node = new Node();
					node.Division(line.Replace(".", ","));
					nodeList.Add(node);
				}
			}
			return nodeList;
		}

		/// <summary>
		/// Запись в datagrid типа узла
		/// </summary>
		/// <returns></returns>
		public string GetTypeNode()
		{
			return _keyType[Type];
		}
		public int GetTypeNode(string type)
		{
			return _keyType.FirstOrDefault(x => x.Value == type).Key;
		}
		public static void SaveFile(List<Node> nodeList, string filename)
		{
			string inform = Information(nodeList);
			File.WriteAllText(filename, inform);
		}
		private static string Information(List<Node> nodeList)
		{
			string inform = "Type;Number;Unom;Bш\n";
			for (int i = 0; i < nodeList.Count; i++)
			{
				inform += ($"{nodeList[i].Type}" + ";"
					+ $"{nodeList[i].NumberNode}" + ";"
					+ $"{nodeList[i].U_nom}" + ";"
					+ $"{nodeList[i].B}" + "\n");
			}
			return inform;
		}
	}
}
