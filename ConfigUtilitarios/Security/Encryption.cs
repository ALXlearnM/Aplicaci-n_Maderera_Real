using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ConfigUtilitarios
{
    public class Encription
    {

        public string Encryption(string strPassword)
        {
            try
            {
                if (strPassword != null)
                {
                    string strEncryption = string.Empty;
                    string sPword = "SysCorpEagle1987";

                    byte[] bKey;
                    byte[] bText = Encoding.UTF8.GetBytes(strPassword);

                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    bKey = md5.ComputeHash(Encoding.UTF8.GetBytes(sPword));
                    md5.Clear();

                    TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
                    tDes.Key = bKey;
                    tDes.Mode = CipherMode.ECB;
                    tDes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform icEncriptor = tDes.CreateEncryptor();
                    byte[] bResult = icEncriptor.TransformFinalBlock(bText, 0, bText.Length);
                    tDes.Clear();

                    return strEncryption = Convert.ToBase64String(bResult, 0, bResult.Length);
                }
                else return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Decryption(string strPassword)
        {

            if (string.IsNullOrEmpty(strPassword) || strPassword.Trim().Length == 0)
            {
                return "";
            }
            else
            {
                try
                {
                    string strDecryption = string.Empty;
                    string sPword = "SysCorpEagle1987";
                    byte[] bKey;
                    byte[] bText = Convert.FromBase64String(strPassword);

                    // Ciframos utilizando el Algoritmo MD5.

                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    bKey = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(sPword));
                    md5.Clear();

                    //Ciframos utilizando el Algoritmo 3DES.

                    TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
                    tDes.Key = bKey;
                    tDes.Mode = CipherMode.ECB;
                    tDes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform icEncriptor = tDes.CreateDecryptor();
                    byte[] bResult = icEncriptor.TransformFinalBlock(bText, 0, bText.Length);
                    tDes.Clear();
                    return strDecryption = UTF8Encoding.UTF8.GetString(bResult);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

        }

    }
}