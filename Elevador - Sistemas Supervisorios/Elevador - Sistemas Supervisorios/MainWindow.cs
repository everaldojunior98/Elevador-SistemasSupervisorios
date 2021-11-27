using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ElevadorSistemasSupervisorios.ElevatorSystem;
using ElevadorSistemasSupervisorios.Properties;
using static ElevadorSistemasSupervisorios.ElevatorSystem.ElevatorUtils;

namespace ElevadorSistemasSupervisorios
{
    public partial class MainWindow : Form
    {
        private const string BebasFontName = "Bebas-Regular";
        private readonly Font bebasFont;

        private const string LCDFontName = "LLDOT2__";
        private readonly Font lcdFont;

        private readonly PrivateFontCollection customFontCollection;

        private readonly Elevator elevator;

        private readonly Simulation simulation;

        private readonly Dictionary<int, Button> internalButtonsClicked;
        private readonly Dictionary<int, Tuple<Button, Button>> externalButtonsClicked;

        private bool stopped;

        public MainWindow()
        {
            InitializeComponent();

            stopped = true;

            elevator = new Elevator();
            elevator.OnFloorChanged += ElevatorOnFloorChanged;
            elevator.OnStoppedFloor += ElevatorOnStoppedFloor;

            simulation = new Simulation(elevator);
            simulation.OnGeneratedFloor += SimulationOnOnGeneratedFloor;

            internalButtonsClicked = new Dictionary<int, Button>();
            externalButtonsClicked = new Dictionary<int, Tuple<Button, Button>>();

            //Carregando as fontes custom
            customFontCollection = new PrivateFontCollection();
            bebasFont = AddFontFile(BebasFontName);
            lcdFont = AddFontFile(LCDFontName);

            //Definindo parents para habilitar a transparencia
            FloorIndicatorText.Parent = InternalPanelImage;
            FloorIndicatorText.BackColor = Color.Transparent;

            DirectionIndicatorImage.Parent = InternalPanelImage;
            DirectionIndicatorImage.BackColor = Color.Transparent;
            DirectionIndicatorImage.Visible = false;

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
            ManualCheckBox.Checked = true;

            //Setup dos botões do painel interno
            SetupButton(Floor1Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor1Button, 1); });
            SetupButton(Floor2Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor2Button, 2); });
            SetupButton(Floor3Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor3Button, 3); });
            SetupButton(Floor4Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor4Button, 4); });
            SetupButton(Floor5Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor5Button, 5); });
            SetupButton(Floor6Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor6Button, 6); });
            SetupButton(Floor7Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor7Button, 7); });
            SetupButton(Floor8Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor8Button, 8); });
            SetupButton(Floor9Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor9Button, 9); });
            SetupButton(Floor10Button, InternalPanelImage, () => { OnInternalButtonClicked(Floor10Button, 10); });

            //Setup dos botões do painel externo
            SetupFloor(Floor1Text, Floor1PanelImage, UpFloor1Button, null, () => { OnExternalButtonClicked(UpFloor1Button, null, 1, ElevatorDirection.UP); }, () => { });
            SetupFloor(Floor2Text, Floor2PanelImage, UpFloor2Button, DownFloor2Button, () => { OnExternalButtonClicked(UpFloor2Button, DownFloor2Button, 2, ElevatorDirection.UP); }, () => { OnExternalButtonClicked(UpFloor2Button, DownFloor2Button, 2, ElevatorDirection.DOWN); });
            SetupFloor(Floor3Text, Floor3PanelImage, UpFloor3Button, DownFloor3Button, () => { OnExternalButtonClicked(UpFloor3Button, DownFloor3Button, 3, ElevatorDirection.UP); }, () => { OnExternalButtonClicked(UpFloor3Button, DownFloor3Button, 3, ElevatorDirection.DOWN); });
            SetupFloor(Floor4Text, Floor4PanelImage, UpFloor4Button, DownFloor4Button, () => { OnExternalButtonClicked(UpFloor4Button, DownFloor4Button, 4, ElevatorDirection.UP); }, () => { OnExternalButtonClicked(UpFloor4Button, DownFloor4Button, 4, ElevatorDirection.DOWN); });
            SetupFloor(Floor5Text, Floor5PanelImage, UpFloor5Button, DownFloor5Button, () => { OnExternalButtonClicked(UpFloor5Button, DownFloor5Button, 5, ElevatorDirection.UP); }, () => { OnExternalButtonClicked(UpFloor5Button, DownFloor5Button, 5, ElevatorDirection.DOWN); });
            SetupFloor(Floor6Text, Floor6PanelImage, UpFloor6Button, DownFloor6Button, () => { OnExternalButtonClicked(UpFloor6Button, DownFloor6Button, 6, ElevatorDirection.UP); }, () => { OnExternalButtonClicked(UpFloor6Button, DownFloor6Button, 6, ElevatorDirection.DOWN); });
            SetupFloor(Floor7Text, Floor7PanelImage, UpFloor7Button, DownFloor7Button, () => { OnExternalButtonClicked(UpFloor7Button, DownFloor7Button, 7, ElevatorDirection.UP); }, () => { OnExternalButtonClicked(UpFloor7Button, DownFloor7Button, 7, ElevatorDirection.DOWN); });
            SetupFloor(Floor8Text, Floor8PanelImage, UpFloor8Button, DownFloor8Button, () => { OnExternalButtonClicked(UpFloor8Button, DownFloor8Button, 8, ElevatorDirection.UP); }, () => { OnExternalButtonClicked(UpFloor8Button, DownFloor8Button, 8, ElevatorDirection.DOWN); });
            SetupFloor(Floor9Text, Floor9PanelImage, UpFloor9Button, DownFloor9Button, () => { OnExternalButtonClicked(UpFloor9Button, DownFloor9Button, 9, ElevatorDirection.UP); }, () => { OnExternalButtonClicked(UpFloor9Button, DownFloor9Button, 9, ElevatorDirection.DOWN); });
            SetupFloor(Floor10Text, Floor10PanelImage, null, DownFloor10Button, () => { }, () => { OnExternalButtonClicked(null, DownFloor10Button, 10, ElevatorDirection.DOWN); });

            //Carregas as fontes custom
            FloorIndicatorText.Font = GetCustomFont(lcdFont, FloorIndicatorText.Font.Size);
        }

        private void SimulationOnOnGeneratedFloor(object source, OnGeneratedFloorEventArgs args)
        {
            var floor = args.Floor;
            var direction = args.Direction;

            var image = new Bitmap(direction == ElevatorDirection.UP
                ? Resources.BotaoVermelho2
                : Resources.BotaoVermelho2Baixo);

            Button buttonUp = null;
            Button buttonDown = null;

            switch (floor)
            {
                case 1:
                    buttonUp = UpFloor1Button;
                    buttonDown = null;
                    break;
                case 2:
                    buttonUp = UpFloor2Button;
                    buttonDown = DownFloor2Button;
                    break;
                case 3:
                    buttonUp = UpFloor3Button;
                    buttonDown = DownFloor3Button;
                    break;
                case 4:
                    buttonUp = UpFloor4Button;
                    buttonDown = DownFloor4Button;
                    break;
                case 5:
                    buttonUp = UpFloor5Button;
                    buttonDown = DownFloor5Button;
                    break;
                case 6:
                    buttonUp = UpFloor6Button;
                    buttonDown = DownFloor6Button;
                    break;
                case 7:
                    buttonUp = UpFloor7Button;
                    buttonDown = DownFloor7Button;
                    break;
                case 8:
                    buttonUp = UpFloor8Button;
                    buttonDown = DownFloor8Button;
                    break;
                case 9:
                    buttonUp = UpFloor9Button;
                    buttonDown = DownFloor9Button;
                    break;
                case 10:
                    buttonUp = null;
                    buttonDown = DownFloor10Button;
                    break;
            }

            if (direction == ElevatorDirection.UP && buttonUp != null)
                buttonUp.BackgroundImage = image;
            else if(buttonDown != null)
                buttonDown.BackgroundImage = image;

            if (!externalButtonsClicked.ContainsKey(floor))
                externalButtonsClicked.Add(floor, Tuple.Create(buttonUp, buttonDown));
        }

        private void OnInternalButtonClicked(Button button, int floor)
        {
            if(stopped && elevator.GetCurrentFloor() == floor)
                return;

            var image = new Bitmap(Resources.BotaoVermelho);
            button.BackgroundImage = image;

            if (!internalButtonsClicked.ContainsKey(floor))
                internalButtonsClicked.Add(floor, button);

            elevator.RequestFloor(floor);
        }

        private void OnExternalButtonClicked(Button buttonUp, Button buttonDown, int floor, ElevatorDirection direction)
        {
            if (stopped && elevator.GetCurrentFloor() == floor)
                return;

            var image = new Bitmap(direction == ElevatorDirection.UP
                ? Resources.BotaoVermelho2
                : Resources.BotaoVermelho2Baixo);

            if (direction == ElevatorDirection.UP)
                buttonUp.BackgroundImage = image;
            else
                buttonDown.BackgroundImage = image;

            if (!externalButtonsClicked.ContainsKey(floor))
                externalButtonsClicked.Add(floor, Tuple.Create(buttonUp, buttonDown));

            elevator.RequestFloor(floor, direction);
        }

        private void ElevatorOnStoppedFloor(object source, EventArgs args)
        {
            stopped = true;
            var currentFloor = elevator.GetCurrentFloor();
            if (internalButtonsClicked.ContainsKey(currentFloor))
            {
                var button = internalButtonsClicked[currentFloor];
                button.Invoke(new Action(() =>
                {
                    var image = new Bitmap(Resources.Botao);
                    button.BackgroundImage = image;
                }));

                internalButtonsClicked.Remove(currentFloor);
            }

            if (externalButtonsClicked.ContainsKey(currentFloor))
            {
                var buttons = externalButtonsClicked[currentFloor];

                buttons.Item1?.Invoke(new Action(() =>
                {
                    var image = new Bitmap(Resources.Botao2);
                    buttons.Item1.BackgroundImage = image;
                }));
                buttons.Item2?.Invoke(new Action(() =>
                {
                    var image = new Bitmap(Resources.Botao2Baixo);
                    buttons.Item2.BackgroundImage = image;
                }));

                externalButtonsClicked.Remove(currentFloor);
            }

            DirectionIndicatorImage.Invoke(new Action(() =>
            {
                DirectionIndicatorImage.Visible = false;
            }));
        }

        private void ElevatorOnFloorChanged(object source, OnFloorChangedEventArgs args)
        {
            stopped = false;
            FloorIndicatorText.Invoke(new Action(() => FloorIndicatorText.Text = args.Floor.ToString()));

            DirectionIndicatorImage.Invoke(new Action(() =>
            {
                DirectionIndicatorImage.Visible = true;
                var arrow = new Bitmap(args.Direction == ElevatorDirection.UP
                    ? Resources.SetaCima
                    : Resources.SetaBaixo);
                DirectionIndicatorImage.Image = arrow;
            }));
        }

        private void AutomaticCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ManualCheckBox.Checked = !AutomaticCheckBox.Checked;

            if (ManualCheckBox.Checked)
            {
                simulation.Disable();

                UpFloor1Button.Enabled = true;
                UpFloor2Button.Enabled = true;
                UpFloor3Button.Enabled = true;
                UpFloor4Button.Enabled = true;
                UpFloor5Button.Enabled = true;
                UpFloor6Button.Enabled = true;
                UpFloor7Button.Enabled = true;
                UpFloor8Button.Enabled = true;
                UpFloor9Button.Enabled = true;

                DownFloor2Button.Enabled = true;
                DownFloor3Button.Enabled = true;
                DownFloor4Button.Enabled = true;
                DownFloor5Button.Enabled = true;
                DownFloor6Button.Enabled = true;
                DownFloor7Button.Enabled = true;
                DownFloor8Button.Enabled = true;
                DownFloor9Button.Enabled = true;
                DownFloor10Button.Enabled = true;
            }

            if (AutomaticCheckBox.Checked)
            {
                simulation.Enable();

                UpFloor1Button.Enabled = false;
                UpFloor2Button.Enabled = false;
                UpFloor3Button.Enabled = false;
                UpFloor4Button.Enabled = false;
                UpFloor5Button.Enabled = false;
                UpFloor6Button.Enabled = false;
                UpFloor7Button.Enabled = false;
                UpFloor8Button.Enabled = false;
                UpFloor9Button.Enabled = false;

                DownFloor2Button.Enabled = false;
                DownFloor3Button.Enabled = false;
                DownFloor4Button.Enabled = false;
                DownFloor5Button.Enabled = false;
                DownFloor6Button.Enabled = false;
                DownFloor7Button.Enabled = false;
                DownFloor8Button.Enabled = false;
                DownFloor9Button.Enabled = false;
                DownFloor10Button.Enabled = false;
            }
        }

        private void ManualCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AutomaticCheckBox.Checked = !ManualCheckBox.Checked;

            if (ManualCheckBox.Checked)
            {
                simulation.Disable();

                UpFloor1Button.Enabled = true;
                UpFloor2Button.Enabled = true;
                UpFloor3Button.Enabled = true;
                UpFloor4Button.Enabled = true;
                UpFloor5Button.Enabled = true;
                UpFloor6Button.Enabled = true;
                UpFloor7Button.Enabled = true;
                UpFloor8Button.Enabled = true;
                UpFloor9Button.Enabled = true;

                DownFloor2Button.Enabled = true;
                DownFloor3Button.Enabled = true;
                DownFloor4Button.Enabled = true;
                DownFloor5Button.Enabled = true;
                DownFloor6Button.Enabled = true;
                DownFloor7Button.Enabled = true;
                DownFloor8Button.Enabled = true;
                DownFloor9Button.Enabled = true;
                DownFloor10Button.Enabled = true;
            }

            if (AutomaticCheckBox.Checked)
            {
                simulation.Enable();

                UpFloor1Button.Enabled = false;
                UpFloor2Button.Enabled = false;
                UpFloor3Button.Enabled = false;
                UpFloor4Button.Enabled = false;
                UpFloor5Button.Enabled = false;
                UpFloor6Button.Enabled = false;
                UpFloor7Button.Enabled = false;
                UpFloor8Button.Enabled = false;
                UpFloor9Button.Enabled = false;

                DownFloor2Button.Enabled = false;
                DownFloor3Button.Enabled = false;
                DownFloor4Button.Enabled = false;
                DownFloor5Button.Enabled = false;
                DownFloor6Button.Enabled = false;
                DownFloor7Button.Enabled = false;
                DownFloor8Button.Enabled = false;
                DownFloor9Button.Enabled = false;
                DownFloor10Button.Enabled = false;
            }
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
            if(buttonUp != null)
                SetupButton(buttonUp, parent, onClickUp);
            if (buttonDown != null)
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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            elevator.Dispose();
            simulation.Dispose();
        }
    }
}
