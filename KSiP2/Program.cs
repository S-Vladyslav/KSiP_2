using System;
using System.Collections.Generic;

namespace KSiP2
{
    class Program
    {
        static string Alphabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцчшщьюя"; //your alphabet

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var rawText = Text.RawText;
            
            var key = "Жидачів";

            var encryptedText = Encryption(rawText, key);

            Console.WriteLine(encryptedText + "\n\n");
            Console.WriteLine(Decryption(encryptedText, key));

        }

        static string Encryption(string rawText, string key)
        {
            var encryptedText = "";
            var keyText = "";

            while (keyText.Length < rawText.Length)
            {
                foreach (var symb in key)
                {
                    keyText += symb;
                    if (keyText.Length >= rawText.Length) break;
                }
            }

            for (int i = 0; i < rawText.Length; i++)
            {
                if (Alphabet.IndexOf(rawText[i]) != -1)
                {
                    encryptedText += Alphabet[(Alphabet.IndexOf(rawText[i]) + Alphabet.IndexOf(keyText[i])) % Alphabet.Length];
                }
                else
                {
                    encryptedText += rawText[i];
                }
            }

            return encryptedText;
        }

        static string Decryption(string encryptedText, string key)
        {
            var decryptedText = "";
            var keyText = "";

            while (keyText.Length < encryptedText.Length)
            {
                foreach (var symb in key)
                {
                    keyText += symb;
                    if (keyText.Length >= encryptedText.Length) break;
                }
            }

            for (int i = 0; i < encryptedText.Length; i++)
            {
                if (Alphabet.IndexOf(encryptedText[i]) != -1)
                {
                    if ((Alphabet.IndexOf(encryptedText[i]) - Alphabet.IndexOf(keyText[i])) >= 0)
                    {
                        decryptedText += Alphabet[Alphabet.IndexOf(encryptedText[i]) - Alphabet.IndexOf(keyText[i])];
                    }
                    else
                    {
                        decryptedText += Alphabet[(Alphabet.IndexOf(encryptedText[i]) - Alphabet.IndexOf(keyText[i])) + Alphabet.Length];
                    }
                }
                else
                {
                    decryptedText += encryptedText[i];
                }
            }

            return decryptedText;
        }


        #region Kasiski
        static List<int> Kasiski(string encryptedText)
        {
            var setsDictionary = new Dictionary<string, List<int>>();
            var indexOfFinding = new List<int>();
            var setFromText = "";



            return indexOfFinding;
        }
        #endregion
    }
}
