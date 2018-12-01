using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using LegoCrypto.Data.Model;
using Newtonsoft.Json;

namespace LegoCrypto.Data
{
    public class TokenRepo
    {
        public IToken SelectedToken { get; set; }
        public ObservableCollection<IToken> Characters { get; set; }
        public ObservableCollection<IToken> Vehicles { get; set; }

        public TokenRepo()
        {
            Characters = new ObservableCollection<IToken>();
            Vehicles = new ObservableCollection<IToken>();
        }

        public void LoadData(string charactermap, string vehiclemap)
        {
            try
            {
                Characters = new ObservableCollection<IToken>(LoadTokens<Character>(charactermap));

            }
            catch(Exception ex)
            {

            }

            try
            {
                Vehicles = new ObservableCollection<IToken>(LoadTokens<Vehicle>(vehiclemap));
            }
            catch (Exception ex)
            {

            }

        }

        public void WriteData()
        {

        }

        public T[] LoadTokens<T>(string filename) where T : IToken
        {
            return TokenRepoData<T>.LoadJson(filename);
        }

        public void WriteTokens(IToken[] tokens, string filename)
        {
            TokenRepoData<IToken>.WriteJson(tokens, filename);
        }
    }
}
