namespace SnmpDGet
{
	partial class Form_DetailList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_DetailList));
			this.button1 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.変換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(642, 513);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(97, 30);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.ContextMenuStrip = this.contextMenuStrip1;
			this.listView1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(12, 21);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(727, 486);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.変換ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(117, 26);
			// 
			// 変換ToolStripMenuItem
			// 
			this.変換ToolStripMenuItem.Name = "変換ToolStripMenuItem";
			this.変換ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.変換ToolStripMenuItem.Text = "変換(&c)";
			this.変換ToolStripMenuItem.Click += new System.EventHandler(this.変換ToolStripMenuItem_Click);
			// 
			// Form_DetailList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(751, 555);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.button1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form_DetailList";
			this.Text = "詳細";
			this.Load += new System.EventHandler(this.Form_DetailDisp_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 変換ToolStripMenuItem;
	}
}