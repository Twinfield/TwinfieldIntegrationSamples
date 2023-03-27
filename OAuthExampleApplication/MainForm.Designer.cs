
namespace OAuth2ExampleApp
{
	partial class MainForm
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
			this.btnAuthorize = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnAuthorize
			// 
			this.btnAuthorize.AutoSize = true;
			this.btnAuthorize.Location = new System.Drawing.Point(12, 63);
			this.btnAuthorize.Name = "btnAuthorize";
			this.btnAuthorize.Size = new System.Drawing.Size(97, 27);
			this.btnAuthorize.TabIndex = 2;
			this.btnAuthorize.Text = "Authorize";
			this.btnAuthorize.UseVisualStyleBackColor = true;
			this.btnAuthorize.Click += new System.EventHandler(this.btnAuthorize_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(581, 34);
			this.label1.TabIndex = 3;
			this.label1.Text = "Make sure you create a client first at https://developers.twinfield.com and updat" +
	 "e the ClientId present in app.config file. \r\nClick Authorize to start.";
			// 
			// txtLog
			// 
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.Location = new System.Drawing.Point(12, 96);
			this.txtLog.MaxLength = 327670;
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(1043, 446);
			this.txtLog.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 554);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnAuthorize);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "OAuth2 Example App";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnAuthorize;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtLog;
	}
}

