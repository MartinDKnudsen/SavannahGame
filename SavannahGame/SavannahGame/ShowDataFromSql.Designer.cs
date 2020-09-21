namespace SavannahGame
{
    partial class ShowDataFromSql
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewFromDatabase = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFromDatabase)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewFromDatabase
            // 
            this.dataGridViewFromDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFromDatabase.Location = new System.Drawing.Point(0, 28);
            this.dataGridViewFromDatabase.Name = "dataGridViewFromDatabase";
            this.dataGridViewFromDatabase.Size = new System.Drawing.Size(851, 446);
            this.dataGridViewFromDatabase.TabIndex = 0;
            this.dataGridViewFromDatabase.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFromDatabase_CellContentClick);
            // 
            // ShowDataFromSql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewFromDatabase);
            this.Name = "ShowDataFromSql";
            this.Size = new System.Drawing.Size(851, 474);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFromDatabase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFromDatabase;
    }
}
