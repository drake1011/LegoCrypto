using LegoCrypto.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoCrypto.WPF.App.ViewModels
{
    public class TokenItemViewModel
    {
        public ObservableCollection<string> TokenTypes { get; }
        public ObservableCollection<IToken> Tokens { get; private set; }

        public TokenItemViewModel()
        {
            TokenTypes = new ObservableCollection<string>(new string[] { "Character", "Vehicles" });
            Tokens = new ObservableCollection<IToken>();
        }
    }
}
