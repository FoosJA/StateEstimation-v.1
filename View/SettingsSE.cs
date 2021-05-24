using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace View
{
	public partial class SettingsSE : Form
	{
		private bool _flagVesCoef;
		/// <summary>
		/// Способ расчёта весовых коэффициентов
		/// </summary>
		public bool FlagVesCoef
		{
			get
			{
				return _flagVesCoef;
			}
		}
		private double _maxError;
		/// <summary>
		/// Максимальная ошибка
		/// </summary>
		public double MaxError
		{
			get
			{
				return _maxError;
			}
			set
			{
				if (value <= 0)
				{
					MessageBox.Show("Ошибка не может быть меньше 0!");									
				}
				_maxError = value;
			}
		}
		private int _nomerIterMax;
		/// <summary>
		/// Максмальное число итераций
		/// </summary>
		public int NomerIterMax
		{
			get
			{
				return _nomerIterMax;
			}
			set
			{
				if (value <= 0)
				{
					//TODO: когда-нибудь доделать
					MessageBox.Show("Номер не может быть меньше 0!");
				}
				_nomerIterMax = value;
			}
		}
        private int _nodeError;
        /// <summary>
        /// Максмальное число итераций
        /// </summary>
        public int NodeError
        {
            get
            {
                return _nodeError;
            }
            set
            {
                _nodeError = value;
            }
        }
        public SettingsSE()
		{
			InitializeComponent();
			Shown += SettingsSEForm_Shown;
		}
		public SettingsSE(bool flagVesCoef, double maxError, int nomerIterMax)
		{
			InitializeComponent();
			_flagVesCoef = flagVesCoef;
			_maxError = maxError;
			_nomerIterMax = nomerIterMax;
			Shown += SettingsSEForm_Shown;
			
		}
		private void SettingsSEForm_Shown(object sender, EventArgs e)
		{
			comboBox1.Items.Add("Ручной");			
			comboBox1.Items.Add("Автоматический");
			if (_flagVesCoef == true)
				comboBox1.Text="Ручной";
			else
				comboBox1.Text="Автоматический";
			textBox1.Text = _nomerIterMax.ToString();
			textBox2.Text = _maxError.ToString();
            textBox3.Text = _nodeError.ToString();
        }

		private void SettingsSE_Load(object sender, EventArgs e)
		{

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{			
			if (comboBox1.Text == "Ручной")
			{
				_flagVesCoef = true;
			}
			if (comboBox1.Text == "Автоматический")
			{
				_flagVesCoef = false;
			}
		}

		private void button2_Click_1(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				_nomerIterMax = Convert.ToInt32(textBox1.Text);
				_maxError = Convert.ToDouble(textBox2.Text);
                _nodeError= Convert.ToInt32(textBox3.Text);
                this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (ArgumentException)
			{
				MessageBox.Show("Не корректные данные!");
			}
		}

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
