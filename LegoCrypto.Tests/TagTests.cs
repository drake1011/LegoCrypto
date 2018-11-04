using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LegoCrypto.Data.Model;

namespace LegoCrypto.Tests
{
    [TestClass]
    public class TagTests
    {
        // verify with https://ldcharcrypto.ags131.com/

        private const string _uid = "04F4E7BADC4C80";
        private const uint _char_id = 23;

        private const string _char_page35 = "00000000";
        private const string _char_page36 = "4949D04D";
        private const string _char_page37 = "1BE3A012";
        private const string _char_page38 = "00000000";

        private const uint _token_id = 1011;

        private const string _token_page35 = "00000000";
        private const string _token_page36 = "F3030000";
        private const string _token_page37 = "00000000";
        private const string _token_page38 = "00010000";

        private const string _pwd = "2E144C21";

        [TestMethod]
        public void Test_Character_Encrypt_Pass()
        {
            var tag = TagFactory.CreateTag(id: _char_id, uid: _uid);
            tag.Encrypt();

            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_char_id, tag.ID);
            Assert.AreEqual(_char_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(_char_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(_char_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(_char_page38, tag.Pages[DataRegister.Page38]);
            Assert.AreEqual(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [TestMethod]
        public void Test_Character_Decrypt_Pass()
        {
            var tag = TagFactory.CreateTag(data: _uid + _char_page35 + _char_page36 + _char_page37 + _char_page38);
            tag.Decrypt();

            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_char_id, tag.ID);
            Assert.AreEqual(_char_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(_char_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(_char_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(_char_page38, tag.Pages[DataRegister.Page38]);
            Assert.AreEqual(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [TestMethod]
        public void Test_Token_Encrypt_Pass()
        {
            var tag = TagFactory.CreateTag(id: _token_id, uid: _uid);
            tag.Encrypt();

            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_token_id, tag.ID);
            Assert.AreEqual(_token_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(_token_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(_token_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(_token_page38, tag.Pages[DataRegister.Page38]);
            Assert.AreEqual(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [TestMethod]
        public void Test_Token_Decrypt_Pass()
        {
            var tag = TagFactory.CreateTag(data: _uid + _token_page35 + _token_page36 + _token_page37 + _token_page38);
            tag.Decrypt();

            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_token_id, tag.ID);
            Assert.AreEqual(_token_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(_token_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(_token_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(_token_page38, tag.Pages[DataRegister.Page38]);
            Assert.AreEqual(_pwd, tag.Pages[DataRegister.Page43]);
        }
    }
}
