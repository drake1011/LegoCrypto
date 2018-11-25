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
        private Character[] _characters;
        public Character[] Characters { get => _characters; set => SetProperty(ref _characters, value); }

        private Vehicle[] _vehicles;
        public Vehicle[] Vehicles { get => _vehicles; set => SetProperty(ref _vehicles, value); }

        private string _statusMessage = string.Empty;
        public string StatusMessage { get => _statusMessage; set => SetProperty(ref _statusMessage, value); }

        public TokenSelectionViewModel(Character[] characters, Vehicle[] vehicles)
        {
            DisplayName = "Tokens";
            StatusMessage += " | ";

            Characters = characters;
            StatusMessage += $"{Characters?.Length} Characters";

            StatusMessage += " | ";

            Vehicles = vehicles;
            StatusMessage += $"{Vehicles?.Length} Vehicles";
        }
    }
}
