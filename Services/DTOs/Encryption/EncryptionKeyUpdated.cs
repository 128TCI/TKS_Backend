using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Services.DTOs.Encryption
{
    public class EncryptionKeyUpdated
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public int KeySize { get; set; }
        public byte[] Key { get; set; } = Array.Empty<byte>();
        public byte[] IV { get; set; } = Array.Empty<byte>();
        public bool WithEncryption { get; set; }
    }
}
