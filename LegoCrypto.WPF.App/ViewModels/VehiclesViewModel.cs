using LegoCrypto.Data;
using LegoCrypto.Data.Model;
using System.Collections.ObjectModel;

namespace LegoCrypto.WPF.App.ViewModels
{
    public class VehiclesViewModel : ViewModelBase, ITokenViewModel
    {
        public ObservableCollection<IToken> Tokens { get; set; }

        public VehiclesViewModel(TokenRepo tokenRepo)
        {
            DisplayName = "Vehicles";
            Tokens = tokenRepo.Vehicles;
        }
    }
}
