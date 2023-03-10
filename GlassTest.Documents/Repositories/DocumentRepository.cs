using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GlassTest.Documents.Models;
using GlassTest.Documents.Utils;
using Microsoft.EntityFrameworkCore;

namespace GlassTest.Documents.Repositories
{
    public class DocumentRepository
    {
        private DocumentDbContext _context;

        private DocumentRepository(DocumentDbContext context)
        {
            _context = context;
        }

        public static DocumentRepository Create(string connectionString)
        {
            return Create(new DbContextOptionsBuilder<DocumentDbContext>().UseSqlServer(connectionString).Options);
        }

        public static DocumentRepository Create(DbContextOptions<DocumentDbContext> options)
        {
            return Create(new DocumentDbContext(options));
        }

        public static DocumentRepository Create(DocumentDbContext context)
        {
            return new DocumentRepository(context);
        }

        public List<Document> SearchDocuments(string query, bool matchAll) 
        {
            var tokens = QueryStringTokenizer.Serialize(query);

            if(tokens.Count == 0)
            {
                throw new ArgumentException("Query hasn't valid words");
            }

            return _context.Documents.Where(CreateExpressionFromTokens(tokens, matchAll)).Distinct().ToList();
        }

        private Expression<Func<Document, bool>> CreateExpressionFromTokens(IReadOnlyList<Token> tokens, bool matchAll)
        {
            ParameterExpression docParameter = Expression.Parameter(typeof(Document), "doc");
            List<Expression> expressions = CreateExpressionListFromTokens(tokens, docParameter);
            Expression finalExpression = CreateExpressionFromExpressionListBasedOnMatchAll(matchAll, expressions);
            return Expression.Lambda<Func<Document, bool>>(finalExpression, docParameter);
        }

        private static List<Expression> CreateExpressionListFromTokens(IReadOnlyList<Token> tokens, ParameterExpression docParameter)
        {
            List<Expression> expressions = new();
            MemberExpression contentProperty = Expression.PropertyOrField(docParameter, "Content");

            foreach (var token in tokens)
            {
                ConstantExpression valueExpression = Expression.Constant(token.Value, typeof(string));
                MethodInfo toLower = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                MethodInfo contains = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                var callToLower = Expression.Call(contentProperty, toLower);
                var call = Expression.Call(callToLower, contains, valueExpression);
                expressions.Add(call);
            }

            return expressions;
        }

        private static Expression CreateExpressionFromExpressionListBasedOnMatchAll(bool matchAll, List<Expression> expressions)
        {
            Expression finalExpression = null;

            foreach (var expression in expressions)
            {
                if (finalExpression == null)
                {
                    finalExpression = expression;
                }
                else
                {
                    finalExpression = matchAll ? Expression.AndAlso(finalExpression, expression) : Expression.OrElse(finalExpression, expression);
                }
            }

            return finalExpression;
        }
    }
}