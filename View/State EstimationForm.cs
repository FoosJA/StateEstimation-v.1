using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ClassLibrarySE;
using System.IO;
using System.Numerics;


namespace View
{
	public partial class StateEstimationForm : Form
	{
		public StateEstimationForm()
		{
			
			InitializeComponent();
			listTable.SelectedIndexChanged += listTable_SelectedIndexChange;
		}
        private string filename = (@"D:\НИР\Чувашия 2021\ОЗ ОДУ СВ");
        private string filenameTM = (@"D:\НИР\Чувашия 2021\ОЗ ОДУ СВ");
        private List<Branch> _branchList;
		private List<Branch_TM> _branchTMList;
		private List<BranchVes> _branchVesList;
		private List<Node> _nodeList;
		private List<Node_TM> _nodeTMList;
		private List<NodeVes> _nodeVesList;
		private List<Node_TM> _nodeOCList = new List<Node_TM>();
		private List<Branch_TM> _branchOCList = new List<Branch_TM>();
		private double _error=1000;
		private double maxError = 0.1;		
		private int _nomerIterMax = 120;
        private int _nodeError = 2;
        private bool flagVesCoef=true;
        private int nomerIterac;
        private string targerFunction="1000";

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			UpdateTable();
		}
		/// <summary>
		/// Обновление таблиц
		/// </summary>
		private void  UpdateTable()
		{
			try
			{
				int index = listTable.SelectedIndex;
				switch (index)
				{
					case 0:
						_nodeList.Clear();
						for (int rows = 0; rows < dataGridView.Rows.Count - 1; rows++)
						{
							Node node = new Node();
							node.Type = node.GetTypeNode(dataGridView.Rows[rows].Cells[0].Value.ToString());
							node.NumberNode = Convert.ToInt32(dataGridView.Rows[rows].Cells[1].Value);
							node.U_nom = Convert.ToDouble(dataGridView.Rows[rows].Cells[2].Value);
							node.B = Convert.ToDouble(dataGridView.Rows[rows].Cells[3].Value);
							_nodeList.Add(node);
						}
						break;
					case 1:
						_branchList.Clear();
						for (int rows=0; rows < dataGridView.Rows.Count-1; rows++)
						{
							Branch branch = new Branch();
							branch.NumberBranch = rows + 1;
							branch.Node_nach = Convert.ToInt32(dataGridView.Rows[rows].Cells[0].Value);
							branch.Node_kon = Convert.ToInt32(dataGridView.Rows[rows].Cells[1].Value);
							branch.R = Convert.ToDouble(dataGridView.Rows[rows].Cells[2].Value);
							branch.X = Convert.ToDouble(dataGridView.Rows[rows].Cells[3].Value);
							branch.Ktr= Convert.ToDouble(dataGridView.Rows[rows].Cells[4].Value);
							_branchList.Add(branch);
						}
						break;
					case 2:
						_nodeVesList.Clear();
						for (int rows=0; rows<dataGridView.Rows.Count-1;rows++)
						{
							NodeVes nodeVes = new NodeVes();
							nodeVes.NumberNode = Convert.ToInt32(dataGridView.Rows[rows].Cells[0].Value);
							nodeVes.VesP = Convert.ToDouble(dataGridView.Rows[rows].Cells[1].Value);
							nodeVes.VesQ = Convert.ToDouble(dataGridView.Rows[rows].Cells[2].Value);
							nodeVes.VesU = Convert.ToDouble(dataGridView.Rows[rows].Cells[3].Value);
							nodeVes.Ubr = Convert.ToDouble(dataGridView.Rows[rows].Cells[4].Value);
							nodeVes.Delta = Convert.ToDouble(dataGridView.Rows[rows].Cells[5].Value);
							nodeVes.Ppi = Convert.ToDouble(dataGridView.Rows[rows].Cells[6].Value);
							nodeVes.Qpi = Convert.ToDouble(dataGridView.Rows[rows].Cells[7].Value);
							_nodeVesList.Add(nodeVes);
						}
						break;
					case 3:
						_branchVesList.Clear();
						for (int rows = 0; rows < dataGridView.Rows.Count - 1; rows++)
						{
							BranchVes branchVes = new BranchVes();
							branchVes.NumberBranch= Convert.ToInt32(dataGridView.Rows[rows].Cells[0].Value);
							branchVes.VesP = Convert.ToDouble(dataGridView.Rows[rows].Cells[1].Value);
							branchVes.VesQ = Convert.ToDouble(dataGridView.Rows[rows].Cells[2].Value);
							branchVes.VesI = Convert.ToDouble(dataGridView.Rows[rows].Cells[3].Value);
							branchVes.VesSigma = Convert.ToDouble(dataGridView.Rows[rows].Cells[4].Value);
							_branchVesList.Add(branchVes);
						}
						break;
					case 4:
						_nodeTMList.Clear();
						for (int rows = 0; rows < dataGridView.Rows.Count - 1; rows++)
						{
							Node_TM node_TM = new Node_TM();
							node_TM.NumberNode = Convert.ToInt32(dataGridView.Rows[rows].Cells[0].Value);
							node_TM.Sostoyanie = node_TM.GetSosteNode(Convert.ToString(dataGridView.Rows[rows].Cells[1].Value));
							node_TM.U = Convert.ToDouble(dataGridView.Rows[rows].Cells[2].Value);
							node_TM.Delta= Convert.ToDouble(dataGridView.Rows[rows].Cells[3].Value);
							node_TM.P= Convert.ToDouble(dataGridView.Rows[rows].Cells[4].Value);
							node_TM.Q= Convert.ToDouble(dataGridView.Rows[rows].Cells[5].Value);
							_nodeTMList.Add(node_TM);
						}
						break;
					case 5:
						_branchTMList.Clear();
						for (int rows = 0; rows < dataGridView.Rows.Count - 1; rows++)
						{
							Branch_TM branch_TM = new Branch_TM();
							branch_TM.Node_nach= Convert.ToInt32(dataGridView.Rows[rows].Cells[0].Value);
							branch_TM.Node_kon = Convert.ToInt32(dataGridView.Rows[rows].Cells[1].Value);
							branch_TM.Sostoyanie = branch_TM.GetSostBranch(Convert.ToString(dataGridView.Rows[rows].Cells[2].Value));
							branch_TM.P_nach = Convert.ToDouble(dataGridView.Rows[rows].Cells[3].Value);
							branch_TM.Q_nach = Convert.ToDouble(dataGridView.Rows[rows].Cells[4].Value);
							branch_TM.P_kon = Convert.ToDouble(dataGridView.Rows[rows].Cells[5].Value);
							branch_TM.Q_kon = Convert.ToDouble(dataGridView.Rows[rows].Cells[6].Value);
							branch_TM.I_nach = Convert.ToDouble(dataGridView.Rows[rows].Cells[7].Value);
							branch_TM.I_kon = Convert.ToDouble(dataGridView.Rows[rows].Cells[8].Value);
							branch_TM.Sigma_nach = Convert.ToDouble(dataGridView.Rows[rows].Cells[9].Value);
							branch_TM.Sigma_kon = Convert.ToDouble(dataGridView.Rows[rows].Cells[10].Value);
							_branchTMList.Add(branch_TM);
						}
						break;

				}
			}
			catch (System.NullReferenceException)
			{ }
			catch (FormatException)
			{

				eventLog.Text += Environment.NewLine + "Формат неверный!";
			}

		}

		/// <summary>
		/// Переключение между таблицами
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listTable_SelectedIndexChange(object sender, EventArgs e)
		{
			try
			{
				int index = listTable.SelectedIndex;
				switch (index)
				{
					case 0:
						dataGridView.Rows.Clear();
						dataGridView.ColumnCount = 4;
						dataGridView.Columns[0].Name = "Тип";
						dataGridView.Columns[1].Name = "№ Узла";
						dataGridView.Columns[2].Name = "Uном, кВ";
						dataGridView.Columns[3].Name = "Bш, мкСм";
						for (int i = 0; i < _nodeList.Count; i++)
						{
							dataGridView.Rows.Add(_nodeList[i].GetTypeNode(), _nodeList[i].NumberNode, _nodeList[i].U_nom.ToString("0.000"),
							 (_nodeList[i].B).ToString("0.000"));
						}
						break;
					case 1:
						dataGridView.Rows.Clear();
						dataGridView.ColumnCount = 6;
						dataGridView.Columns[0].Name = "№ узла начала";
						dataGridView.Columns[1].Name = "№ узла конца";
						dataGridView.Columns[2].Name = "R, Ом";
						dataGridView.Columns[3].Name = "X, Ом";
						dataGridView.Columns[4].Name = "B, мкСм";
						dataGridView.Columns[5].Name = "Kрт";
						for (int i = 0; i < _branchList.Count; i++)
						{
							dataGridView.Rows.Add(_branchList[i].Node_nach, _branchList[i].Node_kon,
								_branchList[i].R, _branchList[i].X, (_branchList[i].Bline).ToString("0.000"), _branchList[i].Ktr.ToString("0.000"));
						}
						break;
					case 2:
						dataGridView.Rows.Clear();
						dataGridView.ColumnCount = 8;
						dataGridView.Columns[0].Name = "№ узла";
						dataGridView.Columns[1].Name = "Вес P";
						dataGridView.Columns[2].Name = "Вес Q";
						dataGridView.Columns[3].Name = "Вес U";
						dataGridView.Columns[4].Name = "Uбр, кВ";
						dataGridView.Columns[5].Name = "Delta, град";
						dataGridView.Columns[6].Name = "ПИ Р МВт";
						dataGridView.Columns[7].Name = "ПИ Q, МВар";
						for (int i = 0; i < _nodeVesList.Count; i++)
						{
							dataGridView.Rows.Add(_nodeVesList[i].NumberNode, _nodeVesList[i].VesP, _nodeVesList[i].VesQ, _nodeVesList[i].VesU,
								_nodeVesList[i].Ubr, _nodeVesList[i].Delta.ToString("0.00000"), _nodeVesList[i].Ppi, _nodeVesList[i].Qpi);
						}
						break;
					case 3:
						dataGridView.Rows.Clear();
						dataGridView.ColumnCount = 5;
						dataGridView.Columns[0].Name = "№ ветви";
						dataGridView.Columns[1].Name = "Вес P";
						dataGridView.Columns[2].Name = "Вес Q";
						dataGridView.Columns[3].Name = "Вес I";
						dataGridView.Columns[4].Name = "Вес Sigma";
						for (int i = 0; i < _branchVesList.Count; i++)
						{
							dataGridView.Rows.Add(_branchVesList[i].NumberBranch, _branchVesList[i].VesP, _branchVesList[i].VesQ,
								_branchVesList[i].VesI, _branchVesList[i].VesSigma);
						}
						break;
					case 4:
						dataGridView.Rows.Clear();
						dataGridView.ColumnCount = 6;
						dataGridView.Columns[0].Name = "№ узла";
						dataGridView.Columns[1].Name = "Сост.";
						dataGridView.Columns[2].Name = "U, кВ";
						dataGridView.Columns[3].Name = "Delta, град";
						dataGridView.Columns[4].Name = "Р, МВт";
						dataGridView.Columns[5].Name = "Q, МВар";
						for (int i = 0; i < _nodeTMList.Count; i++)
						{
							dataGridView.Rows.Add(_nodeTMList[i].NumberNode, _nodeTMList[i].GetSosteNode(),
								_nodeTMList[i].U.ToString("0.000"), _nodeTMList[i].Delta.ToString("0.00000"),
								_nodeTMList[i].P.ToString("0.00"), _nodeTMList[i].Q.ToString("0.00"));
						}
						break;
					case 5:
						dataGridView.Rows.Clear();
						dataGridView.ColumnCount =11;
						dataGridView.Columns[0].Name = "№ узла нач";
						dataGridView.Columns[1].Name = "№ узла кон";
						dataGridView.Columns[2].Name = "Сост.";
						dataGridView.Columns[3].Name = "Pнач, МВт";
						dataGridView.Columns[4].Name = "Qнач, МВар";
						dataGridView.Columns[5].Name = "Pкон, МВт";						
						dataGridView.Columns[6].Name = "Qкон, МВар";
						dataGridView.Columns[7].Name = "Iнач, А";
						dataGridView.Columns[8].Name = "Iкон, А";
						dataGridView.Columns[9].Name = "Sigma нач, град";
						dataGridView.Columns[10].Name = "Sigma кон, град";

						for (int i = 0; i < _branchTMList.Count; i++)
						{
							dataGridView.Rows.Add(_branchTMList[i].Node_nach, _branchTMList[i].Node_kon, _branchTMList[i].GetSostBranch(),
								_branchTMList[i].P_nach.ToString("0.00"), _branchTMList[i].Q_nach.ToString("0.00"), 
								_branchTMList[i].P_kon.ToString("0.00"), _branchTMList[i].Q_kon.ToString("0.00"),
							_branchTMList[i].I_nach.ToString("0.000"), _branchTMList[i].I_kon.ToString("0.000"),
							_branchTMList[i].Sigma_nach.ToString("0.00"), _branchTMList[i].Sigma_kon.ToString("0.00"));
				        }
						break;
						
					case 6:
					
						dataGridView.Rows.Clear();
						dataGridView.ColumnCount = 6;
						dataGridView.Columns[0].Name = "№ узла";
						dataGridView.Columns[1].Name = "Сост.";
						dataGridView.Columns[2].Name = "U, кВ";
						dataGridView.Columns[3].Name = "Delta, град";
						dataGridView.Columns[4].Name = "Р, МВт";
						dataGridView.Columns[5].Name = "Q, МВар";
						if (_nodeOCList.Count!=0)
						{
							for (int i = 0; i < _nodeTMList.Count; i++)
							{
								dataGridView.Rows.Add(_nodeOCList[i].NumberNode, _nodeOCList[i].GetSosteNode(),
									_nodeOCList[i].U.ToString("0.000"), _nodeOCList[i].Delta.ToString("0.00000"),
									_nodeOCList[i].P.ToString("0.00"), _nodeOCList[i].Q.ToString("0.00"));
							}
						}
						
						break;
						
					case 7:
						dataGridView.Rows.Clear();
						dataGridView.ColumnCount = 11;
						dataGridView.Columns[0].Name = "№ узла нач";
						dataGridView.Columns[1].Name = "№ узла кон";
						dataGridView.Columns[2].Name = "Сост.";
						dataGridView.Columns[3].Name = "Pнач, МВт";
						dataGridView.Columns[4].Name = "Pкон, МВт";
						dataGridView.Columns[5].Name = "Qнач, МВар";
						dataGridView.Columns[6].Name = "Qкон, МВар";
						dataGridView.Columns[7].Name = "Iнач, А";
						dataGridView.Columns[8].Name = "Iкон, А";
						dataGridView.Columns[9].Name = "Sigma нач, град";
						dataGridView.Columns[10].Name = "Sigma кон, град";
						if (_branchOCList.Count!=0)
						{
							for (int i = 0; i < _branchOCList.Count; i++)
							{
								dataGridView.Rows.Add(_branchOCList[i].Node_nach, _branchOCList[i].Node_kon, _branchOCList[i].GetSostBranch(),
									_branchOCList[i].P_nach.ToString("0.00"), _branchOCList[i].P_kon.ToString("0.00"),
									_branchOCList[i].Q_nach.ToString("0.00"), _branchOCList[i].Q_kon.ToString("0.00"), _branchOCList[i].I_nach.ToString("0.00"), _branchOCList[i].I_kon.ToString("0.00"),
							_branchOCList[i].Sigma_nach.ToString("0.00"), _branchOCList[i].Sigma_kon.ToString("0.00"));
							}
						}
						
						break;
					default:
						break;
				}
			}
			catch (System.NullReferenceException) { }
		}


		/// <summary>
		/// Загрузить параметры сх.замещения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void загрузитьпараметрыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{     
                FolderBrowserDialog folder = new FolderBrowserDialog { };
				//TODO: Переключение на выбор директории
				//var filename = String.Empty;
				//folder.SelectedPath = Directory.GetCurrentDirectory();
				folder.SelectedPath = filename;

				if (folder.ShowDialog() == DialogResult.OK)
				{                    
                    _nodeList = Node.ReadFileNode(folder.SelectedPath + @"\Node.csv");
					_branchList = Branch.ReadFileBranch(folder.SelectedPath + @"\Branch.csv");
					int nodeBase = _nodeList.Find(x => x.Type == 0).NumberNode;
					var index = _nodeList.FindIndex(x => x.NumberNode == nodeBase);
					if (index != (_nodeList.Count - 1))
					{
						Node baseNode = new Node();
						baseNode = _nodeList[index];
						_nodeList.Remove(_nodeList[index]);
						_nodeList.Add(baseNode);
					}
					_nodeVesList = NodeVes.GetVes(_nodeList);
					_branchVesList = BranchVes.GetVes(_branchList, _nodeList);			
					eventLog.Text += Environment.NewLine+"Параметры сети загружены!";
				}

			}
			catch (FileNotFoundException)
			{
				eventLog.Text += Environment.NewLine + "Файл не найден!";
			}
		}

		/// <summary>
		/// Расчет ОС
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSE_Click(object sender, EventArgs e)
		{
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string infoNode = "";
            string infoBranch = "";
            int test = 0;
            //for (double d = -5.3; d <= 6; d += 1)
            //for (int test=0; test <=1;test+=1)
            //{
            /*
            if (test == 0)
            {
                _nodeTMList.Find(x => x.NumberNode == _nodeError).Delta += -5.3;
                _branchTMList.Find(x => x.Node_nach == _nodeError).Sigma_nach += -5.3;
                _branchTMList.Find(x => x.Node_kon == _nodeError).Sigma_kon += -5.3;
            }
            else
            {
                _nodeTMList.Find(x => x.NumberNode == _nodeError).Delta += 10.6;
                _branchTMList.Find(x => x.Node_nach == _nodeError).Sigma_nach += 10.6;
                _branchTMList.Find(x => x.Node_kon == _nodeError).Sigma_kon += 10.6;
            }*/
                try
                {
                    int nodeBase = _nodeList.Find(x => x.Type == 0).NumberNode;
                    var index = _nodeList.FindIndex(x => x.NumberNode == nodeBase);
                    index = _nodeTMList.FindIndex(x => x.NumberNode == nodeBase);
                    if (index != _nodeTMList.Count - 1)
                    {
                        Node_TM node_TM = new Node_TM();
                        node_TM = _nodeTMList[index];
                        _nodeTMList.Remove(_nodeTMList[index]);
                        _nodeTMList.Add(node_TM);
                    }

                    List<VectorSostoyaniya> nodeUList = new List<VectorSostoyaniya>();
                    nodeUList = VectorSostoyaniya.Initializathion(_nodeTMList, _nodeList, _nodeVesList);
                    List<Result> resultList = new List<Result>();
                    int nodeCount = _nodeList.Count;
                    //кол-во компонентов вектора состояния
                    int K = 2 * nodeCount - 1;
                    //TODO: проверь учет ТМ для сетевого и базового узла
                    int measureCount = Branch_TM.CountTM(_branchTMList) + Node_TM.CountTM(_nodeTMList, nodeBase);
                    if (measureCount < K)
                        eventLog.Text += Environment.NewLine + "Режим не наблюдаемый!";
                    else
                    {
                        //Цикл алгоритма Гаусса-Ньютона-----------------------------------------------------	
                        nomerIterac = 1;
                        Matrix G = new Matrix(_nodeList.Count, _nodeList.Count);
                        Matrix B = new Matrix(_nodeList.Count, _nodeList.Count);
                        for (int i = 0; i < nodeCount; i++)
                        {
                            for (int j = 0; j < nodeCount; j++)
                            {
                                G[i, j] = 0;
                                B[i, j] = 0;
                            }
                        }
                        for (int i = 0; i < _branchList.Count; i++)
                        {
                            int nodeIndex_I = _nodeList.FindIndex(x => x.NumberNode == _branchList[i].Node_nach);
                            int nodeIndex_J = _nodeList.FindIndex(x => x.NumberNode == _branchList[i].Node_kon);
                            //if (_branchList[i].Ktr == 1)
                            //{
                            G[nodeIndex_I, nodeIndex_J] = _branchList[i].G;
                            G[nodeIndex_J, nodeIndex_I] = _branchList[i].G;
                            B[nodeIndex_I, nodeIndex_J] = _branchList[i].B;
                            B[nodeIndex_J, nodeIndex_I] = _branchList[i].B;
                            //}
                            //else
                            //{
                            //	G[nodeIndex_I, nodeIndex_J] =_branchList[i].G / _branchList[i].Ktr;
                            //	G[nodeIndex_J, nodeIndex_I] =_branchList[i].G / _branchList[i].Ktr;
                            //	B[nodeIndex_I, nodeIndex_J] = _branchList[i].B / _branchList[i].Ktr;
                            //	B[nodeIndex_J, nodeIndex_I] = _branchList[i].B / _branchList[i].Ktr;
                            //}
                        }
                        //for (int i = 0; i < _nodeList.Count; i++)
                        //{
                        //	double b = 0;
                        //	double g = 0;
                        //	int node_i = _nodeList[i].NumberNode;
                        //	List<Branch> branchNode = _branchList.Where(x => ((x.Node_kon == node_i)
                        //		| (x.Node_nach == node_i))).ToList<Branch>();
                        //	for (int j = 0; j < branchNode.Count; j++)
                        //	{
                        //		if (branchNode[j].Ktr == 1)
                        //		{
                        //			b += branchNode[j].Bline + branchNode[j].B;
                        //			//TODO: добавить G0 на землю
                        //			g += branchNode[j].G;
                        //		}
                        //		else
                        //		{
                        //			if (branchNode[j].Node_nach == node_i)
                        //			{
                        //				b += branchNode[j].B;
                        //				g += branchNode[j].G;
                        //			}
                        //			else
                        //			{
                        //				b += branchNode[j].B / (branchNode[j].Ktr * branchNode[j].Ktr);
                        //				g += branchNode[j].G / (branchNode[j].Ktr * branchNode[j].Ktr);
                        //			}
                        //		}
                        //	}
                        //	G[i, i] = g;
                        //	B[i, i] = -b;
                        //	G[i, i] = 0;
                        //	B[i, i] = 0;
                        //}
                        //Console.WriteLine(G);
                        //Console.WriteLine(B);

                        do
                        {
                            Matrix J = new Matrix(measureCount, K);
                            Matrix F = new Matrix(measureCount, 1);
                            Matrix U = new Matrix(K, 1);
                            U = VectorSostoyaniya.Vector_U(nodeUList, nodeBase);
                            Matrix C = new Matrix(measureCount, measureCount);

                            double Vi;
                            double Vj;
                            double gij;
                            double bij;
                            double gii = 0;
                            double bii = 0;
                            double delta_i;
                            double delta_j;
                            int m = 0;
                            int k = 0;
                            double Cij;
                            double Dij;
                            double Aij;
                            double Bij;
                            //Составляем уравнения узлов
                            for (int y = 0; y < _nodeTMList.Count; y++)
                            {
                                int node_i = _nodeTMList[y].NumberNode;
                                int node_j;
                                if (_nodeTMList[y].U != 0)
                                {
                                    F[m, 0] = 0;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k] = 1;
                                    if (flagVesCoef == true)
                                        C[m, m] = 1;
                                    else
                                        C[m, m] = _nodeVesList.Find(x => x.NumberNode == node_i).VesU;
                                    m++;
                                }
                                if ((_nodeTMList[y].Delta != 0) && (_nodeTMList[y].NumberNode != nodeBase))
                                {
                                    F[m, 0] = 0;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k + 1] = 1;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    if (flagVesCoef == true)
                                        C[m, m] = 1;
                                    else
                                        C[m, m] = _nodeVesList.Find(x => x.NumberNode == node_i).VesU;
                                    m++;
                                }
                                if (_nodeTMList[y].P != 0)
                                {
                                    double J_Vi = 0;
                                    double J_Deltai = 0;
                                    double Fi = 0;
                                    delta_i = nodeUList.Find(x => x.Number == node_i).Delta * Math.PI / 180;
                                    List<Branch_TM> branchNode = _branchTMList.Where(x => ((x.Node_kon == node_i)
                                    | (x.Node_nach == node_i))).ToList<Branch_TM>();
                                    for (int j = 0; j < branchNode.Count; j++)
                                    {
                                        if (branchNode[j].Node_nach != node_i)
                                        {
                                            node_j = branchNode[j].Node_nach;
                                            Vi = nodeUList.Find(x => x.Number == node_i).V;
                                            Vj = nodeUList.Find(x => x.Number == node_j).V;
                                            var ktr = _branchList.Find(x => ((x.Node_nach == node_j)
                                                && (x.Node_kon == node_i))).Ktr;
                                            if (ktr == 1)
                                            {
                                                gij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).G;
                                                bij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).B;
                                                gii = gij;
                                                bii = bij + _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).Bline * 0.000001;
                                            }
                                            else
                                            {
                                                gij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).G / ktr;
                                                bij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).B / ktr;
                                                if (Vi > Vj)
                                                {
                                                    gii = gij * ktr;
                                                    bii = bij * ktr;
                                                }
                                                else
                                                {
                                                    gii = gij / ktr;
                                                    bii = bij / ktr;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            node_j = branchNode[j].Node_kon;
                                            Vi = nodeUList.Find(x => x.Number == node_i).V;
                                            Vj = nodeUList.Find(x => x.Number == node_j).V;
                                            var ktr = _branchList.Find(x => ((x.Node_nach == node_i) &&
                                                (x.Node_kon == node_j))).Ktr;
                                            if (ktr != 1)
                                            {
                                                gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G;
                                                bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B;
                                                gii = gij;
                                                bii = bij + _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).Bline * 0.000001;
                                            }
                                            else
                                            {
                                                gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G / ktr;
                                                bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B / ktr;
                                                if (Vi > Vj)
                                                {
                                                    gii = gij * ktr;
                                                    bii = bij * ktr;
                                                }
                                                else
                                                {
                                                    gii = gij / ktr;
                                                    bii = bij / ktr;
                                                }
                                            }
                                        }
                                        delta_j = nodeUList.Find(x => x.Number == node_j).Delta *
                                            Math.PI / 180;
                                        Cij = PowerSystemEquations.FC_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                                        Aij = PowerSystemEquations.FA_branch(gij, bij, delta_i, delta_j);
                                        Bij = PowerSystemEquations.FB_branch(gij, bij, delta_i, delta_j);
                                        Fi += Cij * Math.Sqrt(3) * Vi;
                                        J_Vi += 2 * Vi * (gij + gii) + Vj * Aij;
                                        J_Deltai += (-Vi) * Vj * Bij * Math.PI / 180;
                                        k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                        //dF/dVj
                                        J[m, k] = Vi * Aij;
                                        if (node_j != nodeBase)
                                        {
                                            //dF/dбj
                                            J[m, k + 1] = Vi * Vj * Bij * Math.PI / 180;
                                        }
                                    }
                                    F[m, 0] = Fi - _nodeTMList[y].P;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    //dF/dVi
                                    J[m, k] = J_Vi;
                                    if (node_i != nodeBase)
                                    {
                                        //dF/dбi
                                        J[m, k + 1] = J_Deltai;
                                    }
                                    if (flagVesCoef == true)
                                    {
                                        double yacobi = 0;
                                        for (int i = 0; i < K; i++)
                                        {
                                            yacobi += J[m, i] * J[m, i];
                                        }
                                        C[m, m] = 1 / yacobi;
                                    }
                                    else
                                        C[m, m] = _nodeVesList.Find(x => x.NumberNode == node_i).VesP;
                                    m++;
                                }
                                if (_nodeTMList[y].Q != 0)
                                {
                                    double J_Vi = 0;
                                    double J_Deltai = 0;
                                    double Fi = 0;
                                    delta_i = nodeUList.Find(x => x.Number == node_i).Delta * Math.PI / 180;

                                    List<Branch_TM> branchNode = _branchTMList.Where(x => ((x.Node_kon == node_i)
                                    | (x.Node_nach == node_i))).ToList<Branch_TM>();
                                    for (int j = 0; j < branchNode.Count; j++)
                                    {
                                        if (branchNode[j].Node_nach != node_i)
                                        {
                                            node_j = branchNode[j].Node_nach;
                                            Vi = nodeUList.Find(x => x.Number == node_i).V;
                                            Vj = nodeUList.Find(x => x.Number == node_j).V;
                                            var ktr = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).Ktr;
                                            if (ktr == 1)
                                            {
                                                gij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).G;
                                                bij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).B;
                                                gii = gij;
                                                bii = bij + _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).Bline * 0.000001;
                                            }
                                            else
                                            {
                                                gij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).G / ktr;
                                                bij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).B / ktr;
                                                if (Vi > Vj)
                                                {
                                                    gii = gij * ktr;
                                                    bii = bij * ktr;
                                                }
                                                else
                                                {
                                                    gii = gij / ktr;
                                                    bii = bij / ktr;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            node_j = branchNode[j].Node_kon;
                                            Vi = nodeUList.Find(x => x.Number == node_i).V;
                                            Vj = nodeUList.Find(x => x.Number == node_j).V;
                                            var ktr = _branchList.Find(x => ((x.Node_nach == node_i) &&
                                                (x.Node_kon == node_j))).Ktr;
                                            if (ktr == 1)
                                            {
                                                gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G;
                                                bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B;
                                                gii = gij;
                                                bii = bij + _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).Bline * 0.000001;
                                            }
                                            else
                                            {
                                                gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G / ktr;
                                                bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B / ktr;
                                                if (Vi > Vj)
                                                {
                                                    gii = gij * ktr;
                                                    bii = bij * ktr;
                                                }
                                                else
                                                {
                                                    gii = gij / ktr;
                                                    bii = bij / ktr;
                                                }
                                            }
                                        }
                                        delta_j = nodeUList.Find(x => x.Number == node_j).Delta * Math.PI / 180;
                                        Dij = PowerSystemEquations.FD_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                                        Aij = PowerSystemEquations.FA_branch(gij, bij, delta_i, delta_j);
                                        Bij = PowerSystemEquations.FB_branch(gij, bij, delta_i, delta_j);

                                        Fi += Dij * Math.Sqrt(3) * Vi;

                                        J_Vi += 2 * Vi * (bij + bii) + Vj * Bij;
                                        J_Deltai += Vi * Vj * Aij * Math.PI / 180;
                                        k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                        //dF/dVj
                                        J[m, k] = Vi * Bij;
                                        if (node_j != nodeBase)
                                        {
                                            //dF/dбj
                                            J[m, k + 1] = (-Vi) * Vj * Aij * Math.PI / 180;
                                        }
                                    }
                                    F[m, 0] = Fi - _nodeTMList[y].Q;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    //dF/dVi
                                    J[m, k] = J_Vi;
                                    if (node_i != nodeBase)
                                    {
                                        //dF/dбi
                                        J[m, k + 1] = J_Deltai;
                                    }
                                    if (flagVesCoef == true)
                                    {
                                        double yacobi = 0;
                                        for (int i = 0; i < K; i++)
                                        {
                                            yacobi += J[m, i] * J[m, i];
                                        }
                                        C[m, m] = 1 / yacobi;
                                    }
                                    else
                                        C[m, m] = _nodeVesList.Find(x => x.NumberNode == node_i).VesQ;
                                    m++;
                                }
                            }

                            //Уравнения связей
                            for (int z = 0; z < _branchTMList.Count; z++)
                            {
                                int node_i = _branchTMList[z].Node_nach;
                                int node_j = _branchTMList[z].Node_kon;
                                var numberBranch = _branchList.Find(x => (x.Node_nach == node_i)
                                    && (x.Node_kon == node_j)).NumberBranch;
                                var ktr = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).Ktr;
                                Vi = nodeUList.Find(x => x.Number == node_i).V;
                                Vj = nodeUList.Find(x => x.Number == node_j).V;
                                delta_i = nodeUList.Find(x => x.Number == node_i).Delta * Math.PI / 180;
                                delta_j = nodeUList.Find(x => x.Number == node_j).Delta * Math.PI / 180;
                                if (ktr == 1)
                                {
                                    gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G;
                                    bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B;
                                    gii = gij;
                                    bii = bij + _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).Bline * 0.000001;
                                }
                                else
                                {
                                    gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G / ktr;
                                    bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B / ktr;
                                    if (Vi > Vj)
                                    {
                                        gii = gij * ktr;
                                        bii = bij * ktr;
                                    }
                                    else
                                    {
                                        gii = gij / ktr;
                                        bii = bij / ktr;
                                    }
                                }
                                Cij = PowerSystemEquations.FC_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                                Dij = PowerSystemEquations.FD_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                                Aij = PowerSystemEquations.FA_branch(gij, bij, delta_i, delta_j);
                                Bij = PowerSystemEquations.FB_branch(gij, bij, delta_i, delta_j);

                                if (_branchTMList[z].P_nach != 0)
                                {
                                    double cii = 0;
                                    F[m, 0] = Cij * Vi * Math.Sqrt(3) - _branchTMList[z].P_nach;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k] = 2 * Vi * (gii + gij) + Vj * Aij;
                                    cii += J[m, k] * J[m, k];
                                    if (node_i != nodeBase)
                                    {
                                        J[m, k + 1] = (-Vi) * Vj * Bij * Math.PI / 180;
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                    J[m, k] = Vi * Aij;
                                    cii += J[m, k] * J[m, k];
                                    if (node_j != nodeBase)
                                    {
                                        J[m, k + 1] = Vi * Vj * Bij * Math.PI / 180;
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    if (flagVesCoef == true)
                                        C[m, m] = 1 / cii;
                                    else
                                        C[m, m] = _branchVesList.Find(x => x.NumberBranch == numberBranch).VesP;
                                    m++;
                                }

                                if (_branchTMList[z].Q_nach != 0)
                                {
                                    double cii = 0;
                                    F[m, 0] = Dij * Math.Sqrt(3) * Vi - _branchTMList[z].Q_nach;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k] = 2 * Vi * (bii + bij) + Vj * Bij;
                                    cii += J[m, k] * J[m, k];
                                    if (node_i != nodeBase)
                                    {
                                        J[m, k + 1] = Vi * Vj * Aij * Math.PI / 180;
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                    J[m, k] = Vi * Bij;
                                    cii += J[m, k] * J[m, k];
                                    if (node_j != nodeBase)
                                    {
                                        J[m, k + 1] = (-Vi) * Vj * Aij * Math.PI / 180;
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    if (flagVesCoef == true)
                                        C[m, m] = 1 / cii;
                                    else
                                        C[m, m] = _branchVesList.Find(x => x.NumberBranch == numberBranch).VesQ;
                                    m++;
                                }
                                if (_branchTMList[z].I_nach != 0)
                                {
                                    double cii = 0;
                                    F[m, 0] = Math.Sqrt(Cij * Cij + Dij * Dij) - _branchTMList[z].I_nach;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k] = (Math.Pow(Cij * Cij + Dij * Dij, (-0.5)) / Math.Sqrt(3)) * (Cij * (gij + gii) + Dij * (bij + bii));
                                    cii += J[m, k] * J[m, k];
                                    if (node_i != nodeBase)
                                    {
                                        J[m, k + 1] = ((Vj * Math.Pow(Cij * Cij + Dij * Dij, (-0.5))) / Math.Sqrt(3)) *
                                            (Math.PI / 180) * (Dij * Aij - Cij * Bij);
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                    J[m, k] = (Math.Pow(Cij * Cij + Dij * Dij, (-0.5)) / Math.Sqrt(3)) * (Cij * Aij + Dij * Bij);
                                    cii += J[m, k] * J[m, k];
                                    if (node_j != nodeBase)
                                    {
                                        J[m, k + 1] = ((Vj * Math.Pow(Cij * Cij + Dij * Dij, (-0.5))) / Math.Sqrt(3)) *
                                            (Math.PI / 180) * (Cij * Bij - Dij * Aij);
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    if (flagVesCoef == true)
                                        C[m, m] = 1 / cii;
                                    else
                                        C[m, m] = _branchVesList.Find(x => x.NumberBranch == numberBranch).VesI;
                                    m++;
                                }
                                if (_branchTMList[z].Sigma_nach != 0)
                                {
                                    double cii = 0;

                                    F[m, 0] = Math.Atan(Dij / Cij) / (Math.PI / 180) - _branchTMList[z].Sigma_nach;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k] = ((bij + bii) * Cij - (gij + gii) * Dij) / (Math.Sqrt(3) * (Cij * Cij + Dij * Dij) * (Math.PI / 180));
                                    cii += J[m, k] * J[m, k];
                                    if (node_i != nodeBase)
                                    {
                                        J[m, k + 1] = (Vj * (Aij * Cij + Bij * Dij) / (Math.Sqrt(3) * (Cij * Cij + Dij * Dij)));
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                    J[m, k] = (Bij * Cij - Aij * Dij) / (Math.Sqrt(3) * (Cij * Cij + Dij * Dij) * (Math.PI / 180));
                                    cii += J[m, k] * J[m, k];
                                    if (node_j != nodeBase)
                                    {
                                        J[m, k + 1] = (-Vj * (Aij * Cij + Bij * Dij) / (Math.Sqrt(3) * (Cij * Cij + Dij * Dij)));
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    if (flagVesCoef == true)
                                        C[m, m] = 1 / cii;
                                    else
                                        C[m, m] = _branchVesList.Find(x => x.NumberBranch == numberBranch).VesSigma;
                                    m++;
                                }

                                Vi = nodeUList.Find(x => x.Number == node_j).V;
                                Vj = nodeUList.Find(x => x.Number == node_i).V;
                                delta_i = nodeUList.Find(x => x.Number == node_j).Delta * Math.PI / 180;
                                delta_j = nodeUList.Find(x => x.Number == node_i).Delta * Math.PI / 180;
                                if (ktr != 1)
                                {
                                    if (Vi > Vj)
                                    {
                                        gii = gij * ktr;
                                        bii = bij * ktr;
                                    }
                                    else
                                    {
                                        gii = gij / ktr;
                                        bii = bij / ktr;
                                    }
                                }
                                Cij = PowerSystemEquations.FC_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                                Dij = PowerSystemEquations.FD_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                                Aij = PowerSystemEquations.FA_branch(gij, bij, delta_i, delta_j);
                                Bij = PowerSystemEquations.FB_branch(gij, bij, delta_i, delta_j);
                                if (_branchTMList[z].P_kon != 0)
                                {
                                    double cii = 0;
                                    F[m, 0] = Cij * Vi * Math.Sqrt(3) - _branchTMList[z].P_kon;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    //поменяла формулы местами
                                    J[m, k] = Vi * Aij;
                                    cii += J[m, k] * J[m, k];
                                    if (node_i != nodeBase)
                                    {
                                        //Изменила тут -
                                        J[m, k + 1] = (Vi) * Vj * Bij * Math.PI / 180;
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                    J[m, k] = 2 * Vi * (gii + gij) + Vj * Aij; ;
                                    cii += J[m, k] * J[m, k];
                                    if (node_j != nodeBase)
                                    {
                                        J[m, k + 1] = Vi * Vj * Bij * Math.PI / 180;
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    if (flagVesCoef == true)
                                        C[m, m] = 1 / cii;
                                    else
                                        C[m, m] = _branchVesList.Find(x => x.NumberBranch == numberBranch).VesP;
                                    m++;
                                }
                                if (_branchTMList[z].Q_kon != 0)
                                {
                                    double cii = 0;
                                    F[m, 0] = Dij * Math.Sqrt(3) * Vi - _branchTMList[z].Q_kon;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k] = Vi * Bij;
                                    cii += J[m, k] * J[m, k];
                                    if (node_i != nodeBase)
                                    {
                                        J[m, k + 1] = -Vi * Vj * Aij * Math.PI / 180;
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                    J[m, k] = 2 * Vi * (bii + bij) + Vj * Bij;
                                    cii += J[m, k] * J[m, k];
                                    if (node_j != nodeBase)
                                    {
                                        J[m, k + 1] = (Vi) * Vj * Aij * Math.PI / 180;
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    if (flagVesCoef == true)
                                        C[m, m] = 1 / cii;
                                    else
                                        C[m, m] = _branchVesList.Find(x => x.NumberBranch == numberBranch).VesQ;
                                    m++;

                                }
                                if (_branchTMList[z].I_kon != 0)
                                {
                                    //TODO: ДЕЛЕНИЕ НА 0 при плоском старте
                                    double cii = 0;
                                    F[m, 0] = Math.Sqrt(Cij * Cij + Dij * Dij) - _branchTMList[z].I_kon;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k] = (Math.Pow(Cij * Cij + Dij * Dij, (-0.5)) / Math.Sqrt(3)) * (Cij * Aij + Dij * Bij);
                                    cii += J[m, k] * J[m, k];
                                    if (node_i != nodeBase)
                                    {
                                        J[m, k + 1] = ((Vj * Math.Pow(Cij * Cij + Dij * Dij, (-0.5))) / Math.Sqrt(3)) *
                                            (Math.PI / 180) * (Cij * Bij - Dij * Aij);
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                    J[m, k] = (Math.Pow(Cij * Cij + Dij * Dij, (-0.5)) / Math.Sqrt(3)) * (Cij * (gij + gii) + Dij * (bij + bii));
                                    cii += J[m, k] * J[m, k];
                                    if (node_j != nodeBase)
                                    {
                                        J[m, k + 1] = ((Vj * Math.Pow(Cij * Cij + Dij * Dij, (-0.5))) / Math.Sqrt(3)) *
                                            (Math.PI / 180) * (Dij * Aij - Cij * Bij);
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    if (flagVesCoef == true)
                                        C[m, m] = 1 / cii;
                                    else
                                        C[m, m] = _branchVesList.Find(x => x.NumberBranch == numberBranch).VesI;
                                    m++;
                                }
                                if (_branchTMList[z].Sigma_kon != 0)
                                {
                                    double cii = 0;
                                    F[m, 0] = Math.Atan(Dij / Cij) / (Math.PI / 180) - _branchTMList[z].Sigma_kon;
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_i);
                                    J[m, k] = (Bij * Cij - Aij * Dij) / (Math.Sqrt(3) * (Cij * Cij + Dij * Dij) * (Math.PI / 180));
                                    cii += J[m, k] * J[m, k];
                                    if (node_i != nodeBase)
                                    {
                                        J[m, k + 1] = (-Vj * (Aij * Cij + Bij * Dij) / (Math.Sqrt(3) * (Cij * Cij + Dij * Dij)));
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    k = 2 * nodeUList.FindIndex(x => x.Number == node_j);
                                    J[m, k] = ((bij + bii) * Cij - (gij + gii) * Dij) / (Math.Sqrt(3) * (Cij * Cij + Dij * Dij) * (Math.PI / 180));
                                    cii += J[m, k] * J[m, k];
                                    if (node_j != nodeBase)
                                    {
                                        J[m, k + 1] = (Vj * (Aij * Cij + Bij * Dij) / (Math.Sqrt(3) * (Cij * Cij + Dij * Dij)));
                                        cii += J[m, k + 1] * J[m, k + 1];
                                    }
                                    if (flagVesCoef == true)
                                        C[m, m] = 1 / cii;
                                    else
                                        C[m, m] = _branchVesList.Find(x => x.NumberBranch == numberBranch).VesSigma;
                                    m++;
                                }
                            }
                            //Основной рачет
                            var maxF = Matrix.MaxElement(F);
                            Matrix Ft = Matrix.Transpose(F);
                            var f = Matrix.Multiply(0.5, Ft) * C * F;
                            Matrix H = Matrix.Transpose(J) * C * J;
                            Matrix grad = Matrix.Transpose(J) * C * F;
                            Matrix deltaU = H.Invert() * (-grad);
                            _error = Matrix.MaxElement(deltaU);
                            U = U + Matrix.Multiply(1, deltaU);
                            Result res = new Result(f, _error, U);
                            resultList.Add(res);
                            eventLog.Text += Environment.NewLine + "Итерация №" + nomerIterac.ToString()
                                + Environment.NewLine + "Целевая функция F=" + f[0, 0].ToString("0.000") +
                                 Environment.NewLine + "Погрешность e =" + _error.ToString("0.000");
                            nodeUList = VectorSostoyaniya.ObnovVector(nodeUList, U, nodeBase);
                            nomerIterac++;
                            targerFunction = f[0, 0].ToString("0.000");
                            if ((_error < maxError) && (f[0, 0] < 3))
                            {
                                targerFunction = f[0, 0].ToString("0.000");
                            stopWatch.Stop();
                            // Get the elapsed time as a TimeSpan value.
                            TimeSpan ts = stopWatch.Elapsed;

                            // Format and display the TimeSpan value.
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts.Hours, ts.Minutes, ts.Seconds,
                                ts.Milliseconds / 10);
                            eventLog.Text += Environment.NewLine + "Расчёт закончен за"+ elapsedTime;

                                break;
                            }

                            if (nomerIterac > _nomerIterMax)
                            {
                                eventLog.Text += Environment.NewLine + "Превышен предел итераций!";
                                break;
                            }

                        }
                        while (nomerIterac < _nomerIterMax);
                        Matrix Uotvet = new Matrix(K, 1);
                        Uotvet = Result.FindMinE(resultList);
                        nodeUList = VectorSostoyaniya.ObnovVector(nodeUList, Uotvet, nodeBase);

                        //Формируем выходные данные				
                        List<Node_TM> nodeOCList = new List<Node_TM>();
                        List<Branch_TM> branchOCList = new List<Branch_TM>();
                        //Заполняем параметры узла
                        for (int y = 0; y < _nodeList.Count; y++)
                        {
                            Node_TM nodeOC = new Node_TM();
                            double Vi;
                            double Vj;
                            double gij;
                            double bij;
                            int index_i;
                            int index_j;
                            double gii;
                            double bii;
                            double delta_i;
                            double delta_j;
                            double Cij;
                            double Dij;
                            double ktr;
                            double Fp = 0;
                            double Fq = 0;
                            nodeOC.Sostoyanie = false;

                            int node_i = _nodeList[y].NumberNode;
                            int node_j;

                            if (_nodeTMList.Any(x => x.NumberNode == node_i))
                            {
                                nodeOC.Sostoyanie = true;
                                index_i = _nodeList.FindIndex(x => x.NumberNode == node_i);
                                delta_i = nodeUList.Find(x => x.Number == node_i).Delta * Math.PI / 180;
                                List<Branch_TM> branchNode = _branchTMList.Where(x => ((x.Node_kon == node_i)
                                | (x.Node_nach == node_i))).ToList<Branch_TM>();
                                for (int j = 0; j < branchNode.Count; j++)
                                {
                                    if (branchNode[j].Node_nach != node_i)
                                    {
                                        node_j = branchNode[j].Node_nach;
                                        delta_j = nodeUList.Find(x => x.Number == node_j).Delta * Math.PI / 180;
                                        index_j = _nodeList.FindIndex(x => x.NumberNode == node_j);
                                        ktr = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).Ktr;
                                        Vi = nodeUList.Find(x => x.Number == node_i).V;
                                        Vj = nodeUList.Find(x => x.Number == node_j).V;
                                        if (ktr == 1)
                                        {
                                            gij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).G;
                                            bij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).B;
                                            gii = gij;
                                            bii = bij + _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).Bline * 0.000001;
                                        }
                                        else
                                        {
                                            gij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).G / ktr;
                                            bij = _branchList.Find(x => ((x.Node_nach == node_j) && (x.Node_kon == node_i))).B / ktr;
                                            if (Vi > Vj)
                                            {
                                                gii = gij * ktr;
                                                bii = bij * ktr;
                                            }
                                            else
                                            {
                                                gii = gij / ktr;
                                                bii = bij / ktr;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        node_j = branchNode[j].Node_kon;
                                        delta_j = nodeUList.Find(x => x.Number == node_j).Delta * Math.PI / 180;
                                        index_j = _nodeList.FindIndex(x => x.NumberNode == node_j);
                                        ktr = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).Ktr;
                                        Vi = nodeUList.Find(x => x.Number == node_i).V;
                                        Vj = nodeUList.Find(x => x.Number == node_j).V;
                                        if (ktr == 1)
                                        {
                                            gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G;
                                            bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B;
                                            gii = gij;
                                            bii = bij + _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).Bline * 0.000001;
                                        }
                                        else
                                        {
                                            gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G / ktr;
                                            bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B / ktr;
                                            if (Vi > Vj)
                                            {
                                                gii = gij * ktr;
                                                bii = bij * ktr;
                                            }
                                            else
                                            {
                                                gii = gij / ktr;
                                                bii = bij / ktr;
                                            }

                                        }
                                    }
                                    Cij = PowerSystemEquations.FC_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                                    Dij = PowerSystemEquations.FD_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                                    Fp += Cij * Math.Sqrt(3) * Vi;
                                    Fq += Dij * Math.Sqrt(3) * Vi;
                                }
                            }
                            nodeOC.NumberNode = node_i;
                            nodeOC.U = nodeUList.Find(x => x.Number == node_i).V;
                            nodeOC.Delta = nodeUList.Find(x => x.Number == node_i).Delta;
                            nodeOC.P = Fp;
                            nodeOC.Q = Fq;
                            nodeOCList.Add(nodeOC);
                        }

                        //Заполняем параметры ветвей
                        for (int z = 0; z < _branchList.Count; z++)
                        {
                            Branch_TM branchOC = new Branch_TM();
                            double Vi;
                            double Vj;
                            double gij;
                            double bij;
                            double gii;
                            double bii;
                            int index_i;
                            int index_j;
                            double delta_i;
                            double delta_j;
                            double Cij;
                            double Dij;
                            branchOC.Sostoyanie = false;

                            int node_i = _branchTMList[z].Node_nach;
                            int node_j = _branchTMList[z].Node_kon;
                            branchOC.Node_nach = node_i;
                            branchOC.Node_kon = node_j;
                            ////index_i = _nodeList.FindIndex(x => x.NumberNode == node_i);
                            ////index_j = _nodeList.FindIndex(x => x.NumberNode == node_j);
                            var ktr = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).Ktr;
                            //gij = G[index_i, index_j];
                            //bij = B[index_i, index_j];
                            Vi = nodeUList.Find(x => x.Number == node_i).V;
                            Vj = nodeUList.Find(x => x.Number == node_j).V;
                            if (ktr == 1)
                            {
                                gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G;
                                bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B;
                                gii = gij;
                                bii = bij + _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).Bline * 0.000001;
                            }
                            else
                            {
                                gij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).G / ktr;
                                bij = _branchList.Find(x => ((x.Node_nach == node_i) && (x.Node_kon == node_j))).B / ktr;
                                if (Vi > Vj)
                                {
                                    gii = gij * ktr;
                                    bii = bij * ktr;
                                }
                                else
                                {
                                    gii = gij / ktr;
                                    bii = bij / ktr;
                                }
                            }
                            delta_i = nodeUList.Find(x => x.Number == node_i).Delta * Math.PI / 180;
                            delta_j = nodeUList.Find(x => x.Number == node_j).Delta * Math.PI / 180;
                            Cij = PowerSystemEquations.FC_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                            Dij = PowerSystemEquations.FD_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);

                            branchOC.P_nach = Cij * Vi * Math.Sqrt(3);
                            branchOC.Q_nach = Dij * Math.Sqrt(3) * Vi;
                            branchOC.I_nach = Math.Sqrt(Cij * Cij + Dij * Dij);
                            branchOC.Sigma_nach = Math.Atan(Dij / Cij) / (Math.PI / 180);

                            Vi = nodeUList.Find(x => x.Number == node_j).V;
                            Vj = nodeUList.Find(x => x.Number == node_i).V;
                            delta_i = nodeUList.Find(x => x.Number == node_j).Delta * Math.PI / 180;
                            delta_j = nodeUList.Find(x => x.Number == node_i).Delta * Math.PI / 180;
                            if (ktr != 1)
                            {
                                if (Vi > Vj)
                                {
                                    gii = gij * ktr;
                                    bii = bij * ktr;
                                }
                                else
                                {
                                    gii = gij / ktr;
                                    bii = bij / ktr;
                                }
                            }
                            Cij = PowerSystemEquations.FC_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                            Dij = PowerSystemEquations.FD_branch(Vi, Vj, gii, bii, gij, bij, delta_i, delta_j);
                            branchOC.P_kon = Cij * Vi * Math.Sqrt(3);
                            branchOC.Q_kon = Dij * Math.Sqrt(3) * Vi;
                            branchOC.I_kon = Math.Sqrt(Cij * Cij + Dij * Dij);
                            branchOC.Sigma_kon = Math.Atan(Dij / Cij) / (Math.PI / 180);
                            branchOCList.Add(branchOC);
                        }
                        _nodeOCList = nodeOCList;
                        _branchOCList = branchOCList;
						infoNode += ("k=" + ";" + $"{nomerIterac}" + ";"
							+ "errorNode" + ";" + $"{test}" + ";"
							+ "targerFunction" + ";" + targerFunction.Replace(",", ".") + ";"
							+ "error" + ";" + _error.ToString().Replace(",", ".") + ";" + "\n"
							+ Node_TM.Information(nodeOCList) + "\n");
						infoBranch += ("k=" + ";" + $"{nomerIterac}" + ";"
							+ "errorNode" + ";" + $"{test}" + ";"
							+ "targerFunction" + ";" + targerFunction.Replace(",", ".") + ";"
							+ "error" + ";" + _error.ToString().Replace(",", ".") + ";" + "\n"
							+ Branch_TM.Information(branchOCList) + "\n");
					}
                }
                catch (System.NullReferenceException)
                {
                    eventLog.Text += Environment.NewLine + "Введены не все данные!";
                }

			//}
			File.WriteAllText(filenameTM + @"\Node_OC_" + _nodeError + ".csv", infoNode.Replace(",", "."));
			File.WriteAllText(filenameTM + @"\Branch_OC_" + _nodeError + ".csv", infoBranch.Replace(",", "."));
			eventLog.Text += Environment.NewLine + "ОС сохранен!";
		}

        /// <summary>
        /// Загрузить ТМ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void загрузитьТМToolStripMenuItem_Click(object sender, EventArgs e)

		{
			try
			{
				FolderBrowserDialog folder = new FolderBrowserDialog { };
				//var filename = String.Empty;
				//folder.SelectedPath = Directory.GetCurrentDirectory();
				folder.SelectedPath = filenameTM;
				if (folder.ShowDialog() == DialogResult.OK)
				{
					_nodeTMList = Node_TM.ReadFileNode(folder.SelectedPath + @"\Node_TM.csv");
                    _branchTMList = Branch_TM.ReadFileBranch(folder.SelectedPath + @"\Branch_TM.csv");
                    //Для учета сетевого узла
                    /*for (int i=0; i<_nodeTMList.Count;i++)
					{
						if (_nodeTMList[i].NumberNode == _nodeList.Find(z => z.Type == 3).NumberNode)
						{
							_nodeTMList[i].P = 0.000001;
							_nodeTMList[i].Q = 0.000001;
						}
					}*/
                    eventLog.Text += Environment.NewLine + "ТМ загружена!";
				}
			}
			catch (FileNotFoundException)
			{
				eventLog.Text += Environment.NewLine + "Файл не найден!";
			}
		}

		/// <summary>
		/// Загрузит Базовый режим
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void загрузитьБРToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog folder = new FolderBrowserDialog { };
				//TODO: Переключение на выбор директории
				//var filename = String.Empty;
				//folder.SelectedPath = Directory.GetCurrentDirectory();
				folder.SelectedPath = filename;
				if (folder.ShowDialog() == DialogResult.OK)
				{
					List<Node_TM> node_TMs = new List<Node_TM>();
					node_TMs = Node_TM.ReadFileNode(folder.SelectedPath + @"\Node_BR.csv");
					List<NodeVes> ves = new List<NodeVes>();
					if (_nodeVesList is null)
					{
						for (int i=0; i<node_TMs.Count; i++)
						{
							NodeVes nodeVes = new NodeVes();
							nodeVes.NumberNode = node_TMs[i].NumberNode;
							nodeVes.Ubr = node_TMs[i].U;
							nodeVes.Delta= node_TMs[i].Delta;
							nodeVes.Ppi = node_TMs[i].P;
							nodeVes.Qpi = node_TMs[i].Q;
							ves.Add(nodeVes);
						}
						_nodeVesList = ves;
					}
					else
					{
						for (int i = 0; i < _nodeVesList.Count; i++)
						{
							_nodeVesList[i].Ubr = node_TMs.Find(x => x.NumberNode == _nodeVesList[i].NumberNode).U;
							_nodeVesList[i].Delta = node_TMs.Find(x => x.NumberNode == _nodeVesList[i].NumberNode).Delta;
							_nodeVesList[i].Ppi  = node_TMs.Find(x => x.NumberNode == _nodeVesList[i].NumberNode).P;
							_nodeVesList[i].Qpi = node_TMs.Find(x => x.NumberNode == _nodeVesList[i].NumberNode).Q;
						}
					}

					eventLog.Text += Environment.NewLine + "БР загружен!";
				}
			}
			catch (FileNotFoundException)
			{
				eventLog.Text += Environment.NewLine + "Файл не найден!";
			}
		}

		/// <summary>
		/// Сохранение ОС в файл
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void оСToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_nodeOCList.Count <1)
				eventLog.Text += Environment.NewLine + "ОС не рассчитан!";
			else
			{
				FolderBrowserDialog folder = new FolderBrowserDialog { };
				folder.SelectedPath = filenameTM;
				if (folder.ShowDialog() == DialogResult.OK)
				{
					Node_TM.SaveFile(_nodeOCList, folder.SelectedPath + @"\Node_OC_"+nomerIterac+ ".csv");
					Branch_TM.SaveFile(_branchOCList, folder.SelectedPath + @"\Branch_OC_"+nomerIterac+".csv");
					eventLog.Text += Environment.NewLine + "ОС сохранен!";
				}
			}
		}

		/// <summary>
		/// Сохранить ТМ в файл
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void тМToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_nodeTMList is null)
				eventLog.Text += Environment.NewLine + "ТМ нет!";
			else
			{		
				FolderBrowserDialog folder = new FolderBrowserDialog { };
				folder.SelectedPath = filename;
				if (folder.ShowDialog() == DialogResult.OK)
				{
					Node_TM.SaveFile(_nodeTMList, folder.SelectedPath + @"\Node_TM.csv");
					Branch_TM.SaveFile(_branchTMList, folder.SelectedPath + @"\Branch_TM.csv");
					eventLog.Text += Environment.NewLine + "Набор ТМ сохранен!";
				}			
			}
		}

		/// <summary>
		/// Сохранить параметры
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void парамерыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_nodeList is null)
				eventLog.Text += Environment.NewLine + "Параметров нет!";
			else
			{
				FolderBrowserDialog folder = new FolderBrowserDialog { };
				folder.SelectedPath = filename;
				if (folder.ShowDialog() == DialogResult.OK)
				{
					Node.SaveFile(_nodeList, folder.SelectedPath + @"\Node.csv");
					Branch.SaveFile(_branchList, folder.SelectedPath + @"\Branch_.csv");
					eventLog.Text += Environment.NewLine + "Параметры сохранены!";
				}
			}
		}

		private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			 this.Close();
		}
		/// <summary>
		/// Формирование набора ТМ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void моделированиеТМToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_branchTMList = Branch_TM.GetBranch_TM(_branchTMList);
			_nodeTMList = Node_TM.GetNode_TM(_nodeTMList);
			eventLog.Text += Environment.NewLine + "Набор ТМ сформирован!";
		}

		private void StateEstimationForm_Load(object sender, EventArgs e)
		{

		}

		private void параметрыНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SettingsSE newForm = new SettingsSE(flagVesCoef, maxError, _nomerIterMax);
			if (newForm.ShowDialog() == DialogResult.OK)
			{
				flagVesCoef = newForm.FlagVesCoef;
				maxError = newForm.MaxError;
				_nomerIterMax = newForm.NomerIterMax;
                _nodeError = newForm.NodeError;
                eventLog.Text += Environment.NewLine + "Узел с погрешностью:"+_nodeError;
            }
		}

		private void button1_Click(object sender, EventArgs e)
		{
            eventLog.Text = "";

        }
	}
}
