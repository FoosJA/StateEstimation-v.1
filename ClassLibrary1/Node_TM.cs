using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ClassLibrarySE
{
	/// <summary>
	/// ТМ узлов
	/// </summary>
	public class Node_TM
	{
        private double _nullValue = 0;
        private Dictionary<bool, string> _keySost = new Dictionary<bool, string>
		{
			[true] = "Вкл.",
			[false] = "Выкл.",
		};

        #region Parametrs
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
        /// Состояние узла
        /// </summary>
        private bool sostoyanie;

		/// <summary>
		/// Состояние узла
		/// </summary>
		public bool Sostoyanie
        {
            get { return sostoyanie; }
            set { sostoyanie = value; }
        }

        /// <summary>
        /// Напряжение в узле
        /// </summary>
        private double u;

		/// <summary>
		/// Напряжение узла ТМ
		/// </summary>
		public double U
        {
            get { return u; }
            set { u = value; }
        }

        /// <summary>
        /// Угло напряжения в узле
        /// </summary>
        private double delta;

		/// <summary>
		/// Угол напряжения узла ТМ
		/// </summary>
		public double Delta
        {
            get { return delta; }
            set { delta = value; }
        }

        /// <summary>
        /// Инъекция активной мощности в узле
        /// </summary>
        private double p;

		/// <summary>
		/// Инъекция активной мощности в узле
		/// </summary>
		public double P
		{
            get { return p; }
            set { p = value; }
        }

        /// <summary>
        /// Инъекция реактивной мощности в узле
        /// </summary>
        private double q;

		/// <summary>
		/// Инъекция реактивной мощности в узле
		/// </summary>
		public double Q
        {
            get { return q; }
            set { q = value; }
        }
        #endregion

        /// <summary>
        /// Деление строки
        /// </summary>
        /// <param name="line">строка</param>
        private void Division(string line)
		{
			string[] parts = line.Split(';');
            NumberNode = Int32.Parse(parts[0]);
            if (parts[1] == "1")
                Sostoyanie = false;
            else
                Sostoyanie = true;

            try { U = Double.Parse(parts[2]); }
            catch { U = _nullValue; }
            try { Delta = Double.Parse(parts[3]); }
            catch { Delta = _nullValue; }
            double Pg;
            double Qg;
            double Pn;
            double Qn;
            try { Pg = Double.Parse(parts[4]); }
            catch { Pg = 0; }
            try { Pn = Double.Parse(parts[6]); }
            catch { Pn = 0; }
            P = Pg - Pn;
            try { Qg = Double.Parse(parts[5]); }
            catch { Qg = 0; }
            try { Qn = Double.Parse(parts[7]); }
            catch { Qn = 0; }
            Q = Qg -Qn;
		}

		/// <summary>
		/// Чтение из файла УЗЛЫ
		/// </summary>
		/// <param name="filename">Путь к файлу</param>
		/// <returns>Список узлов</returns>
		public static List<Node_TM> ReadFileNode(string filename)
		{
			List<Node_TM> nodeList = new List<Node_TM>();
			using (StreamReader file_csv = new StreamReader(filename))
			{
				string line;
				while ((line = file_csv.ReadLine()) != null)
				{
					Node_TM node = new Node_TM();
					node.Division(line.Replace(".",","));					
					nodeList.Add(node);
				}
			}
			return nodeList;
		}

		/// <summary>
		/// Запись в файл
		/// </summary>
		/// <param name="node_OC"></param>
		/// <returns></returns>
		public static string Information(List<Node_TM> node_OC)
		{
			//string inform = "Number;Sostoyanie;U;delta;Рg;Qg;Pn;Qn\n";
			string inform = String.Empty;
			for (int i = 0; i < node_OC.Count; i++)
			{
				if (node_OC[i].P < 0)
				{
					inform += ($"{node_OC[i].NumberNode}" + ";"
						+ $"{node_OC[i].Sostoyanie}" + ";"
						+ $"{node_OC[i].U}" + ";"
						+ $"{node_OC[i].Delta}" + ";"
						+ "0" + ";"
						+ "0" + ";"
						+ $"{node_OC[i].P * (-1)}" + ";"
						+ $"{node_OC[i].Q * (-1)}" + "\n");
				}
				else
				{
					inform += ($"{node_OC[i].NumberNode}" + ";"
						+ $"{node_OC[i].Sostoyanie}" + ";"
						+ $"{node_OC[i].U}" + ";"
						+ $"{node_OC[i].Delta}" + ";"
						+ $"{node_OC[i].P}" + ";"
						+ $"{node_OC[i].Q}" + ";"
						+ "0" + ";"
						+ "0" + "\n");
				}
			}
			return inform;
		}
		/// <summary>
		/// Сохранить файл
		/// </summary>
		/// <param name="node_OC"></param>
		/// <param name="filename"></param>
		public static void SaveFile(List<Node_TM> node_OC, string filename)
		{
			string inform = Information(node_OC);
            File.WriteAllText(filename, inform.Replace(",","."));
		}

		/// <summary>
		/// Подсчёт кол-ва ТМ
		/// </summary>
		/// <param name="nodeTMList"></param>
		/// <returns></returns>
		public static int CountTM(List<Node_TM> nodeTMList, int nodeBase)
		{
			int result = nodeTMList.Count(x => x.P != x._nullValue);
			result += nodeTMList.Count(x => x.Q != x._nullValue);
			result += nodeTMList.Count(x => x.U != x._nullValue);
			result += nodeTMList.Count(x => x.Delta != x._nullValue);
			return result;
		}
		public string GetSosteNode()
		{
			return _keySost[Sostoyanie];
		}
		public bool GetSosteNode(string sost)
		{
			if (sost == "Выкл.")
				return false;
			else
				return true;
		}
		private static Random _rand = new Random();
		/// <summary>
		/// Формирование набора ТМ
		/// </summary>
		/// <param name="node_TM"></param>
		/// <returns></returns>
		public static List<Node_TM> GetNode_TM(List<Node_TM> node_TM)
		{
			for (int i=0; i< node_TM.Count; i++)
			{
				node_TM[i].P += node_TM[i].P * 1 / 300 * ( _rand.NextDouble() * 2 - 1);
				node_TM[i].Q += node_TM[i].Q * 1/ 300 * (_rand.NextDouble() * 2 - 1);
				node_TM[i].U += node_TM[i].U * 1 / 300 * (_rand.NextDouble() * 2 - 1);
				//node_TM[i].Delta += node_TM[i].Delta * 0.5 / 300 * _rand.NextDouble();
				//if (node_TM[i].Delta != 0)
				//{

					
				//	node_TM[i].Delta +=-0.1;
				//}
				//else
				//{
					//node_TM[i].U += node_TM[i].U * 1 / 300 * _rand.NextDouble();					
					//node_TM[i].P += node_TM[i].P * 2 / 300 * _rand.NextDouble();
					//node_TM[i].Q += node_TM[i].Q * 2 / 300 * _rand.NextDouble();
				//}
			}
			return node_TM;
		}
	}
}
