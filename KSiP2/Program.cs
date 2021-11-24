using System;
using System.Collections.Generic;
using System.Linq;

namespace KSiP2
{
    class Program
    {
        static string Alphabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцчшщьюя"; //your alphabet

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var rawText = Text.RawText;
            
            var key = "Жидачівщина";

            //var encryptedText = Encryption(rawText, key);

            //Console.WriteLine(encryptedText + "\n\n");
           // Console.WriteLine(Decryption(encryptedText, key));
            Console.WriteLine(Kasiski(rawText));

        }

        #region Encryption
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
        #endregion

        #region Decryption
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
        #endregion

        #region Kasiski
        static string Kasiski(string startText)
        {
            var setsDictionary = new Dictionary<string, List<int>>();
            
            var setFromText = "";
            var set = "";
            var encryptedText = "";

            foreach (var symbol in startText)
            {
                if (Alphabet.IndexOf(symbol) != -1)
                {
                    encryptedText += symbol;
                }
            }

            for (int i = 2; i < encryptedText.Length / 2; i++)
            {
                for (int j = 0; j < encryptedText.Length-i+1; j++)
                {
                    set = encryptedText.Substring(j, i);
                    //Console.WriteLine($"{i} : {j} : {set}");

                    if (setsDictionary.ContainsKey(set))
                    {
                        setsDictionary[set].Add(j);
                        //Console.WriteLine("Added");
                    }
                    else
                    {
                        var indexOfFinding = new List<int>() { j };
                        setsDictionary.Add(set, indexOfFinding);
                        //Console.WriteLine("Added new");
                    }
                }
            }

            foreach (var item in setsDictionary)
            {
                if (item.Value.Count > 1) Console.WriteLine($"{item.Key} : {item.Value.Count}");
            }

            #region Linq
            var mostCommon = setsDictionary.OrderByDescending(el => el.Value.Count > 2);

            Console.WriteLine("");
            //return mostCommon.ToString();
            #endregion
            return "";
        }
        #endregion
    }
}
