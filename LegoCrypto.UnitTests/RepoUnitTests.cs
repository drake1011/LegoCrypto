using LegoCrypto.Data;
using LegoCrypto.Data.Model;
using System.IO;
using Xunit;

namespace LegoCrypto.UnitTests
{
    public class RepoUnitTests
    {
        private readonly string _charactermap = $"Resources{Path.DirectorySeparatorChar}charactermap.json";
        private readonly string _tokenmap = $"Resources{Path.DirectorySeparatorChar}tokenmap.json";
        private readonly string _tempCharactermap = $"Resources{Path.DirectorySeparatorChar}_charactermap.json";
        private readonly string _tempTokenmap = $"Resources{Path.DirectorySeparatorChar}_tokenmap.json";

        private readonly Character _character = new Character { ID = 1, Name = "Test", World = "myWorld" };
        private readonly Vehicle _vehicle = new Vehicle { ID = 2000, Name = "Vehicle", Rebuild = 0 };

        [Fact]
        public void Test_Load_Characters()
        {
            var tokens = TokenRepo<Character>.Load(_charactermap);
            Assert.True(tokens.Length > 0);
        }

        [Fact]
        public void Test_Load_Vehicles()
        {
            var tokens = TokenRepo<Vehicle>.Load(_tokenmap);
            Assert.True(tokens.Length > 0);
        }

        [Fact]
        public void Test_Write_Characters()
        {
            var characters = new Character[] { _character, _character, _character };

            if (File.Exists(_tempCharactermap))
                File.Delete(_tempCharactermap);

            TokenRepo<Character>.Write(characters, _tempCharactermap);
            Assert.True(File.Exists(_tempCharactermap));

            var tokens = TokenRepo<Character>.Load(_tempCharactermap);
            Assert.Equal(tokens.Length, characters.Length);

            if (File.Exists(_tempCharactermap))
                File.Delete(_tempCharactermap);
        }

        [Fact]
        public void Test_Write_Vehicle()
        {
            var vehicles = new Vehicle[] { _vehicle, _vehicle };

            if (File.Exists(_tempTokenmap))
                File.Delete(_tempTokenmap);

            TokenRepo<Vehicle>.Write(vehicles, _tempTokenmap);
            Assert.True(File.Exists(_tempTokenmap));

            var tokens = TokenRepo<Vehicle>.Load(_tempTokenmap);
            Assert.Equal(tokens.Length, vehicles.Length);

            if (File.Exists(_tempTokenmap))
                File.Delete(_tempTokenmap);
        }
    }
}
