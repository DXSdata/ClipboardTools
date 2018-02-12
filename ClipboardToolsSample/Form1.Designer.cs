namespace ClipboardToolsSample
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.linkLabelManualStart = new System.Windows.Forms.LinkLabel();
            this.checkBoxOutlook = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Copy some special/virtual content to the clipboard, e.g. an email from eM Client." +
    "..";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(28, 147);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(43, 13);
            this.labelResult.TabIndex = 1;
            this.labelResult.Text = "[Result]";
            // 
            // linkLabelManualStart
            // 
            this.linkLabelManualStart.AutoSize = true;
            this.linkLabelManualStart.Location = new System.Drawing.Point(681, 49);
            this.linkLabelManualStart.Name = "linkLabelManualStart";
            this.linkLabelManualStart.Size = new System.Drawing.Size(65, 13);
            this.linkLabelManualStart.TabIndex = 2;
            this.linkLabelManualStart.TabStop = true;
            this.linkLabelManualStart.Text = "Manual start";
            this.linkLabelManualStart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelManualStart_LinkClicked);
            // 
            // checkBoxOutlook
            // 
            this.checkBoxOutlook.AutoSize = true;
            this.checkBoxOutlook.Location = new System.Drawing.Point(31, 79);
            this.checkBoxOutlook.Name = "checkBoxOutlook";
            this.checkBoxOutlook.Size = new System.Drawing.Size(226, 17);
            this.checkBoxOutlook.TabIndex = 4;
            this.checkBoxOutlook.Text = "Use special way for Outlook msg handling.";
            this.checkBoxOutlook.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 405);
            this.Controls.Add(this.checkBoxOutlook);
            this.Controls.Add(this.linkLabelManualStart);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Clipboard Tools Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.LinkLabel linkLabelManualStart;
        private System.Windows.Forms.CheckBox checkBoxOutlook;
    }
}

