using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Scenario_1;
using Scenario_2;
using Scenario_3;
namespace SelectionWIndow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnScenario1_Click(object sender, RoutedEventArgs e)
        {
            NewWindow scenarioOne = new NewWindow();
            scenarioOne.Show();
        }

        private void btnScenario2_Click(object sender, RoutedEventArgs e)
        {
            PeaceWindow scenarioTwo = new PeaceWindow();
            scenarioTwo.Show();
        }

        private void btnScenario3_Click(object sender, RoutedEventArgs e)
        {
            WonderlandWindow scenarioThree = new WonderlandWindow();
            scenarioThree.Show();
        }
    }
}
