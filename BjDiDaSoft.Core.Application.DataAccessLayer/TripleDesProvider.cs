using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer
{
    public class TripleDesProvider
    {
        #region " Declaración de variables "

        string keyClave = "";
        string keyVector = "";

        private Hashtable htKV;

        #endregion

        #region " Constructor de clase "

        public TripleDesProvider()
        {
            /*
            htKV = new Hashtable();
            htKV = ReadFileKV(ref sTextoError);
            if (htKV.ContainsKey("KEY_IS"))
                keyClave = htKV["KEY_IS"].ToString();
            if (htKV.ContainsKey("VEC_IS"))
                keyVector = htKV["VEC_IS"].ToString();
            */

            keyClave = "AF623EC6D8923C239260F0DB887C65C8AF3A20D36AB11B28";
            keyVector = "882F24D25AB06C7E";

        }//EncryptDecrypt3DESProvider

        #endregion

        #region " Métodos públicos "

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptData"></param>
        /// <returns></returns>
        public string EncryptString3DES(string encryptData)
        {
            try
            {
                string key;
                string vector;
                int i, j;
                byte[] bKey = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                byte[] bIV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

                key = keyClave;
                vector = keyVector;
                key = key.ToUpper();
                vector = vector.ToUpper();

                j = 0;
                for (i = 0; i < key.Length; i += 2, j++)
                {
                    bKey[j] = Convert.ToByte(key.Substring(i, 2), 16);
                }
                j = 0;
                for (i = 0; i < vector.Length; i += 2, j++)
                {
                    bIV[j] = Convert.ToByte(vector.Substring(i, 2), 16);
                }
                return this.Encrypt3DES(encryptData, bKey, bIV);
            }
            catch (Exception)
            {
                return null;
            }
        }// EncryptString3DES

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decryptData"></param>
        /// <returns></returns>
        public string DecryptString3DES(string decryptData)
        {
            try
            {
                int i, j;
                string key;
                string vector;

                byte[] bKey = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                byte[] bIV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                key = keyClave;
                vector = keyVector;

                key = key.ToUpper();
                vector = vector.ToUpper();

                j = 0;
                for (i = 0; i < key.Length; i += 2, j++)
                {
                    bKey[j] = Convert.ToByte(key.Substring(i, 2), 16);
                }
                j = 0;
                for (i = 0; i < vector.Length; i += 2, j++)
                {
                    bIV[j] = Convert.ToByte(vector.Substring(i, 2), 16);
                }
                return this.Decrypt3DES(decryptData, bKey, bIV);
            }
            catch (Exception)
            {
                return null;
            }
        }// DecryptString3DES


        #endregion

        #region " Métodos privados "

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptData"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        private string Encrypt3DES(string encryptData, string key, string vector)
        {
            try
            {
                int i, j;
                byte[] bKey = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                byte[] bIV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                key = key.ToUpper();
                vector = vector.ToUpper();

                j = 0;
                for (i = 0; i < key.Length; i += 2, j++)
                {
                    bKey[j] = Convert.ToByte(key.Substring(i, 2), 16);
                }
                j = 0;
                for (i = 0; i < vector.Length; i += 2, j++)
                {
                    bIV[j] = Convert.ToByte(vector.Substring(i, 2), 16);
                }
                return this.Encrypt3DES(encryptData, bKey, bIV);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private string Encrypt3DES(string encryptData, byte[] Key, byte[] IV)
        {
            try
            {
                // Crea un objeto DES para generar una clave e inicializar el vector IV.
                //TripleDES TripleDESalg = TripleDES.Create("TripleDES");
                // Encripta la cadena en un buffer en memoria.
                byte[] data = EncryptTextToMemory3DES(encryptData, Key, IV);
                string dataEncryption = "";
                string temporal = "";
                for (int i = 0; i < data.Length; i++)
                {
                    temporal = String.Format("{0:00}", Convert.ToString(data[i], 16));
                    if (temporal.Length == 1)
                        temporal = "0" + temporal;
                    dataEncryption += temporal.ToUpper();
                }
                return dataEncryption;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="decryptData"></param>
        /// <param name="key"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        private string Decrypt3DES(string decryptData, string key, string vector)
        {
            try
            {
                int i, j;
                byte[] bKey = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                byte[] bIV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                key = key.ToUpper();
                vector = vector.ToUpper();

                j = 0;
                for (i = 0; i < key.Length; i += 2, j++)
                {
                    bKey[j] = Convert.ToByte(key.Substring(i, 2), 16);
                }
                j = 0;
                for (i = 0; i < vector.Length; i += 2, j++)
                {
                    bIV[j] = Convert.ToByte(vector.Substring(i, 2), 16);
                }
                return this.Decrypt3DES(decryptData, bKey, bIV);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decryptData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private string Decrypt3DES(string decryptData, byte[] Key, byte[] IV)
        {
            try
            {
                int len = decryptData.Length;
                byte[] data = new byte[len / 2];
                int j = 0;
                for (int i = 0; i < len; i += 2)
                {
                    string x = decryptData.Substring(i, 2);
                    data[j++] = Convert.ToByte(x, 16);
                }
                // Encripta la cadena en un buffer en memoria.
                return DecryptTextFromMemory3DES(data, Key, IV);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private string DecryptTextFromMemory3DES(byte[] data, byte[] Key, byte[] IV)
        {
            try
            {
                MemoryStream memoryDecrypt = new MemoryStream(data);
                TripleDES tripleDESalg = TripleDES.Create();
                tripleDESalg.Padding = PaddingMode.Zeros;
                CryptoStream cryptoDecrypt = new CryptoStream(memoryDecrypt, tripleDESalg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
                byte[] fromEncrypt = new byte[data.Length];
                cryptoDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                return new ASCIIEncoding().GetString(fromEncrypt);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private byte[] EncryptTextToMemory3DES(string encryptData, byte[] Key, byte[] IV)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                TripleDES TripleDESalg = TripleDES.Create();
                TripleDESalg.Padding = PaddingMode.Zeros;
                CryptoStream cryptoStream = new CryptoStream(memoryStream, TripleDESalg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                byte[] toEncrypt;
                toEncrypt = new ASCIIEncoding().GetBytes(encryptData);
                cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                byte[] ret = memoryStream.ToArray();
                cryptoStream.Close();
                memoryStream.Close();
                return ret;
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        /// <summary>
        /// Lee el archivo que contiene la clave y vector para encriptación de mensajes
        /// </summary>
        /// <param name="ErrorText"></param>
        /// <returns></returns>
        private Hashtable ReadFileKV(ref String ErrorText)
        {
            int iRows;
            string sLine;
            string sPathFile;
            Hashtable htKeyVector;
            StreamReader srFile;

            try
            {
                // Create an instance of StreamReader to read from a file.
                sPathFile = System.AppDomain.CurrentDomain.BaseDirectory;
                sPathFile = sPathFile + @"KeyVecIS.ini";

                srFile = new StreamReader(sPathFile);
                string Location = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // Read and display the lines from the file until the end 
                // of the file is reached.

                htKeyVector = new Hashtable();
                iRows = 0;
                do
                {
                    sLine = srFile.ReadLine();
                    if (iRows == 0)
                        htKeyVector.Add("KEY_IS", sLine);
                    if (iRows == 1)
                        htKeyVector.Add("VEC_IS", sLine);

                    iRows++;
                } while (sLine != null);

                srFile.Close();
                return htKeyVector;
            }
            catch (Exception e)
            {
                ErrorText = "The file could not be read: " + e.ToString();
                return null;
            }
        }// ReadFileKV

        #endregion

    }
}
