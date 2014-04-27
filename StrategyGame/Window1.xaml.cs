using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StrategyGame.CustomComponents;

namespace StrategyGame
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(Window owner)
        {
            InitializeComponent();

            Dictionary<string, string> a;

            this.Owner = owner;

            BitmapImage img = new BitmapImage(new Uri(@"C:\shared\render_recente\cubo_terra.png"));

            lsbBuildings.Items.Add(new BuildingCard(img, "teste"));
            lsbBuildings.Items.Add(new BuildingCard(img, "Factory1"));
            lsbBuildings.Items.Add(new BuildingCard(img, "Factory2"));
            lsbBuildings.Items.Add(new BuildingCard(img, "Factory3"));
            lsbBuildings.Items.Add(new BuildingCard(img, "Factory4"));
            lsbBuildings.Items.Add(new BuildingCard(img, "Factory5"));
            
            //grdMain.Children.Add(new BuildingCard(img, "teste"));
            //grdMain.Children.Add(new BuildingCard(img, "teste"));
        }

        private void winMain_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void grdMain_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = (this.Owner.Height / 2) + this.Owner.Top - (this.Width / 2);
            this.Left = this.Owner.Left + 20;
        }
    }
}
