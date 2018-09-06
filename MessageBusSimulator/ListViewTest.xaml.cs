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
using System.Collections.ObjectModel;

namespace MessageBusSimulator
{
    /// <summary>
    /// ListViewTest.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ListViewTest : Page
    {
        public ListViewTest()
        {
            GameCollection.Add(new GameData { GameName = "FirstGame", Creator = "FirstCreator" });
            InitializeComponent();
        }
        public ObservableCollection<GameData> GameDataCollection
        {
            get
            {
                return GameCollection;
            }
        }

        public ObservableCollection<GameData> GameCollection { get; set; } = new ObservableCollection<GameData>();

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            GameCollection.Add(new GameData { GameName = "NewGame", Creator = "NewCreator" });
        }
    }
    public class GameData
    {
        public string GameName { get; set; }
        public string Creator { get; set; }
    }
}
