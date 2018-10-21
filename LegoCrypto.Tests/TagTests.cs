using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LegoCrypto.Data.Model;

namespace LegoCrypto.Tests
{
    [TestClass]
    public class TagTests
    {
        // verify with https://ldcharcrypto.ags131.com/

        readonly string uid = "04F4E7BADC4C80";
        readonly uint char_id = 23;

        readonly string char_page35 = "00000000";
        readonly string char_page36 = "4949D04D";
        readonly string char_page37 = "1BE3A012";
        readonly string char_page38 = "00000000";

        readonly uint token_id = 1011;

        readonly string token_page35 = "00000000";
        readonly string token_page36 = "F3030000";
        readonly string token_page37 = "00000000";
        readonly string token_page38 = "00010000";

        readonly string pwd = "2E144C21";

        [TestMethod]
        public void Test_Character_Encrypt_Pass()
        {
            var tag = TagFactory.CreateTag(char_id, uid);
            tag.Encrypt();

            Assert.AreEqual(uid, tag.UID);
            Assert.AreEqual(char_id, tag.ID);
            Assert.AreEqual(char_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(char_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(char_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(char_page38, tag.Pages[DataRegister.Page38]);
            Assert.AreEqual(pwd, tag.Pages[DataRegister.Page43]);
        }

        [TestMethod]
        public void Test_Character_Decrypt_Pass()
        {
            var tag = TagFactory.CreateTag(uid + char_page35 + char_page36 + char_page37 + char_page38);
            tag.Decrypt();

            Assert.AreEqual(uid, tag.UID);
            Assert.AreEqual(char_id, tag.ID);
            Assert.AreEqual(char_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(char_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(char_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(char_page38, tag.Pages[DataRegister.Page38]);
            Assert.AreEqual(pwd, tag.Pages[DataRegister.Page43]);
        }

        [TestMethod]
        public void Test_Token_Encrypt_Pass()
        {
            var tag = TagFactory.CreateTag(token_id, uid);
            tag.Encrypt();

            Assert.AreEqual(uid, tag.UID);
            Assert.AreEqual(token_id, tag.ID);
            Assert.AreEqual(token_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(token_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(token_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(token_page38, tag.Pages[DataRegister.Page38]);
            Assert.AreEqual(pwd, tag.Pages[DataRegister.Page43]);
        }

        [TestMethod]
        public void Test_Token_Decrypt_Pass()
        {
            var tag = TagFactory.CreateTag(uid + token_page35 + token_page36 + token_page37 + token_page38);
            tag.Decrypt();

            Assert.AreEqual(uid, tag.UID);
            Assert.AreEqual(token_id, tag.ID);
            Assert.AreEqual(token_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(token_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(token_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(token_page38, tag.Pages[DataRegister.Page38]);
            Assert.AreEqual(pwd, tag.Pages[DataRegister.Page43]);
        }
    }
}
