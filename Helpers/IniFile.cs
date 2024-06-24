using System.IO;

namespace DeathCounter.Helpers
{
    public class IniFile
    {
        private readonly string _filePath;

        public IniFile(string filePath)
        {
            _filePath = filePath;
        }

        public Dictionary<string, string> Read()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if(!File.Exists(_filePath))
                return dictionary;

            string[] lines = File.ReadAllLines(_filePath);

            foreach(string line in lines.Where(line => line.Contains('=')))
            {
                string[] parts = line.Split(new char[] { '=' }, 2);
                if(parts.Length >= 2)
                    dictionary[parts[0].Trim()] = parts[1].Trim();
            }

            return dictionary;
        }

        public void Write(Dictionary<string, string> dictionary)
        {
            List<string> lines = new List<string>();

            foreach(var kvp in dictionary)
            {
                lines.Add($"{kvp.Key}={kvp.Value}");
            }

            File.WriteAllLines(_filePath, lines);
        }

        public void Append(Dictionary<string, string> dictionary)
        {
            List<string> lines = new List<string>();

            foreach(var kvp in dictionary)
            {
                lines.Add($"{kvp.Key}={kvp.Value}");
            }

            lines.Add("___________________________________________");  // Add a separator line

            File.AppendAllLines(_filePath, lines);
        }
    }
}