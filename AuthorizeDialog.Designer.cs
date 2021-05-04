
namespace OAuth2ExampleApp
{
	partial class AuthorizeDialog
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
			this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
			((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
			this.SuspendLayout();
			// 
			// webView
			// 
			this.webView.CreationProperties = null;
			this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
			this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webView.Location = new System.Drawing.Point(0, 0);
			this.webView.Margin = new System.Windows.Forms.Padding(4);
			this.webView.Name = "webView";
			this.webView.Size = new System.Drawing.Size(1182, 853);
			this.webView.Source = new System.Uri("about:blank", System.UriKind.Absolute);
			this.webView.TabIndex = 1;
			this.webView.ZoomFactor = 1D;
			this.webView.NavigationStarting += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(this.webView_NavigationStarting);
			// 
			// AuthorizeDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1182, 853);
			this.Controls.Add(this.webView);
			this.MinimumSize = new System.Drawing.Size(850, 600);
			this.Name = "AuthorizeDialog";
			this.Text = "Authorize";
			((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.Web.WebView2.WinForms.WebView2 webView;
	}
}