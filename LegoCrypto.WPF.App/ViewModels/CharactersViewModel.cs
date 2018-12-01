using LegoCrypto.Data;
using LegoCrypto.Data.Model;
using System.Collections.ObjectModel;

namespace LegoCrypto.WPF.App.ViewModels
{
    public class CharactersViewModel : ViewModelBase, ITokenViewModel
    {
        public ObservableCollection<IToken> Tokens { get; set; }

        public CharactersViewModel(TokenRepo tokenRepo)
        {
            DisplayName = "Characters";
            Tokens = tokenRepo.Characters;
        }
    }
}
