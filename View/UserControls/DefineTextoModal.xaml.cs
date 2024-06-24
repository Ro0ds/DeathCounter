using System.Windows;
using System.Windows.Input;

namespace DeathCounter.View.UserControls
{
    public partial class DefineTextoModal : Window
    {
        public event EventHandler<string>? SubmitClicked;

        public DefineTextoModal()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnSalvarNome_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtDeathText.Text))
            {
                string userInput = "Falecimentos:";
                SubmitClicked?.Invoke(this, userInput);

                this.Close();
            }
            else
            {
                string userInput = txtDeathText.Text;
                SubmitClicked?.Invoke(this, userInput);

                this.Close();
            }
        }
    }
}
