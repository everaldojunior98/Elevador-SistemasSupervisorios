
namespace ElevadorSistemasSupervisorios
{
    partial class MainWindow
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.FloorIndicatorText = new System.Windows.Forms.Label();
            this.Floor1Button = new System.Windows.Forms.Button();
            this.DirectionIndicatorImage = new System.Windows.Forms.PictureBox();
            this.InternalPanelImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DirectionIndicatorImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InternalPanelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // FloorIndicatorText
            // 
            this.FloorIndicatorText.AutoSize = true;
            this.FloorIndicatorText.BackColor = System.Drawing.Color.Transparent;
            this.FloorIndicatorText.Font = new System.Drawing.Font("LLDot", 50.24999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FloorIndicatorText.ForeColor = System.Drawing.Color.Red;
            this.FloorIndicatorText.Location = new System.Drawing.Point(90, 37);
            this.FloorIndicatorText.Name = "FloorIndicatorText";
            this.FloorIndicatorText.Size = new System.Drawing.Size(94, 68);
            this.FloorIndicatorText.TabIndex = 1;
            this.FloorIndicatorText.Text = "10";
            this.FloorIndicatorText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Floor1Button
            // 
            this.Floor1Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Floor1Button.BackColor = System.Drawing.Color.Transparent;
            this.Floor1Button.BackgroundImage = global::ElevadorSistemasSupervisorios.Properties.Resources.Botao;
            this.Floor1Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Floor1Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Floor1Button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Floor1Button.FlatAppearance.BorderSize = 0;
            this.Floor1Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Floor1Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Floor1Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Floor1Button.Font = new System.Drawing.Font("Bebas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Floor1Button.ForeColor = System.Drawing.Color.Black;
            this.Floor1Button.Location = new System.Drawing.Point(52, 373);
            this.Floor1Button.Name = "Floor1Button";
            this.Floor1Button.Size = new System.Drawing.Size(55, 55);
            this.Floor1Button.TabIndex = 3;
            this.Floor1Button.Text = "1";
            this.Floor1Button.UseVisualStyleBackColor = false;
            // 
            // DirectionIndicatorImage
            // 
            this.DirectionIndicatorImage.BackColor = System.Drawing.Color.Transparent;
            this.DirectionIndicatorImage.Image = global::ElevadorSistemasSupervisorios.Properties.Resources.SetaBaixo;
            this.DirectionIndicatorImage.Location = new System.Drawing.Point(52, 37);
            this.DirectionIndicatorImage.Name = "DirectionIndicatorImage";
            this.DirectionIndicatorImage.Size = new System.Drawing.Size(41, 64);
            this.DirectionIndicatorImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DirectionIndicatorImage.TabIndex = 2;
            this.DirectionIndicatorImage.TabStop = false;
            // 
            // InternalPanelImage
            // 
            this.InternalPanelImage.Image = global::ElevadorSistemasSupervisorios.Properties.Resources.PainelInterno;
            this.InternalPanelImage.Location = new System.Drawing.Point(0, 0);
            this.InternalPanelImage.Name = "InternalPanelImage";
            this.InternalPanelImage.Size = new System.Drawing.Size(226, 533);
            this.InternalPanelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.InternalPanelImage.TabIndex = 0;
            this.InternalPanelImage.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(960, 534);
            this.Controls.Add(this.Floor1Button);
            this.Controls.Add(this.DirectionIndicatorImage);
            this.Controls.Add(this.FloorIndicatorText);
            this.Controls.Add(this.InternalPanelImage);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DirectionIndicatorImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InternalPanelImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox InternalPanelImage;
        private System.Windows.Forms.Label FloorIndicatorText;
        private System.Windows.Forms.PictureBox DirectionIndicatorImage;
        private System.Windows.Forms.Button Floor1Button;
    }
}

