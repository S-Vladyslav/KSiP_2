using System;
using System.Collections.Generic;
using System.Linq;

namespace KSiP2
{
    class Program
    {
        static readonly string Alphabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";//абвгґдеєжзиіїйклмнопрстуфхцчшщьюя"; //your alphabet

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var rawText = Text.RawText.ToUpper();
            
            var key = "АБВГҐ";

            var encryptedText = Encryption(rawText, key);

            Console.WriteLine(encryptedText + "\n\n");
            Console.WriteLine(Decryption(encryptedText, key));

            var result = Kasiski(encryptedText);

            Console.WriteLine("\n\nНайймовірніші довжини ключів:");
            foreach (var i in result)
            {
                if (i > 1) Console.WriteLine(i);
            }

            Console.WriteLine($"Довжина справжнього ключа: {key.Length}");
            Console.WriteLine($"Довжина тексту: {encryptedText.Length}");
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
        static List<int> Kasiski(string startText)
        {
            var setsDictionary = new Dictionary<string, List<int>>();

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

            var mostCommon = setsDictionary.Where(el => el.Value.Count > 2);

            //foreach (var i in mostCommon)
            //{
            //    Console.WriteLine(i.Value.Count);
            //}

            var distList = new List<List<int>>();

            foreach (var coordinates in mostCommon)
            {
                List<int> coord = coordinates.Value.ToList();

                var differense = new List<int>();

                for (int i = 1; i < coord.Count; i++)
                {
                    differense.Add(coord[i] - coord[i - 1]);
                }

                distList.Add(differense);
            }

            var GCD = new List<int>();

            foreach (var j in distList)
            {
                //var listGCD = j[0];
                for (int i = 1; i < j.Count; i++)
                {
                    Console.WriteLine($"i = {j[i]}\t i-1 = {j[i - 1]}");
                    //listGCD = GCDMethod(listGCD, j[i]);
                    GCD.Add(GCDMethod(j[i-1], j[i]));
                }
                //GCD.Add(listGCD);
            }

            return GCD.Distinct().ToList();
        }
        #endregion

        #region GCDMethod
        static int GCDMethod(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                var min = Min(a, b);
                var max = Max(a, b);
                //викликаємо метод з новими аргументами
                return GCDMethod(max - min, min);
            }
        }
        static int Min(int x, int y)
        {
            return x < y ? x : y;
        }

        static int Max(int x, int y)
        {
            return x > y ? x : y;
        }
        #endregion
    }
}
