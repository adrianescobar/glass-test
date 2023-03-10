using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GlassTest.Documents.Utils
{
    public class QueryStringTokenizer
    {
        public static IReadOnlyList<Token> Serialize(string queryString) 
        {
            if(string.IsNullOrEmpty(queryString))
            {
                throw new ArgumentException("queryString can't be null or empty");
            }

            string[] words = queryString.Split(" ");

            List<Token> tokens = new List<Token>();
            foreach (var word in words)
            {
                string cleanedWord = CleanWord(word);
                if(!string.IsNullOrEmpty(cleanedWord))
                {
                    tokens.Add(new Token(cleanedWord));
                }
            }
            return tokens;
        }

        private static string CleanWord(string word)
        {
            return Regex.Replace(word, "[^0-9A-Za-z]", "").ToLower();
        }
    }
}