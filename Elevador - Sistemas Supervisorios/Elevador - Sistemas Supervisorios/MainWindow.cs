using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ElevadorSistemasSupervisorios
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            //Definindo parents para habilitar a transparencia
            FloorIndicatorText.Parent = InternalPanelImage;
            FloorIndicatorText.BackColor = Color.Transparent;

            DirectionIndicatorImage.Parent = InternalPanelImage;
            DirectionIndicatorImage.BackColor = Color.Transparent;
            
            //Setup dos botões
            SetupButton(Floor1Button, () => { Debug.WriteLine("Clicou"); });

            //Carregas as fontes custom
            FloorIndicatorText.Font = GetCustomFont(Properties.Resources.LLDOT2__, FloorIndicatorText.Font.Size);
            Floor1Button.Font = GetCustomFont(Properties.Resources.Bebas_Regular, Floor1Button.Font.Size);
        }

        private Font GetCustomFont(byte[] fontData, float size)
        {
            var pfc = new PrivateFontCollection();
            var fontLength = fontData.Length;
            var data = Marshal.AllocCoTaskMem(fontLength);

            Marshal.Copy(fontData, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);

            return new Font(pfc.Families[0], size);
        }

        private void SetupButton(Button button, Action onClick)
        {
            //Definindo parents para habilitar a transparencia
            button.Parent = InternalPanelImage;
            button.BackColor = Color.Transparent;

            var increaseSizeTimer = new Timer();
            var decreaseSizeTimer = new Timer();
            increaseSizeTimer.Interval = 50;
            decreaseSizeTimer.Interval = 50;

            increaseSizeTimer.Stop();
            decreaseSizeTimer.Stop();

            var animationDuration = TimeSpan.FromMilliseconds(100);
            var animationStarted = DateTime.Now;

            var initialWidth = button.Width;
            var initialHeight = button.Height;

            //Vai diminuir 10% do tamanho original
            var resizePercent = 0.01f;
            var finalWidth = initialWidth + initialWidth * resizePercent;
            var finalHeight = initialHeight + initialHeight * resizePercent;

            button.Click += (sender, args) =>
            {
                increaseSizeTimer.Stop();
                decreaseSizeTimer.Start();
                animationStarted = DateTime.Now;

                onClick();
            };

            decreaseSizeTimer.Tick += (sender, args) =>
            {
                var percentComplete = (float)(DateTime.Now - animationStarted).Ticks / animationDuration.Ticks;

                if (percentComplete >= 1)
                {
                    decreaseSizeTimer.Stop();
                    increaseSizeTimer.Start();
                    animationStarted = DateTime.Now;
                }
                else
                {
                    button.Width = (int)(initialWidth - (finalWidth - initialWidth) * percentComplete);
                    button.Height = (int)(initialHeight - (finalHeight - initialHeight) * percentComplete);
                }
            };

            increaseSizeTimer.Tick += (sender, args) =>
            {
                var percentComplete = (float)(DateTime.Now - animationStarted).Ticks / animationDuration.Ticks;
                if (percentComplete >= 1)
                {
                    increaseSizeTimer.Stop();
                }
                else
                {
                    button.Width = (int)(finalWidth + (finalWidth - initialWidth) * percentComplete);
                    button.Height = (int)(finalHeight + (finalHeight - initialWidth) * percentComplete);
                }
            };
        }
    }
}
