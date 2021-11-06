using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ElevadorSistemasSupervisorios
{
    public partial class MainWindow : Form
    {
        private const string BebasFontName = "Bebas-Regular";
        private readonly Font bebasFont;

        private const string LCDFontName = "LLDOT2__";
        private readonly Font lcdFont;

        private PrivateFontCollection customFontCollection;

        public MainWindow()
        {
            InitializeComponent();

            //Carregando as fontes custom
            customFontCollection = new PrivateFontCollection();
            bebasFont = AddFontFile(BebasFontName);
            lcdFont = AddFontFile(LCDFontName);

            //Definindo parents para habilitar a transparencia
            FloorIndicatorText.Parent = InternalPanelImage;
            FloorIndicatorText.BackColor = Color.Transparent;

            DirectionIndicatorImage.Parent = InternalPanelImage;
            DirectionIndicatorImage.BackColor = Color.Transparent;

            //Texto Painel do Modo Operacional
            OperationText.Parent = OperationPanelImage;
            OperationText.BackColor = Color.Transparent;
            OperationText.Font = GetCustomFont(bebasFont, OperationText.Font.Size);

            //CheckBox Painel do Modo Operacional
            ManualCheckBox.Parent = OperationPanelImage;
            ManualCheckBox.BackColor = Color.Transparent;
            ManualCheckBox.Font = GetCustomFont(bebasFont, ManualCheckBox.Font.Size);
            AutomaticCheckBox.Parent = OperationPanelImage;
            AutomaticCheckBox.BackColor = Color.Transparent;
            AutomaticCheckBox.Font = GetCustomFont(bebasFont, AutomaticCheckBox.Font.Size);

            //Painel Integrantes do Trabalho
            StudentsText.Parent = StudentsPanelImage;
            StudentsText.BackColor = Color.Transparent;
            StudentsText.Font = GetCustomFont(bebasFont, StudentsText.Font.Size);
            EveraldoText.Parent = StudentsPanelImage;
            EveraldoText.BackColor = Color.Transparent;
            EveraldoText.Font = GetCustomFont(bebasFont, EveraldoText.Font.Size);
            GabrielText.Parent = StudentsPanelImage;
            GabrielText.BackColor = Color.Transparent;
            GabrielText.Font = GetCustomFont(bebasFont, GabrielText.Font.Size);

            //CheckBox Modo Operacional
            AutomaticCheckBox.CheckedChanged += AutomaticCheckBox_CheckedChanged;
            ManualCheckBox.CheckedChanged += ManualCheckBox_CheckedChanged;
            AutomaticCheckBox.Checked = true;

            //Setup dos botões do painel interno
            SetupButton(Floor1Button, InternalPanelImage, () => { FloorIndicatorText.Text = "1"; });
            SetupButton(Floor2Button, InternalPanelImage, () => { FloorIndicatorText.Text = "2"; });
            SetupButton(Floor3Button, InternalPanelImage, () => { FloorIndicatorText.Text = "3"; });
            SetupButton(Floor4Button, InternalPanelImage, () => { FloorIndicatorText.Text = "4"; });
            SetupButton(Floor5Button, InternalPanelImage, () => { FloorIndicatorText.Text = "5"; });
            SetupButton(Floor6Button, InternalPanelImage, () => { FloorIndicatorText.Text = "6"; });
            SetupButton(Floor7Button, InternalPanelImage, () => { FloorIndicatorText.Text = "7"; });
            SetupButton(Floor8Button, InternalPanelImage, () => { FloorIndicatorText.Text = "8"; });
            SetupButton(Floor9Button, InternalPanelImage, () => { FloorIndicatorText.Text = "9"; });
            SetupButton(Floor10Button, InternalPanelImage, () => { FloorIndicatorText.Text = "10"; });

            //Setup dos botões do painel externo
            SetupFloor(Floor1Text, Floor1PanelImage, UpFloor1Button, DownFloor1Button, () => { }, () => { });
            SetupFloor(Floor2Text, Floor2PanelImage, UpFloor2Button, DownFloor2Button, () => { }, () => { });
            SetupFloor(Floor3Text, Floor3PanelImage, UpFloor3Button, DownFloor3Button, () => { }, () => { });
            SetupFloor(Floor4Text, Floor4PanelImage, UpFloor4Button, DownFloor4Button, () => { }, () => { });
            SetupFloor(Floor5Text, Floor5PanelImage, UpFloor5Button, DownFloor5Button, () => { }, () => { });
            SetupFloor(Floor6Text, Floor6PanelImage, UpFloor6Button, DownFloor6Button, () => { }, () => { });
            SetupFloor(Floor7Text, Floor7PanelImage, UpFloor7Button, DownFloor7Button, () => { }, () => { });
            SetupFloor(Floor8Text, Floor8PanelImage, UpFloor8Button, DownFloor8Button, () => { }, () => { });
            SetupFloor(Floor9Text, Floor9PanelImage, UpFloor9Button, DownFloor9Button, () => { }, () => { });
            SetupFloor(Floor10Text, Floor10PanelImage, UpFloor10Button, DownFloor10Button, () => { }, () => { });

            //Carregas as fontes custom
            FloorIndicatorText.Font = GetCustomFont(lcdFont, FloorIndicatorText.Font.Size);
        }

        private void AutomaticCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(AutomaticCheckBox.Checked)
                ManualCheckBox.Checked = false;
            else
                ManualCheckBox.Checked = true;
        }

        private void ManualCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualCheckBox.Checked)
                AutomaticCheckBox.Checked = false;
            else
                AutomaticCheckBox.Checked = true;
        }

        private Font AddFontFile(string fontName)
        {
            customFontCollection.AddFontFile(Path.Combine(Application.StartupPath, "Fonts", $"{fontName}.ttf"));
            return new Font(customFontCollection.Families.Last(), 20f);
        }

        private Font GetCustomFont(Font font, float fontSize)
        {
            return new Font(font.FontFamily, fontSize);
        }

        private void SetupFloor(Label floorLabel, Control parent, Button buttonUp, Button buttonDown, Action onClickUp,
            Action onClickDown)
        {
            floorLabel.Parent = parent;
            floorLabel.BackColor = Color.Transparent;
            floorLabel.Font = GetCustomFont(bebasFont, floorLabel.Font.Size);
            SetupButton(buttonUp, parent, onClickUp);
            SetupButton(buttonDown, parent, onClickDown);
        }

        private void SetupButton(Button button, Control parent, Action onClick)
        {
            //Carrega a fonte custom
            button.Font = GetCustomFont(bebasFont, button.Font.Size);

            //Definindo parents para habilitar a transparencia
            button.Parent = parent;
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

            //Vai diminuir 1% do tamanho original
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
