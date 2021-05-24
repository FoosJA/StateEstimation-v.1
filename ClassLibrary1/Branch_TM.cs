using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ClassLibrarySE
{
	/// <summary>
	/// ТМ в ветвях
	/// </summary>
	public class Branch_TM
	{
        private double _nullValue = 0;
		private Dictionary<bool, string> _keySost = new Dictionary<bool, string>
		{
			[true] = "Вкл.",
			[false] = "Выкл.",
		};

        #region Parametrs
        /// <summary>
        /// Узел начала ветви
        /// </summary>
        private int node_nach;

		/// <summary>
		/// Узел начала ветви
		/// </summary>
		public int Node_nach
		{
            get { return node_nach; }
            set { node_nach = value; }
        }

		/// <summary>
		/// Узел конца ветви
		/// </summary>
		private int node_kon;

		/// <summary>
		/// Узел конца ветви
		/// </summary>
		public int Node_kon
		{
            get { return node_kon; }
            set { node_kon = value; }
        }

		/// <summary>
		/// Состояние ветви
		/// </summary>
		private bool sostoyanie;
		
		/// <summary>
		/// Состояние ветви
		/// </summary>
		public bool Sostoyanie
		{
            get { return sostoyanie; }
            set { sostoyanie = value; }
        }

		/// <summary>
		/// Активная мощность в начале ветви
		/// </summary>
		private double  p_nach;
		/// <summary>
		/// Активная мощность в начале ветви
		/// </summary>
		public double  P_nach
        {
            get { return p_nach; }
            set { p_nach = value; }
        }

        /// <summary>
        /// Реактивная мощноть в начале ветви
        /// </summary>
        private double q_nach;
		
		/// <summary>
		/// Реактивная мощноть в начале ветви
		/// </summary>
		public double  Q_nach
		{
            get { return q_nach; }
            set { q_nach = value; }
        }

        /// <summary>
        /// Активная мощность в конце ветви
        /// </summary>
        private double  p_kon;
		
		/// <summary>
		/// Активная мощность в конце ветви
		/// </summary>
		public double  P_kon
        {
            get { return p_kon; }
            set { p_kon = value; }
        }

		/// <summary>
		/// Реактивная мощность в конце ветви
		/// </summary>
		private double  q_kon;
		
		/// <summary>
		/// Реактивная мощность в конце ветви
		/// </summary>		
		public double  Q_kon
		{
            get { return q_kon; }
            set { q_kon = value; }
        }

        /// <summary>
        /// Ток в начале линии
        /// </summary>
        private double  i_nach;
		
		/// <summary>
		/// Ток в начале линии
		/// </summary>
		public double  I_nach
        {
            get { return i_nach; }
            set { i_nach = value; }
        }

		/// <summary>
		/// Ток в конце линии
		/// </summary>		
		private double  i_kon;
		/// <summary>
		/// Ток в конце линии
		/// </summary>
		public double  I_kon
        {
            get { return i_kon; }
            set { i_kon = value; }
        }

        /// <summary>
        /// Угол тока в начале ветви
        /// </summary>
        private double  sigma_nach;

		/// <summary>
		/// Угол тока в начале ветви
		/// </summary>
		public double  Sigma_nach
        {
            get { return sigma_nach; }
            set { sigma_nach = value; }
        }

        /// <summary>
        /// Угол тока в конце ветви
        /// </summary>
        private double  sigma_kon;

		/// <summary>
		/// Угол в конце ветви
		/// </summary>
		public double  Sigma_kon
		{
            get { return sigma_kon; }
            set { sigma_kon = value; }
        }
#endregion
        
        /// <summary>
        /// Деление строки
        /// </summary>
        /// <param name="line">Строка</param>
        private void Division(string line)
		{
			string[] parts = line.Split(';');
            Node_nach = Int32.Parse(parts[0]);
            Node_kon =Int32.Parse(parts[1]);
            if (parts[2] == "1")
                Sostoyanie = false;
            else
                Sostoyanie = true;
            try { P_nach = Double.Parse(parts[3]); }
            catch { P_nach = _nullValue; }
            try { Q_nach = Double.Parse(parts[4]); }
            catch { Q_nach = _nullValue; }
            try { P_kon = Double.Parse(parts[5]); }
            catch { P_kon = _nullValue; }
            try { Q_kon = Double.Parse(parts[6]); }
            catch { Q_kon = _nullValue; }
            try { I_nach = Double.Parse(parts[7]); }
            catch { I_nach = _nullValue; }
            try { I_kon = Double.Parse(parts[8]); } //Ток в Амперах!
            catch { I_kon = _nullValue; }
            try { Sigma_nach = Double.Parse(parts[9]); }
            catch { Sigma_nach = _nullValue; }
            try { Sigma_kon = Double.Parse(parts[10]); }
            catch { Sigma_kon = _nullValue; }
            
            //Sigma_nach = Math.Atan(Q_nach / P_nach) / (Math.PI / 180);
            //sigma_kon = Math.Atan(Q_kon / P_kon) / (Math.PI / 180);
            
		}

		/// <summary>
		/// Чтение из файла
		/// </summary>
		/// <param name="filename">Путь к файлу</param>
		/// <returns>Список ветвей</returns>
		public static List<Branch_TM> ReadFileBranch(string filename)
		{
			try
			{
				List<Branch_TM> branchTMList = new List<Branch_TM>();
				using (StreamReader file_csv = new StreamReader(filename))
				{
					string line;
					while ((line = file_csv.ReadLine()) != null)
					{
						Branch_TM branchTM = new Branch_TM();
						branchTM.Division(line.Replace(".",","));
                        branchTMList.Add(branchTM);						
					}
				}
				return branchTMList;
			}
			catch (System.IO.IOException)
			{
				return null;
			}
        }

		/// <summary>
		/// Расчет кол-ва ТМ
		/// </summary>
		/// <param name="branchTMList"></param>
		/// <returns></returns>
		public static int CountTM(List<Branch_TM> branchTMList)
		{
			int result = branchTMList.Count(x => x.P_nach != x._nullValue);
			result += branchTMList.Count(x => x.Q_nach != x._nullValue);
			result += branchTMList.Count(x => x.P_kon != x._nullValue);
			result += branchTMList.Count(x => x.Q_kon != x._nullValue);
			result += branchTMList.Count(x => x.I_nach != x._nullValue);
			result += branchTMList.Count(x => x.I_kon != x._nullValue);
			result += branchTMList.Count(x => x.Sigma_nach != x._nullValue);
			result += branchTMList.Count(x => x.sigma_kon != x._nullValue);
			return result;
		}

		/// <summary>
		/// Вывод информации
		/// </summary>
		/// <param name="branch_OC"></param>
		/// <returns></returns>
		public static string Information(List<Branch_TM> branch_OC)
		{
			//string inform = "Number;Number;Sostoyanie;Pnach;Qnach;Pkon;Qkon\n";
			string inform = String.Empty;
			for (int i = 0; i < branch_OC.Count; i++)
			{
				inform += ($"{branch_OC[i].Node_nach}" + ";"
						+ $"{branch_OC[i].Node_kon}" + ";"
						+ $"{branch_OC[i].Sostoyanie}" + ";"
						+ $"{branch_OC[i].P_nach}" + ";"
						+ $"{branch_OC[i].Q_nach}" + ";"
						+ $"{branch_OC[i].P_kon}" + ";"
						+ $"{branch_OC[i].Q_kon}" + ";"
						+ $"{branch_OC[i].I_nach}" + ";"
						+ $"{branch_OC[i].I_kon}" + ";"
						+ $"{branch_OC[i].Sigma_nach}" + ";"
						+ $"{branch_OC[i].sigma_kon}" + "\n");
			}
			return inform;
		}
		
		/// <summary>
		/// Сохранить в файл
		/// </summary>
		/// <param name="branch_OC"></param>
		/// <param name="filename"></param>
		public static void SaveFile(List<Branch_TM> branch_OC, string filename)
		{
			string inform = Information(branch_OC);
			File.WriteAllText(filename, inform.Replace(",","."));
			Console.WriteLine("Файл сохранен!");
		}
		
		public string GetSostBranch()
		{
			return _keySost[Sostoyanie];
		}
		public bool GetSostBranch(string str)
		{
			if (str == "Выкл.")			
				return false;			
			else
				return true;
		}
		private static Random _rand = new Random();
		/// <summary>
		/// Формаирование набора ТМ
		/// </summary>
		/// <param name="branch_TMs"></param>
		/// <returns></returns>
		public static List<Branch_TM> GetBranch_TM(List<Branch_TM> branch_TMs)
		{
			for (int i = 0; i < branch_TMs.Count; i++)
			{
				branch_TMs[i].P_nach += branch_TMs[i].P_nach * 0.5 / 300 * _rand.NextDouble();
				branch_TMs[i].Q_nach += branch_TMs[i].Q_nach * 0.5 / 300 * _rand.NextDouble();
				branch_TMs[i].P_kon += branch_TMs[i].P_nach * 0.5 / 300 * _rand.NextDouble();
				branch_TMs[i].Q_kon += branch_TMs[i].Q_nach * 0.5 / 300 * _rand.NextDouble();
				
				//branch_TMs[i].P_kon += _rand.Next(-3, 3) + _rand.NextDouble();
				//branch_TMs[i].P_nach += _rand.Next(-3, 3) + _rand.NextDouble();
				//branch_TMs[i].Q_nach += _rand.Next(-3, 3) + _rand.NextDouble();
				//branch_TMs[i].Q_kon += _rand.Next(-3, 3) + _rand.NextDouble();
				//branch_TMs[i].I_nach += (_rand.Next(-2, 2) + _rand.NextDouble()) / 1000;
				//branch_TMs[i].I_kon += (_rand.Next(-2, 2) + _rand.NextDouble()) / 1000;
				//branch_TMs[i].Sigma_nach += _rand.Next(-8, 8) * 0.1;
				//branch_TMs[i].sigma_kon += _rand.Next(-8, 8) * 0.1;
			}
			return branch_TMs;
		}
	}
}
