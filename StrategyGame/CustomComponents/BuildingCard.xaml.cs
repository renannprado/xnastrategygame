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
    /// Interaction logic for BuildingCard.xaml
    /// </summary>
    public partial class BuildingCard : UserControl
    {
        public BuildingCard(BitmapImage imgCardBuilding, String buildingName)
        {
            InitializeComponent();

            this.imgBuilding.Source = imgCardBuilding;
            this.lblBuildingName.Content = buildingName;
        }
    }
}
