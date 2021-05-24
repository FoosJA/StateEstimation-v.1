using System;
using System.Collections.Generic;

namespace ClassLibrarySE
{
	public class VectorSostoyaniya
	{
		/// <summary>
		/// Номер узла
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// Действующее значение напряжения
		/// </summary>
		public double V { get; set; }
		
		/// <summary>
		/// Угол напряжения
		/// </summary>
		public double Delta { get; set; }
		
		/// <summary>
		/// Конструктор класса 
		/// </summary>
		/// <param name="numb">Номер узла</param>
		/// <param name="v">Напряжение</param>
		/// <param name="delta">Угол</param>
		private void ParamNode(int numb, double v, double delta)
		{
			Number = numb;
			V = v;
			Delta = delta;
		}
		
		/// <summary>
		/// Заполнение листа значений напряжений U,б
		/// </summary>
		/// <param name="nodeTMList">Таблица Узлы</param>
		/// <returns>Таблица U,б </returns>
		public static List<VectorSostoyaniya> Initializathion(List<Node_TM> nodeTMList, List<Node> nodeList, List<NodeVes> nodeVesList)
		{
			
			List<VectorSostoyaniya> nodeUList = new List<VectorSostoyaniya>();
			int numb;
			double v;
			double delta;
			for (int i = 0; i < nodeTMList.Count; i++)
			{
				VectorSostoyaniya vector = new VectorSostoyaniya();
				numb = nodeTMList[i].NumberNode;
				if (nodeTMList[i].U != 0)
				{
					v = nodeTMList.Find(x => x.NumberNode == numb).U;
				}
				else if (nodeVesList.Find(x=>x.NumberNode==numb).Ubr !=0)
				{
					v = nodeVesList.Find(x => x.NumberNode == numb).Ubr;
				}
				else
				{
					v = nodeList.Find(x => x.NumberNode == numb).U_nom;
				}

				if (nodeTMList[i].Delta != 0)
				{
					delta = nodeTMList.Find(x => x.NumberNode == numb).Delta;
				}
				else if (nodeVesList.Find(x=>x.NumberNode == numb).Delta !=0 )
				{
					delta = nodeVesList.Find(x => x.NumberNode == numb).Delta;
				}
				else
				{
					delta = 0;
				}
				vector.ParamNode(numb, v, delta);
				nodeUList.Add(vector);
			}
			return nodeUList;
		}
		
		/// <summary>
		/// Создание вектора состояния
		/// </summary>
		/// <param name="nodeUList"></param>
		/// <returns>Вектор состояния</returns>
		public static Matrix Vector_U(List<VectorSostoyaniya> nodeUList, int nodeBase)
		{
			int j = 0;// счетчик строк в U
			int k = 2 * nodeUList.Count - 1;
			Matrix U = new Matrix(k, 1);
			for (int i = 0; i < nodeUList.Count; i++)
			{
				U[j, 0] = nodeUList[i].V;
				j++;
				if (nodeUList[i].Number != nodeBase)
				{
					U[j, 0] = nodeUList[i].Delta;
					j++;
				}
			}
			return U;
		}
		
		/// <summary>
		/// Обновление значений напряжений в узлах
		/// </summary>
		/// <param name="nodeUList">Исходные значения напряжений</param>
		/// <param name="newU">Новый вектор состояния</param>
		/// <param name="nodeBase">Базовый узел</param>
		/// <returns></returns>
		public static List<VectorSostoyaniya> ObnovVector(List<VectorSostoyaniya> nodeUList, Matrix newU, int nodeBase)
		{
			int j = 0;
			int k = nodeUList.Count;
			for (int i = 0; i < k; i++)
			{
				nodeUList[i].V = newU[j, 0];
				j++;
				if (nodeUList[i].Number != nodeBase)
				{
					nodeUList[i].Delta = newU[j, 0];
					j++;
				}
			}
			return nodeUList;
		}
	}
}
