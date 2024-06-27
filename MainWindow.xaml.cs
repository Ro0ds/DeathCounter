using DeathCounter.View.UserControls;
using DeathCounter.View.Models;
using DeathCounter.Helpers;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace DeathCounter
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _isFileSaved = false;

        private string _DeathLabelText = "Deaths";
        public string DeathLabelText
        {
            get => _DeathLabelText;
            set 
            { 
                _DeathLabelText = value;
                OnPropertyChanged();
            }
        }

        private int _Deaths = 0;
        public int Deaths
        {
            get => _Deaths;
            set 
            { 
                _Deaths = value; 
                OnPropertyChanged();
            }
        }

        private string _GameName = "Game";
        public string GameName
        {
            get => _GameName;
            set 
            { 
                _GameName = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            DataContext = this;
            
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ContextMenu = (ContextMenu)Resources["MainMenu"];

            // checking if config and stats exists
            string currentDir = Environment.CurrentDirectory;
            const string CONFIG_FILE_NAME = "config.ini";
            const string STATS_FILE_NAME = "GameStats.ini";

            string configFile = Path.Combine(currentDir, CONFIG_FILE_NAME);
            string statsFile = Path.Combine(currentDir, STATS_FILE_NAME);

            var configExists = File.Exists(configFile);
            var statsExists = File.Exists(statsFile);

            if(configExists)
            {
                IniFile iniFile = new IniFile(configFile);
                Dictionary<string, string> config = iniFile.Read();

                this.GameName = config["GameName"];
                this.DeathLabelText = config["DeathLabelText"];
            }

            if(statsExists)
            {
                IniFile iniFile = new IniFile(statsFile);
                Dictionary<string, string> stats = iniFile.Read();

                this.Deaths = int.Parse(stats["Deaths"]);
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Fechar_Click(object sender, RoutedEventArgs e)
        {
            if(_isFileSaved)
                DeathCounter.Close();
            else
            {
                MessageBoxResult result = MessageBox.Show("O arquivo não foi salvo, deseja salvar?", "Salvar Arquivo", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if(result == MessageBoxResult.Yes)
                {
                    string fileName = "GameStats.ini";
                    string path = Environment.CurrentDirectory;

                    bool arquivoSalvo = SalvarConteudoNoArquivo(path, fileName);
                    _isFileSaved = arquivoSalvo;
                    DeathCounter.Close();
                }
                else
                    DeathCounter.Close();
            }
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Deseja salvar o resultado da partida?", "Salvar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if(result == MessageBoxResult.Yes) 
            { 
                string fileName = "GameStats.ini";
                string path = Environment.CurrentDirectory;

                bool arquivoSalvo = SalvarConteudoNoArquivo(path, fileName);
                _isFileSaved = arquivoSalvo;
            }
        }

        private void SalvarConfigs_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Deseja salvar as configurações?", "Salvar", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                (bool, string) arquivoSalvo = SaveConfigFile();
                if(arquivoSalvo.Item1)
                    MessageBox.Show($"Configuração salva com sucesso!\nCaminho: {arquivoSalvo.Item2}", "Salvar configuração", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool SalvarConteudoNoArquivo(string path, string fileName, bool isConfig = false)
        {
            bool caminhoVazio = string.IsNullOrEmpty(path);
            bool nomeVazio = string.IsNullOrEmpty(fileName);

            if(caminhoVazio || nomeVazio)
            {
                MessageBox.Show($"Não foi possível salvar, dados insuficientes.\nCaminho do arquivo: {path}\nNome do arquivo: {fileName}");
                return false;
            }

            if(isConfig)
            {
                (bool, string) fileSaved = SaveConfigFile();
                if(fileSaved.Item1)
                    MessageBox.Show($"Configuração salva com sucesso!\nCaminho: {fileSaved.Item2}", "Salvar configuração", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string caminhoInteiro = Path.Join(path, fileName);

                DeathCounterModel deathCounterModel = new DeathCounterModel
                {
                    GameName = this.GameName,
                    Deaths = this.Deaths,
                    TimeSpan = DateTime.Now
                };

                bool fileSaved = SaveFile(caminhoInteiro, deathCounterModel);
                if(fileSaved)
                {
                    MessageBox.Show($"Conteúdo salvo com sucesso!\nCaminho: {caminhoInteiro}", "Salvar arquivo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
            }

            return false;
        }

        private bool SaveFile(string fullPath, DeathCounterModel deathCounterModel)
        {
            Dictionary<string, string>  modelDict = new Dictionary<string, string>
            {
                { "GameName", deathCounterModel.GameName },
                { "Deaths", deathCounterModel.Deaths.ToString() },
                { "TimeSpan", deathCounterModel.TimeSpan.ToString() },
            };

            try
            {
                IniFile iniFile = new IniFile(fullPath);
                iniFile.Append(modelDict);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Não foi possível salvar o arquivo.\nErro: {ex.Message}", "Salvar arquivo de configuração", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private (bool, string) SaveConfigFile()
        {
            string configFileName = "config.ini";
            string configPath = Environment.CurrentDirectory;
            string configFilePath = Path.Combine(configPath, configFileName);

            ConfigurationModel configurationModel = new ConfigurationModel
            {
                GameNameText = this.GameName,
                DeathCounterLabelText = this.DeathLabelText,
            };

            Dictionary<string, string> configDict = new Dictionary<string, string>
            {
                { "GameName", configurationModel.GameNameText },
                { "DeathLabelText", configurationModel.DeathCounterLabelText }
            };

            try
            {
                IniFile iniFile = new IniFile(configFilePath);
                iniFile.Write(configDict);
                return (true, configFilePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Não foi possível salvar o arquivo de configuração.\nErro: {ex.Message}", "Salvar arquivo de configuração", MessageBoxButton.OK, MessageBoxImage.Error);
                return (false, string.Empty);
            }
        }

        private void DefinirJogo_Click(object sender, RoutedEventArgs e)
        {
            ModalWindow modal = new ModalWindow();
            modal.Owner = this;
            modal.SubmitClicked += Modal_SubmitClicked!;
            modal.ShowDialog();
        }

        private void DefinirTxtMorte_Click(object sender, RoutedEventArgs e)
        {
            DefineTextoModal modal = new DefineTextoModal();
            modal.Owner = this;
            modal.SubmitClicked += DefinirTxtMorte_SubmitClicked!;
            modal.ShowDialog();
        }

        private void DefinirTxtMorte_SubmitClicked(object sender, string userInput)
        {
            DeathLabelText = userInput;
        }

        private void Modal_SubmitClicked(object sender, string userInput)
        {
            GameName = userInput;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.NumPad1)
                Deaths++;   
            else if(e.Key == Key.NumPad2 && Deaths > 0)
                Deaths--;
        }
    }
}