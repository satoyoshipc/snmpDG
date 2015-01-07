namespace SnmpDGet
{
	partial class Form_Convert
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Convert));
			this.button1 = new System.Windows.Forms.Button();
			this.m_text = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button1.Location = new System.Drawing.Point(154, 289);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 30);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// m_text
			// 
			this.m_text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_text.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.m_text.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.m_text.Location = new System.Drawing.Point(12, 12);
			this.m_text.Multiline = true;
			this.m_text.Name = "m_text";
			this.m_text.ReadOnly = true;
			this.m_text.Size = new System.Drawing.Size(396, 270);
			this.m_text.TabIndex = 2;
			// 
			// Form_Convert
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(420, 322);
			this.Controls.Add(this.m_text);
			this.Controls.Add(this.button1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form_Convert";
			this.Text = "変換";
			this.Load += new System.EventHandler(this.Form_Convert_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox m_text;
	}
}