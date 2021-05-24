using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLibrarySE
{
	/// <summary>
	/// Параметры схемы замещения линий
	/// </summary>
    public class Branch
    {

    #region Parametrs
        /// <summary>
        /// Номер ветви
        /// </summary>
        private int numberNode;

		/// <summary>
		/// Номер ветви
		/// </summary>
		public int NumberBranch
        {
            get { return numberNode; }
            set { numberNode = value; }
        }

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
		/// Активное сопротивление
		/// </summary>
		public double r;

		/// <summary>
		/// Активное сопротивление
		/// </summary>
		public double R
		{
			get { return r; }
			set { r = value; }
		}

		/// <summary>
		/// Реактивное сопротивление
		/// </summary>
		private double x;

		/// <summary>
		/// Реактивное сопротивление
		/// </summary>
		public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Активная проводимость
        /// </summary>
        private double g;

		/// <summary>
		/// Активная проводимость
		/// </summary>
		public double G
		{
			get { return g; }
			set { g = value; }
		}

		/// <summary>
		/// Реактивная проводимость
		/// </summary>
		private double b;

		/// <summary>
		/// Реактивная проводимость
		/// </summary>
		public double B
		{
			get { return b; }
			set { b = value; }
		}

		/// <summary>
		/// Реактивная проводимость ВЛ
		/// </summary>
		private double bline;

		/// <summary>
		/// Реактивная проводимость ВЛ [Cм]
		/// </summary>
		public double Bline
		{
			get { return bline; }
			set { bline = value ; }
		}

		/// <summary>
		/// Коэф трансформации
		/// </summary>
		private double ktr;

		/// <summary>
		/// Коэф трансформации
		/// </summary>
		public double Ktr
		{
			get { return ktr; }
			set { ktr = value; }
		}
    #endregion

        /// <summary>
        /// Деление строки
        /// </summary>
        /// <param name="line">Строка</param>
        private void Division(string line, int i)
		{
			string[] parts = line.Split(';');
			NumberBranch = i;
            Node_nach = Int32.Parse(parts[0]);
            Node_kon = Int32.Parse(parts[1]);
            try { R = Double.Parse(parts[2]); }
            catch { R = 0; }
            try { X = Double.Parse(parts[3]); }
            catch { X = 0; }
            try { Bline = Double.Parse(parts[4]) / 2; }
            catch { Bline = 0; }
            try { Ktr = Double.Parse(parts[5]); }
            catch { Ktr= 1; }
			G = R / (R * R + X * X);
			B = X / (R * R + X * X);
		}
		
		/// <summary>
		/// Чтение из файла
		/// </summary>
		/// <param name="filename">Путь к файлу</param>
		/// <returns>Список ветвей</returns>
		public static List<Branch> ReadFileBranch(string filename)
		{
			List<Branch> branchList = new List<Branch>();
			using (StreamReader file_csv = new StreamReader(filename))
			{
				string line;
				int i = 1;
				while ((line = file_csv.ReadLine()) != null)
				{
					Branch branch = new Branch();
					branch.Division(line.Replace(".",","),i);
					branchList.Add(branch);
					i++;
				}
			}
			return branchList;
		}
		public static void SaveFile(List<Branch> branches, string filename)
		{
			string inform = Information(branches);
			File.WriteAllText(filename, inform);
		}
		private static string Information(List<Branch> branches)
		{
			string inform = "Node_nach;Node_kon;R;X;kтр\n";
			for (int i = 0; i < branches.Count; i++)
			{
				inform += ($"{branches[i].Node_nach}" + ";"
					+ $"{branches[i].Node_kon}" + ";"
					+ $"{branches[i].R}" + ";"
					+ $"{branches[i].X}" + ";"
                    + $"{branches[i].Bline*2}" + ";"
                    + $"{branches[i].Ktr}" + "\n");
			}
			return inform;
		}
	}
}
