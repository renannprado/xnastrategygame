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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StrategyGame.CustomComponents
{
    /// <summary>
    /// Interaction logic for BuildingInformationCard.xaml
    /// </summary>
    public partial class BuildingInformationCard : UserControl
    {
        public BuildingInformationCard()
        {
            InitializeComponent();

            this.imgBuilding.Source = new BitmapImage(new Uri(@"C:\shared\render_recente\cubo_terra.png"));
        }
    }
}
