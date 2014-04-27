using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DummyProject;
using System.Windows.Forms;
using DummyProject.Objects.Buildings.DataLayer;
using StrategyGame.Screens;
using DummyProject.Helpers;
using DummyProject.Objects;
using StrategyGameLibrary;

namespace StrategyGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StrategyGameThread XNAgameThread = null;

        public MainWindow()
        {
            InitializeComponent();

            //inicianlizando a surface do jogo
            XNAgameThread = new StrategyGameThread(pcbTeste.Handle, this, pcbTeste);

            //setando o metodo do sizechanged do picture box pro StrategyGameThread tratar
            pcbTeste.SizeChanged += XNAgameThread.pictureBox_SizeChanged;

            //setando a localização do jogo
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo("pt-BR");
        }

        //private void updateTitle()
        //{
        //    this.Title = "Altura: " + this.ActualHeight.ToString() + "; Largura: " + this.ActualWidth.ToString();
        //    if (XNAgameThread.mapTeste != null)
        //    {
        //        this.Title += "Offiset.X: " + XNAgameThread.mapTeste.Offset.X.ToString()
        //        + "Offiset.Y: " + XNAgameThread.mapTeste.Offset.Y.ToString();
        //    }
        //}

        public void OnSelectBuilding(HoldableObject b)
        {
            if (b != null)
            {
                SelectedBuildingWindow teste = new SelectedBuildingWindow(b);
                teste.ShowDialog();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //colocando a XNA surface na tela

            GameSession.OnSelectBuilding += this.OnSelectBuilding;

            XNAgameThread.Run();

            //this.SizeChanged += new SizeChangedEventHandler(XNAgameThread.UpdateTitle);

            #region clean
            //GameComponent teste = new GameComponent();
            //
            //teste.Height = (int)WindowTeste.ActualHeight;
            //teste.Width = (int)WindowTeste.ActualWidth;
            ////teste.Location = new System.Drawing.Point(100, 100);
            //
            //WindowTeste.Child.Controls.Add(teste);
            //WindowTeste.Child = new DummyUserControl();
            //WindowTeste.Child.Controls.Add();
            #endregion
        }

        private void MenuItem_Click_Teste(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1(this);

            w.ShowDialog();
        }

        private void mainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            //fechando a thread do jogo
            XNAgameThread.Exit();
        }

        private void mainWindow_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //System.Windows.MessageBox.Show("asdjasldj");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            GenericBuildingTest.Teste();
        }
    }
}
