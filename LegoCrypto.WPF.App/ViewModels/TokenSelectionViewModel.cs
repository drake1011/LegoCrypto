using LegoCrypto.Data;
using LegoCrypto.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoCrypto.WPF.App
{
    public class TokenSelectionViewModel : ViewModelBase
    {
        private readonly string _charactermap = $"Resources{Path.DirectorySeparatorChar}charactermap.json";
        private Character[] _characters;
        public Character[] Characters { get => _characters; set => SetProperty(ref _characters, value); }

        private readonly string _vehiclemap = $"Resources{Path.DirectorySeparatorChar}vehiclemap.json";
        private Vehicle[] _vehicles;
        public Vehicle[] Vehicles { get => _vehicles; set => SetProperty(ref _vehicles, value); }

        public TokenSelectionViewModel()
        {
            Characters = TokenRepo<Character>.Load(_charactermap);
            Vehicles = TokenRepo<Vehicle>.Load(_vehiclemap);
        }
    }
}
