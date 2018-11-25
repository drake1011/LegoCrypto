using LegoCrypto.Data;
using LegoCrypto.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LegoCrypto.WPF.App.ViewModels
{
    public class TokenSelectionViewModel : ViewModelBase
    {
        private readonly string _charactermap = "charactermap.json";
        private readonly string _vehiclemap = "vehiclemap.json";

        private Character[] _characters;
        public Character[] Characters { get => _characters; set => SetProperty(ref _characters, value); }

        private Vehicle[] _vehicles;
        public Vehicle[] Vehicles { get => _vehicles; set => SetProperty(ref _vehicles, value); }

        private string _statusMessage = string.Empty;
        public string StatusMessage { get => _statusMessage; set => SetProperty(ref _statusMessage, value); }

        public TokenSelectionViewModel()
        {
            DisplayName = "Tokens";
            StatusMessage += " | ";
            try
            {
                Characters = TokenRepo<Character>.Load($"Resources{Path.DirectorySeparatorChar}{_charactermap}");
                StatusMessage += $"{Characters.Length} Characters";
            }
            catch
            {
                StatusMessage += $"Error loading {_charactermap}";
            }

            StatusMessage += " | ";

            try
            { 
                Vehicles = TokenRepo<Vehicle>.Load($"Resources{Path.DirectorySeparatorChar}{_vehiclemap}");
                StatusMessage += $"{Vehicles.Length} Vehicles";
            }
            catch
            {
                StatusMessage += $"Error loading {_vehiclemap}";
            }
        }
    }
}
