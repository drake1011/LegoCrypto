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

        private const string _charTagImpl = "CharacterTag";
        private const string _tokenTagImpl = "TokenTag";

        [TestMethod]
        public void Test_CreateTag_Character_id_uid_Pass()
        {
            var tag = TagFactory.CreateTag(id: _char_id, uid: _uid);

            //verify the correct implementation was created
            Assert.AreEqual(tag.GetType().Name, _charTagImpl);
            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_char_id, tag.ID);
        }

        [TestMethod]
        public void Test_CreateTag_Token_id_uid_Pass()
        {
            var tag = TagFactory.CreateTag(id: _token_id, uid: _uid);

            //verify the correct implementation was created
            Assert.AreEqual(tag.GetType().Name, _tokenTagImpl);
            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_token_id, tag.ID);
        }

        [TestMethod]
        public void Test_CreateTag_Character_data_Pass()
        {
            var tag = TagFactory.CreateTag(data: _uid + _char_page35 + _char_page36 + _char_page37 + _char_page38);

            //verify the correct implementation was created
            var test = tag.GetType();
            Assert.AreEqual(tag.GetType().Name, _charTagImpl);
            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_char_id, tag.ID);
            Assert.AreEqual(_char_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(_char_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(_char_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(_char_page38, tag.Pages[DataRegister.Page38]);
        }

        [TestMethod]
        public void Test_CreateTag_Token_data_Pass()
        {
            var tag = TagFactory.CreateTag(data: _uid + _token_page35 + _token_page36 + _token_page37 + _token_page38);

            //verify the correct implementation was created
            Assert.AreEqual(tag.GetType().Name, _tokenTagImpl);
            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_token_id, tag.ID);
            Assert.AreEqual(_token_page35, tag.Pages[DataRegister.Page35]);
            Assert.AreEqual(_token_page36, tag.Pages[DataRegister.Page36]);
            Assert.AreEqual(_token_page37, tag.Pages[DataRegister.Page37]);
            Assert.AreEqual(_token_page38, tag.Pages[DataRegister.Page38]);
        }

        [TestMethod]
        public void Test_Encrypt_Character_Pass()
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
        public void Test_Decrypt_Character_data_Pass()
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
        public void Test_Encrypt_Token_Pass()
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
        public void Test_Decrypt_Token_data_Pass()
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

        [TestMethod]
        public void Test_Decrypt_Character_id_Pass()
        {
            var tag = TagFactory.CreateTag(id: _char_id, uid: _uid);
            tag.Decrypt();

            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_char_id, tag.ID);
            Assert.AreEqual(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [TestMethod]
        public void Test_Decrypt_Token_id_Pass()
        {
            var tag = TagFactory.CreateTag(id: _token_id, uid: _uid);
            tag.Decrypt();

            Assert.AreEqual(_uid, tag.UID);
            Assert.AreEqual(_token_id, tag.ID);
            Assert.AreEqual(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [TestMethod]
        public void Test_CreateTag_validate_ID_Not_Set_Fail()
        {
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(id: 0, uid: _uid));
        }

        [TestMethod]
        public void Test_CreateTag_validate_UID_Not_Set_Fail()
        {
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: null));
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: string.Empty));
        }

        [TestMethod]
        public void Test_CreateTag_validate_UID_Not_Valid_Length_Fail()
        {
            const string nibble = "A";

            // too short
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: nibble));
            // too long
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: _uid + nibble));
        }

        [TestMethod]
        public void Test_CreateTag_validate_FullData_Not_Valid_Length_Fail()
        {
            const string dataShort = _uid + _char_page35;
            const string dataLong =
                _uid +
                _char_page35 +
                _char_page36 +
                _char_page37 +
                _char_page38 +
                _char_page38;

            // too short
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(data: dataShort));
            // too long
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(data: dataLong));
        }

        [TestMethod]
        public void Test_CreateTag_validate_DataPage_Not_Valid_Length_Fail()
        {
            const string dataPageOK = "00000000";
            const string dataPageLong = "123456789";
            const string dataPageShort = "123";

            #region dataPageLong
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageLong,
                dataPage36: dataPageLong,
                dataPage37: dataPageLong,
                dataPage38: dataPageLong
                ));

            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageLong,
                dataPage36: dataPageOK,
                dataPage37: dataPageOK,
                dataPage38: dataPageOK
                ));

            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageLong,
                dataPage37: dataPageOK,
                dataPage38: dataPageOK
                ));

            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageOK,
                dataPage37: dataPageLong,
                dataPage38: dataPageOK
                ));

            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageOK,
                dataPage37: dataPageOK,
                dataPage38: dataPageLong
                ));
            #endregion
            #region dataPageShort
            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageShort,
                dataPage36: dataPageShort,
                dataPage37: dataPageShort,
                dataPage38: dataPageShort
                ));

            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageShort,
                dataPage36: dataPageOK,
                dataPage37: dataPageOK,
                dataPage38: dataPageOK
                ));

            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageShort,
                dataPage37: dataPageOK,
                dataPage38: dataPageOK
                ));

            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageOK,
                dataPage37: dataPageShort,
                dataPage38: dataPageOK
                ));

            Assert.ThrowsException<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageOK,
                dataPage37: dataPageOK,
                dataPage38: dataPageShort
                ));
            #endregion
        }

        [TestMethod]
        public void Test_CreateTag_validate_Invalid_Hex_UID_Fail()
        {
            const string uidInvalid = "04F4E7BADC4C8Z";
            Assert.ThrowsException<FormatException>(() => TagFactory.CreateTag(id: _char_id, uid: uidInvalid));
        }

        [TestMethod]
        public void Test_CreateTag_validate_Invalid_Hex_data_Fail()
        {
            const string uidInvalid = "04F4E7BADC4C8Z";
            const string dataInvalid = "000N0000";
            Assert.ThrowsException<FormatException>(() => TagFactory.CreateTag(data: _uid + _char_page35 + dataInvalid + _char_page37 + _char_page38));
            Assert.ThrowsException<FormatException>(() => TagFactory.CreateTag(data: uidInvalid + _char_page35 + _char_page36 + _char_page37 + _char_page38));
        }
    }
}
