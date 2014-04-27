using System.Windows;
using DummyProject.Objects;
using DummyProject.Objects.Buildings.DataLayer;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace StrategyGame.Screens
{
    /// <summary>
    /// Interaction logic for SelectedBuildingWindow.xaml
    /// </summary>
    public partial class SelectedBuildingWindow : Window
    {
        private HoldableObject Building = null;

        public SelectedBuildingWindow(HoldableObject b)
        {
            InitializeComponent();

            Building = b;

            this.Title = Building.ToString();
           
            
        }
    }
}
