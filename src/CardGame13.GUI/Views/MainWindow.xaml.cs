using CardGame13.GUI.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace CardGame13.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClosable
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel(this);
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e) => Close();

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            DragMove();
        }
    }
}
