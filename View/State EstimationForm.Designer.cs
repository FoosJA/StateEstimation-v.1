namespace View
{
	partial class StateEstimationForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateEstimationForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.загрузитьБРToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.парамерыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.тМToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.бРToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.оСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.выходToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.параметрыНастройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.моделированиеТМToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.eventLog = new System.Windows.Forms.TextBox();
			this.buttonSE = new System.Windows.Forms.Button();
			this.listTable = new System.Windows.Forms.ListBox();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// файлToolStripMenuItem
			// 
			this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.загрузитьБРToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.выходToolStripMenuItem1});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			// 
			// загрузитьToolStripMenuItem
			// 
			this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
			this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.загрузитьToolStripMenuItem.Text = "Загрузить параметры";
			this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьпараметрыToolStripMenuItem_Click);
			// 
			// сохранитьToolStripMenuItem
			// 
			this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
			this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.сохранитьToolStripMenuItem.Text = "Загрузить ТМ";
			this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьТМToolStripMenuItem_Click);
			// 
			// загрузитьБРToolStripMenuItem
			// 
			this.загрузитьБРToolStripMenuItem.Name = "загрузитьБРToolStripMenuItem";
			this.загрузитьБРToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.загрузитьБРToolStripMenuItem.Text = "Загрузить БР";
			this.загрузитьБРToolStripMenuItem.Click += new System.EventHandler(this.загрузитьБРToolStripMenuItem_Click);
			// 
			// сохранитьКакToolStripMenuItem
			// 
			this.сохранитьКакToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.парамерыToolStripMenuItem,
            this.тМToolStripMenuItem,
            this.бРToolStripMenuItem,
            this.оСToolStripMenuItem});
			this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
			this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.сохранитьКакToolStripMenuItem.Text = "Сохранить";
			// 
			// парамерыToolStripMenuItem
			// 
			this.парамерыToolStripMenuItem.Name = "парамерыToolStripMenuItem";
			this.парамерыToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.парамерыToolStripMenuItem.Text = "Парамеры";
			this.парамерыToolStripMenuItem.Click += new System.EventHandler(this.парамерыToolStripMenuItem_Click);
			// 
			// тМToolStripMenuItem
			// 
			this.тМToolStripMenuItem.Name = "тМToolStripMenuItem";
			this.тМToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.тМToolStripMenuItem.Text = "ТМ";
			this.тМToolStripMenuItem.Click += new System.EventHandler(this.тМToolStripMenuItem_Click);
			// 
			// бРToolStripMenuItem
			// 
			this.бРToolStripMenuItem.Name = "бРToolStripMenuItem";
			this.бРToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.бРToolStripMenuItem.Text = "БР";
			// 
			// оСToolStripMenuItem
			// 
			this.оСToolStripMenuItem.Name = "оСToolStripMenuItem";
			this.оСToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.оСToolStripMenuItem.Text = "ОС";
			this.оСToolStripMenuItem.Click += new System.EventHandler(this.оСToolStripMenuItem_Click);
			// 
			// выходToolStripMenuItem1
			// 
			this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
			this.выходToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
			this.выходToolStripMenuItem1.Text = "Выход";
			this.выходToolStripMenuItem1.Click += new System.EventHandler(this.выходToolStripMenuItem1_Click);
			// 
			// настройкиToolStripMenuItem
			// 
			this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.параметрыНастройкиToolStripMenuItem,
            this.моделированиеТМToolStripMenuItem});
			this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
			this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
			this.настройкиToolStripMenuItem.Text = "Настройки";
			// 
			// параметрыНастройкиToolStripMenuItem
			// 
			this.параметрыНастройкиToolStripMenuItem.Name = "параметрыНастройкиToolStripMenuItem";
			this.параметрыНастройкиToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
			this.параметрыНастройкиToolStripMenuItem.Text = "Параметры настройки";
			this.параметрыНастройкиToolStripMenuItem.Click += new System.EventHandler(this.параметрыНастройкиToolStripMenuItem_Click);
			// 
			// моделированиеТМToolStripMenuItem
			// 
			this.моделированиеТМToolStripMenuItem.Name = "моделированиеТМToolStripMenuItem";
			this.моделированиеТМToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
			this.моделированиеТМToolStripMenuItem.Text = "Моделирование ТМ";
			this.моделированиеТМToolStripMenuItem.Click += new System.EventHandler(this.моделированиеТМToolStripMenuItem_Click);
			// 
			// оПрограммеToolStripMenuItem
			// 
			this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
			this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
			this.оПрограммеToolStripMenuItem.Text = "О программе";
			// 
			// splitContainer
			// 
			this.splitContainer.BackColor = System.Drawing.Color.Silver;
			this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer.IsSplitterFixed = true;
			this.splitContainer.Location = new System.Drawing.Point(0, 24);
			this.splitContainer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.BackColor = System.Drawing.Color.White;
			this.splitContainer.Panel1.Controls.Add(this.button1);
			this.splitContainer.Panel1.Controls.Add(this.eventLog);
			this.splitContainer.Panel1.Controls.Add(this.buttonSE);
			this.splitContainer.Panel1.Controls.Add(this.listTable);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.dataGridView);
			this.splitContainer.Size = new System.Drawing.Size(800, 426);
			this.splitContainer.SplitterDistance = 227;
			this.splitContainer.TabIndex = 1;
			this.splitContainer.TabStop = false;
			// 
			// eventLog
			// 
			this.eventLog.Location = new System.Drawing.Point(7, 193);
			this.eventLog.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eventLog.Multiline = true;
			this.eventLog.Name = "eventLog";
			this.eventLog.ReadOnly = true;
			this.eventLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.eventLog.Size = new System.Drawing.Size(206, 174);
			this.eventLog.TabIndex = 2;
			// 
			// buttonSE
			// 
			this.buttonSE.BackColor = System.Drawing.SystemColors.MenuHighlight;
			this.buttonSE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.buttonSE.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.buttonSE.FlatAppearance.BorderSize = 3;
			this.buttonSE.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonSE.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonSE.ForeColor = System.Drawing.Color.DarkRed;
			this.buttonSE.Location = new System.Drawing.Point(38, 154);
			this.buttonSE.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.buttonSE.Name = "buttonSE";
			this.buttonSE.Size = new System.Drawing.Size(130, 33);
			this.buttonSE.TabIndex = 0;
			this.buttonSE.Text = "Расчёт ОС";
			this.buttonSE.UseVisualStyleBackColor = false;
			this.buttonSE.Click += new System.EventHandler(this.buttonSE_Click);
			// 
			// listTable
			// 
			this.listTable.BackColor = System.Drawing.Color.White;
			this.listTable.Dock = System.Windows.Forms.DockStyle.Top;
			this.listTable.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.listTable.FormattingEnabled = true;
			this.listTable.ItemHeight = 18;
			this.listTable.Items.AddRange(new object[] {
            "Параметры Узлы",
            "Параметры Ветви",
            "Вес. коэф. Узлы",
            "Вес. коэф. Ветви",
            "ТМ Узлы",
            "ТМ Ветви",
            "ОС Узлы",
            "ОС Ветви"});
			this.listTable.Location = new System.Drawing.Point(0, 0);
			this.listTable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.listTable.Name = "listTable";
			this.listTable.Size = new System.Drawing.Size(225, 148);
			this.listTable.TabIndex = 1;
			// 
			// dataGridView
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.NullValue = "-";
			this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.dataGridView.BackgroundColor = System.Drawing.Color.White;
			this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.NullValue = "-";
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.ShowCellErrors = false;
			this.dataGridView.ShowCellToolTips = false;
			this.dataGridView.ShowEditingIcon = false;
			this.dataGridView.ShowRowErrors = false;
			this.dataGridView.Size = new System.Drawing.Size(567, 424);
			this.dataGridView.TabIndex = 1;
			this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.button1.FlatAppearance.BorderSize = 3;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.DarkRed;
			this.button1.Location = new System.Drawing.Point(51, 403);
			this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(117, 18);
			this.button1.TabIndex = 3;
			this.button1.Text = "очистить";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// StateEstimationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Silver;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.Name = "StateEstimationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "State Estimation";
			this.Load += new System.EventHandler(this.StateEstimationForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ListBox listTable;
		private System.Windows.Forms.Button buttonSE;
		private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem1;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.ToolStripMenuItem загрузитьБРToolStripMenuItem;
		private System.Windows.Forms.TextBox eventLog;
		private System.Windows.Forms.ToolStripMenuItem парамерыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem тМToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem бРToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem оСToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem параметрыНастройкиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem моделированиеТМToolStripMenuItem;
		private System.Windows.Forms.Button button1;
	}
}

