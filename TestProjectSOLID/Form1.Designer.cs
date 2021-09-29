
namespace TestProjectSOLID
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.DateSearch = new System.Windows.Forms.DateTimePicker();
            this.GetCurrencyRateBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите дату:";
            // 
            // DateSearch
            // 
            this.DateSearch.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateSearch.Location = new System.Drawing.Point(95, 34);
            this.DateSearch.Name = "DateSearch";
            this.DateSearch.Size = new System.Drawing.Size(114, 20);
            this.DateSearch.TabIndex = 1;
            // 
            // GetCurrencyRateBtn
            // 
            this.GetCurrencyRateBtn.Location = new System.Drawing.Point(15, 74);
            this.GetCurrencyRateBtn.Name = "GetCurrencyRateBtn";
            this.GetCurrencyRateBtn.Size = new System.Drawing.Size(124, 43);
            this.GetCurrencyRateBtn.TabIndex = 2;
            this.GetCurrencyRateBtn.Text = "Получить данные";
            this.GetCurrencyRateBtn.UseVisualStyleBackColor = true;
            this.GetCurrencyRateBtn.Click += new System.EventHandler(this.GetCurrencyRateBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 173);
            this.Controls.Add(this.GetCurrencyRateBtn);
            this.Controls.Add(this.DateSearch);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DateSearch;
        private System.Windows.Forms.Button GetCurrencyRateBtn;
    }
}

