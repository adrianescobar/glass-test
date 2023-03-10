using System;
using System.Collections.Generic;
using GlassTest.Documents.Utils;
using Xunit;

namespace GlassTest.Documents.Tests.Utils
{
    public class QueryStringTokenizerTest
    {
        [Fact]
        public void Serialize_Simple_QueryString()
        {
            string queryString = "Mother Father Some";
            IReadOnlyList<Token> expectedResult = new List<Token> { new Token("mother"), new Token("father"), new Token("some") };

            IReadOnlyList<Token> result = QueryStringTokenizer.Serialize(queryString);

            Assert.Equal(expectedResult.Count, result.Count);
            Assert.Collection(result, 
                        item => item.Equals(expectedResult[0]),
                        item => item.Equals(expectedResult[1]),
                        item => item.Equals(expectedResult[2]));
        }

        [Fact]
        public void Serialize_QueryString_With_Special_Characters()
        {
            string queryString = "Mother@ Father.  -Some! @ -";
            IReadOnlyList<Token> expectedResult = new List<Token> { new Token("mother"), new Token("father"), new Token("some") };

            IReadOnlyList<Token> result = QueryStringTokenizer.Serialize(queryString);

            Assert.Equal(expectedResult.Count, result.Count);
            Assert.Collection(result, 
                        item => item.Equals(expectedResult[0]),
                        item => item.Equals(expectedResult[1]),
                        item => item.Equals(expectedResult[2]));
        }

        [Fact]
        public void Serialize_Empty_QueryString_Fails()
        {
            string queryString = string.Empty;
            Assert.Throws<ArgumentException>(() => QueryStringTokenizer.Serialize(queryString));
        }
    }
}