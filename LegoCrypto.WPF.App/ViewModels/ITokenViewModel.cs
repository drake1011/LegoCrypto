using LegoCrypto.Data.Model;
using System.Collections.ObjectModel;

namespace LegoCrypto.WPF.App.ViewModels
{
    public interface ITokenViewModel
    {
        ObservableCollection<IToken> Tokens { get; set; }
    }
}
