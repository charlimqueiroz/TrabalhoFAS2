using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CadastroAluno.Infra
{
    /// <summary>
    /// Classe responsável por criptografar e decriptografar
    /// </summary>
    public static class Security
    {
        private static string encryptionKey = "@@CADASTROALUNO@@";


        public static string EncodeTo64(string encode)
        {
            byte[] toEncodeAsBytes = System.Text.UTF8Encoding.UTF8.GetBytes(encode);
            return System.Convert.ToBase64String(toEncodeAsBytes);
        }

        // Methods
        public static string decrypt(string textToDecrypt)
        {
            textToDecrypt = textToDecrypt.Replace("_", "/");
            textToDecrypt = textToDecrypt.Replace("-", "+");
            byte[] iv = new byte[] { 0x79, 0xf1, 10, 1, 0x84, 0x4a, 11, 0x27, 0xff, 0x5b, 0x2d, 0x4e, 14, 0xd3, 0x16, 0x3e };
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            byte[] byteTextToDecrypt = Convert.FromBase64String(textToDecrypt);
            if (Strings.Len(Security.encryptionKey) >= 0x20)
            {
                encryptionKey = Strings.Left(encryptionKey, 0x20);
            }
            else
            {
                int encryptionKeyLength = Strings.Len(encryptionKey);
                int missingEncryptionKeyLength = 0x20 - encryptionKeyLength;
                encryptionKey = encryptionKey + Strings.StrDup(missingEncryptionKeyLength, "X");
            }
            byte[] byteEncryptionKey = Encoding.ASCII.GetBytes(encryptionKey.ToCharArray());
            byte[] temp = new byte[byteTextToDecrypt.Length + 1];
            MemoryStream memoryStream = new MemoryStream(byteTextToDecrypt);
            try
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(byteEncryptionKey, iv), CryptoStreamMode.Read);
                cryptoStream.Read(temp, 0, temp.Length);
                cryptoStream.FlushFinalBlock();
                memoryStream.Close();
                cryptoStream.Close();
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            return Encoding.ASCII.GetString(temp).Replace("\0", string.Empty);
        }

        public static string encrypt(string textToEncrypt)
        {
            if (!string.IsNullOrEmpty(textToEncrypt))
            {
                byte[] coded = null;
                byte[] iv = new byte[] { 0x79, 0xf1, 10, 1, 0x84, 0x4a, 11, 0x27, 0xff, 0x5b, 0x2d, 0x4e, 14, 0xd3, 0x16, 0x3e };
                MemoryStream memoryStream = new MemoryStream();
                textToEncrypt = textToEncrypt.Replace("\0", string.Empty);
                byte[] value = Encoding.ASCII.GetBytes(textToEncrypt.ToCharArray());
                if (Strings.Len(encryptionKey) >= 0x20)
                {
                    encryptionKey = Strings.Left(encryptionKey, 0x20);
                }
                else
                {
                    int encryptionKeyLength = Strings.Len(encryptionKey);
                    int missingEncryptionKeyLength = 0x20 - encryptionKeyLength;
                    encryptionKey = encryptionKey + Strings.StrDup(missingEncryptionKeyLength, "X");
                }
                byte[] key = Encoding.ASCII.GetBytes(encryptionKey.ToCharArray());
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                try
                {
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                    cryptoStream.Write(value, 0, value.Length);
                    cryptoStream.FlushFinalBlock();
                    coded = memoryStream.ToArray();
                    memoryStream.Close();
                    cryptoStream.Close();
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
                return Convert.ToBase64String(coded).Replace("+", "-").Replace("/", "_");
            }
            return string.Empty;
        }
    }
}
