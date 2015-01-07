namespace SnmpDGet
{
	partial class Form_detail
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_detail));
			this.m_string = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.変換cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.m_oid = new System.Windows.Forms.TextBox();
			this.m_objectname = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_string
			// 
			this.m_string.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_string.BackColor = System.Drawing.SystemColors.Window;
			this.m_string.ContextMenuStrip = this.contextMenuStrip1;
			this.m_string.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.m_string.Location = new System.Drawing.Point(12, 65);
			this.m_string.Multiline = true;
			this.m_string.Name = "m_string";
			this.m_string.ReadOnly = true;
			this.m_string.Size = new System.Drawing.Size(397, 132);
			this.m_string.TabIndex = 2;
			this.m_string.TabStop = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.変換cToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(117, 26);
			// 
			// 変換cToolStripMenuItem
			// 
			this.変換cToolStripMenuItem.Name = "変換cToolStripMenuItem";
			this.変換cToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.変換cToolStripMenuItem.Text = "変換(&c)";
			this.変換cToolStripMenuItem.Click += new System.EventHandler(this.変換cToolStripMenuItem_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button1.Location = new System.Drawing.Point(351, 203);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(93, 30);
			this.button1.TabIndex = 4;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// m_oid
			// 
			this.m_oid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_oid.BackColor = System.Drawing.SystemColors.Window;
			this.m_oid.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.m_oid.Location = new System.Drawing.Point(12, 12);
			this.m_oid.Name = "m_oid";
			this.m_oid.ReadOnly = true;
			this.m_oid.Size = new System.Drawing.Size(432, 22);
			this.m_oid.TabIndex = 0;
			this.m_oid.TabStop = false;
			// 
			// m_objectname
			// 
			this.m_objectname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_objectname.BackColor = System.Drawing.SystemColors.Window;
			this.m_objectname.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.m_objectname.Location = new System.Drawing.Point(12, 38);
			this.m_objectname.Name = "m_objectname";
			this.m_objectname.ReadOnly = true;
			this.m_objectname.Size = new System.Drawing.Size(432, 22);
			this.m_objectname.TabIndex = 1;
			this.m_objectname.TabStop = false;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button2.Location = new System.Drawing.Point(415, 66);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(29, 69);
			this.button2.TabIndex = 3;
			this.button2.Text = "変換";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form_detail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(456, 240);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.m_objectname);
			this.Controls.Add(this.m_oid);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.m_string);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form_detail";
			this.Text = "詳細";
			this.Load += new System.EventHandler(this.Form_detail_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox m_string;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox m_oid;
		private System.Windows.Forms.TextBox m_objectname;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 変換cToolStripMenuItem;
	}
}