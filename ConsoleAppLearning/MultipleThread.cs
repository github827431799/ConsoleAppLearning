using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppLearning
{
    class MultipleThread
    {
        static string pathFile = @"C:\Users\mcao3\source\repos\ConsoleAppLearning\Data\Pride and Prejudice.txt";
        static string pathFileResult = @"C:\Users\mcao3\source\repos\ConsoleAppLearning\Data\Result.txt";
        static Task<string> task = ReadCharacters(pathFile);
        static int characterCount;
        static Object obj = new Object();
        static char[] separators = {' '};

        //Process words in parallel
        public static void ProcessWordParallel()
        {
            var result = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            Parallel.ForEach(
                File.ReadLines(pathFile),
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                //Func with no parameter and return value
                () => new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase),
                //Func with four parameters and return value
                (line, state, index, localDic) =>
                {
                    foreach (var rawWord in line.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string word = ValidWord(rawWord);
                        //if (!IsValidWord(word)) { continue; }
                        if (localDic.ContainsKey(word))
                        {
                            localDic[word] = localDic[word] + 1;
                        }
                        else
                        {
                            localDic.Add(word, 1);
                        }
                        //Console.WriteLine(state);
                        //Console.WriteLine(index);
                    }
                    return localDic;
                },
                //Action with one parameter and no return value
                localDic =>
                {   //Lock result for atomic operation
                    lock (result)
                    {
                        foreach (var pair in localDic)
                        {
                            var key = pair.Key;
                            if (result.ContainsKey(key))
                            {
                                result[key] += pair.Value;
                            }
                            else
                            {
                                result[key] = pair.Value;
                            }
                        }
                    }
                }
            );

            //Populate result
            using (StreamWriter sw = new StreamWriter(pathFileResult))
            {
                foreach (var pair in result)
                {
                    //Console.WriteLine(pair.Key + ": " + pair.Value);
                    sw.WriteLine(pair.Key + ": " + pair.Value);
                }
            }

            Console.WriteLine("Process completed");

        }

        private static string ValidWord(string word)
        {
            word = word.Replace("‘", "");
            word = word.Replace("’", "");
            word = word.Replace(",", "");
            word = word.Replace(".", "");
            word = word.Replace("?", "");
            word = word.Replace(";", "");
            word = word.Replace(":", "");
            word = word.Replace("'", "");
            word = word.Replace("!", "");
            word = word.Replace("(", "");
            word = word.Replace(")", "");
            return word;
        }

        //Process every character in parallel using multiple threads 
        public static void ProcessCharacterParallel()
        {

            string text = task.Result;
            ParallelLoopResult result = Parallel.ForEach(text,
                (ch) =>
                {
                    if (!Char.IsWhiteSpace(ch))
                    {
                        lock (obj)
                        {
                            characterCount++;
                        }
                    }
                });
            Console.WriteLine("Total characters:" + text.Length);
            Console.WriteLine("Non-white-space characters:" + characterCount);
        }

        //Read file using stream 
        private static async Task<string> ReadCharacters(string fn)
        {
            string text;
            using (StreamReader sr = new StreamReader(fn))//StreamReader is not thread-secure class
            {
                text = await sr.ReadToEndAsync();
                return text;
            }

        }


    }
}
