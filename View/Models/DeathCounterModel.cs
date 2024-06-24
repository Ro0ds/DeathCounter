namespace DeathCounter.View.Models
{
    public class DeathCounterModel
    {
		private string _gameName = string.Empty;
		public string GameName
		{
			get => _gameName;
			set 
			{ 
				_gameName = value; 
			}
		}

        private int _deaths;
        public int Deaths
        {
            get => _deaths;
            set
            {
                _deaths = value;
            }
        }

		private DateTime _timeSpan;

		public DateTime TimeSpan
        {
			get => _timeSpan;
			set 
			{ 
				_timeSpan = value;
			}
		}
	}
}