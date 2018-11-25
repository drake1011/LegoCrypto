using LegoCrypto.Data.Model;
using SysLib.Bitwise;
using System;
using Xunit;

namespace LegoCrypto.UnitTests
{
    public class TagUnitTests
    {
        // verify with https://ldcharcrypto.ags131.com/

        private const string _uid = "04F4E7BADC4C80";
        private const uint _char_id = 23;

        private const string _char_page35 = "00000000";
        private const string _char_page36 = "4949D04D";
        private const string _char_page37 = "1BE3A012";
        private const string _char_page38 = "00000000";

        private const uint _token_id = 1011;

        private const string _vehicle_page35 = "00000000";
        private const string _vehicle_page36 = "F3030000";
        private const string _vehicle_page37 = "00000000";
        private const string _vehicle_page38 = "00010000";

        private const string _pwd = "2E144C21";

        private const string _charTagImpl = "CharacterTag";
        private const string _vehicleTagImpl = "VehicleTag";

        private const string _uid_invalid = "04F4E7BADC4C8Z";
        private const string _data_invalid = "000N0000";

        [Fact]
        public void Test_CreateTag_Character_id_uid_Pass()
        {
            var tag = TagFactory.CreateTag(id: _char_id, uid: _uid);

            //verify the correct implementation was created
            Assert.Equal(tag.GetType().Name, _charTagImpl);
            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_char_id, tag.ID);
        }

        [Fact]
        public void Test_CreateTag_Vehicle_id_uid_Pass()
        {
            var tag = TagFactory.CreateTag(id: _token_id, uid: _uid);

            //verify the correct implementation was created
            Assert.Equal(tag.GetType().Name, _vehicleTagImpl);
            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_token_id, tag.ID);
        }

        [Fact]
        public void Test_CreateTag_Character_data_Pass()
        {
            var tag = TagFactory.CreateTag(data: _uid + _char_page35 + _char_page36 + _char_page37 + _char_page38);

            //verify the correct implementation was created
            var test = tag.GetType();
            Assert.Equal(tag.GetType().Name, _charTagImpl);
            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_char_id, tag.ID);
            Assert.Equal(_char_page35, tag.Pages[DataRegister.Page35]);
            Assert.Equal(_char_page36, tag.Pages[DataRegister.Page36]);
            Assert.Equal(_char_page37, tag.Pages[DataRegister.Page37]);
            Assert.Equal(_char_page38, tag.Pages[DataRegister.Page38]);
        }

        [Fact]
        public void Test_CreateTag_Token_data_Pass()
        {
            var tag = TagFactory.CreateTag(data: _uid + _vehicle_page35 + _vehicle_page36 + _vehicle_page37 + _vehicle_page38);

            //verify the correct implementation was created
            Assert.Equal(tag.GetType().Name, _vehicleTagImpl);
            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_token_id, tag.ID);
            Assert.Equal(_vehicle_page35, tag.Pages[DataRegister.Page35]);
            Assert.Equal(_vehicle_page36, tag.Pages[DataRegister.Page36]);
            Assert.Equal(_vehicle_page37, tag.Pages[DataRegister.Page37]);
            Assert.Equal(_vehicle_page38, tag.Pages[DataRegister.Page38]);
        }

        [Fact]
        public void Test_Encrypt_Character_Pass()
        {
            var tag = TagFactory.CreateTag(id: _char_id, uid: _uid);
            tag.Encrypt();

            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_char_id, tag.ID);
            Assert.Equal(_char_page35, tag.Pages[DataRegister.Page35]);
            Assert.Equal(_char_page36, tag.Pages[DataRegister.Page36]);
            Assert.Equal(_char_page37, tag.Pages[DataRegister.Page37]);
            Assert.Equal(_char_page38, tag.Pages[DataRegister.Page38]);
            Assert.Equal(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [Fact]
        public void Test_Decrypt_Character_data_Pass()
        {
            var tag = TagFactory.CreateTag(data: _uid + _char_page35 + _char_page36 + _char_page37 + _char_page38);
            tag.Decrypt();

            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_char_id, tag.ID);
            Assert.Equal(_char_page35, tag.Pages[DataRegister.Page35]);
            Assert.Equal(_char_page36, tag.Pages[DataRegister.Page36]);
            Assert.Equal(_char_page37, tag.Pages[DataRegister.Page37]);
            Assert.Equal(_char_page38, tag.Pages[DataRegister.Page38]);
            Assert.Equal(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [Fact]
        public void Test_Encrypt_Token_Pass()
        {
            var tag = TagFactory.CreateTag(id: _token_id, uid: _uid);
            tag.Encrypt();

            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_token_id, tag.ID);
            Assert.Equal(_vehicle_page35, tag.Pages[DataRegister.Page35]);
            Assert.Equal(_vehicle_page36, tag.Pages[DataRegister.Page36]);
            Assert.Equal(_vehicle_page37, tag.Pages[DataRegister.Page37]);
            Assert.Equal(_vehicle_page38, tag.Pages[DataRegister.Page38]);
            Assert.Equal(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [Fact]
        public void Test_Decrypt_Token_data_Pass()
        {
            var tag = TagFactory.CreateTag(data: _uid + _vehicle_page35 + _vehicle_page36 + _vehicle_page37 + _vehicle_page38);
            tag.Decrypt();

            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_token_id, tag.ID);
            Assert.Equal(_vehicle_page35, tag.Pages[DataRegister.Page35]);
            Assert.Equal(_vehicle_page36, tag.Pages[DataRegister.Page36]);
            Assert.Equal(_vehicle_page37, tag.Pages[DataRegister.Page37]);
            Assert.Equal(_vehicle_page38, tag.Pages[DataRegister.Page38]);
            Assert.Equal(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [Fact]
        public void Test_Decrypt_Character_id_Pass()
        {
            var tag = TagFactory.CreateTag(id: _char_id, uid: _uid);
            tag.Decrypt();

            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_char_id, tag.ID);
            Assert.Equal(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [Fact]
        public void Test_Decrypt_Token_id_Pass()
        {
            var tag = TagFactory.CreateTag(id: _token_id, uid: _uid);
            tag.Decrypt();

            Assert.Equal(_uid, tag.UID);
            Assert.Equal(_token_id, tag.ID);
            Assert.Equal(_pwd, tag.Pages[DataRegister.Page43]);
        }

        [Fact]
        public void Test_CreateTag_validate_ID_Not_Set_Fail()
        {
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(id: 0, uid: _uid));
        }

        [Fact]
        public void Test_CreateTag_validate_UID_Not_Set_Fail()
        {
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: null));
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: string.Empty));
        }

        [Fact]
        public void Test_CreateTag_validate_UID_Not_Valid_Length_Fail()
        {
            const string nibble = "A";

            // too short
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: nibble));
            // too long
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: _uid + nibble));
        }

        [Fact]
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
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(data: dataShort));
            // too long
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(data: dataLong));
        }

        [Fact]
        public void Test_CreateTag_validate_DataPage_Not_Valid_Length_Fail()
        {
            const string dataPageOK = "00000000";
            const string dataPageLong = "123456789";
            const string dataPageShort = "123";

            #region dataPageLong
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageLong,
                dataPage36: dataPageLong,
                dataPage37: dataPageLong,
                dataPage38: dataPageLong
                ));

            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageLong,
                dataPage36: dataPageOK,
                dataPage37: dataPageOK,
                dataPage38: dataPageOK
                ));

            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageLong,
                dataPage37: dataPageOK,
                dataPage38: dataPageOK
                ));

            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageOK,
                dataPage37: dataPageLong,
                dataPage38: dataPageOK
                ));

            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageOK,
                dataPage37: dataPageOK,
                dataPage38: dataPageLong
                ));
            #endregion
            #region dataPageShort
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageShort,
                dataPage36: dataPageShort,
                dataPage37: dataPageShort,
                dataPage38: dataPageShort
                ));

            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageShort,
                dataPage36: dataPageOK,
                dataPage37: dataPageOK,
                dataPage38: dataPageOK
                ));

            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageShort,
                dataPage37: dataPageOK,
                dataPage38: dataPageOK
                ));

            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageOK,
                dataPage37: dataPageShort,
                dataPage38: dataPageOK
                ));

            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(
                uid: _uid,
                dataPage35: dataPageOK,
                dataPage36: dataPageOK,
                dataPage37: dataPageOK,
                dataPage38: dataPageShort
                ));
            #endregion
        }

        [Fact]
        public void Test_CreateTag_validate_Invalid_Hex_UID_Fail()
        {
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(id: _char_id, uid: _uid_invalid));
        }

        [Fact]
        public void Test_CreateTag_validate_Invalid_Hex_data_Fail()
        {
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(data: _uid + _char_page35 + _data_invalid + _char_page37 + _char_page38));
            Assert.Throws<ArgumentException>(() => TagFactory.CreateTag(data: _uid_invalid + _char_page35 + _char_page36 + _char_page37 + _char_page38));
        }

        [Fact]
        public void Test_Bitwise_validate_Hex_PassFail()
        {
            const string hexValid = "A";
            const string hexValid2 = "FF";
            const string hexInvalid = "0Z";
            const string hexInvalid2 = "AG";

            Assert.True(HexConverter.ContainsOnlyHexNibbles(_char_page35));
            Assert.True(HexConverter.ContainsOnlyHexNibbles(_char_page36));
            Assert.True(HexConverter.ContainsOnlyHexNibbles(_uid));
            Assert.True(HexConverter.ContainsOnlyHexNibbles(hexValid));
            Assert.True(HexConverter.ContainsOnlyHexNibbles(hexValid2));
            Assert.True(HexConverter.ContainsOnlyHexNibbles(_uid + hexValid));

            Assert.False(HexConverter.ContainsOnlyHexNibbles(hexInvalid));
            Assert.False(HexConverter.ContainsOnlyHexNibbles(hexInvalid2));
            Assert.False(HexConverter.ContainsOnlyHexNibbles(_uid + hexInvalid));
            Assert.False(HexConverter.ContainsOnlyHexNibbles(_char_page35 + hexInvalid2));
        }

        [Fact]
        public void Test_Bitwise_validate_ConvertHexStringToByteArray_Fail()
        {
            Assert.Throws<ArgumentException>(() => HexConverter.HexToBytes(_uid_invalid));
            Assert.Throws<ArgumentException>(() => HexConverter.HexToBytes(_data_invalid));
        }

        [Fact]
        public void Test_Bitwise_validate_ConvertHexStringToByteArray_Pass()
        {
            var buffer = HexConverter.HexToBytes(_uid);
            var uid = HexConverter.BytesToHex(buffer);
            Assert.Equal(uid, _uid);
        }
    }
}
