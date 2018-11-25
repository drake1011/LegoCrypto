using LegoCrypto.Data.Model;

namespace LegoCrypto.IO
{
    public interface INfcDevice
    {
        /// <summary>
        /// Check if device exists with correct software
        /// </summary>
        /// <returns>bool</returns>
        bool CheckDevice();

        /// <summary>
        /// Read Ntag and return ITag
        /// </summary>
        /// <returns>ITag</returns>
        ITag ReadNtag();

        /// <summary>
        /// Stop currently executing command
        /// </summary>
        void CancelCommand();
    }

    /// <summary>
    /// Supported NFC command types
    /// </summary>
    public enum NfcCommand
    {
        CheckDevice,
        ReadNtag
    }
}
