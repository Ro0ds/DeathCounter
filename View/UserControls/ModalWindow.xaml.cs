using System.Windows;
using System.Windows.Input;

namespace DeathCounter.View.UserControls
{
    public partial class ModalWindow : Window
    {
        public event EventHandler<string>? SubmitClicked;

        public ModalWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnSalvarNome_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtGameName.Text))
            {
                MessageBox.Show("Digite um nome para o jogo.", "Nome incorreto.", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                string userInput = txtGameName.Text;
                SubmitClicked?.Invoke(this, userInput);

                this.Close();
            }
        }
    }
}
