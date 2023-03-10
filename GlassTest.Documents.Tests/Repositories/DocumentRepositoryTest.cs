using System;
using System.Linq;
using GlassTest.Documents.Models;
using GlassTest.Documents.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace GlassTest.Documents.Tests.Repositories
{
    public class DocumentRepositoryTest
    {
        [Fact]
        public void Search_Documents_Without_All_Matched_Keywords()
        {
            var context = CreateDocumentContextWithFewData("Not_All_Matches");
            var repository = DocumentRepository.Create(context);

            var documentsResult = repository.SearchDocuments("Father Mother, Sister.", false);

            Assert.Equal(3, documentsResult.Count);
        }

        [Fact]
        public void Search_Documents_With_All_Matched_Keywords()
        {
            var context = CreateDocumentContextWithFewData("All_Matches");
            var repository = DocumentRepository.Create(context);

            var documentsResult = repository.SearchDocuments("Father happy", true);

            Assert.Single(documentsResult);
        }

        [Fact]
        public void Search_Documents_With_Not_Matched_Query()
        {
            var context = CreateDocumentContextWithFewData("Not_Matches");
            var repository = DocumentRepository.Create(context);

            var documentsResult = repository.SearchDocuments("Uncle", true);

            Assert.Empty(documentsResult);
        }

        private DocumentDbContext CreateDocumentContextWithFewData(string name)
        {
            var context = GetDocumentContext(name);
            
            Document[] documents = new Document[] {
              new Document { Title = "My Father is happy", Content = "My Father is happy because of me. I born 10 years ago."},
              new Document { Title = "My Mother is happy", Content = "My Mother is happy because of me. I born 10 years ago."},
              new Document { Title = "My Sister is happy", Content = "My Sister is happy because of me. I born 10 years ago."},
              new Document { Title = "My Aunt is happy", Content = "My Aunt is happy because of me. I born 10 years ago."},
              new Document { Title = "My Brother is happy", Content = "My Brother is happy because of me. I born 10 years ago."},  
            };

            context.Documents.AddRange(documents);
            context.SaveChanges();

            return context;
        }   

        private DocumentDbContext GetDocumentContext(string name = "test")
        {
            var options = new DbContextOptionsBuilder<DocumentDbContext>()
                            .UseInMemoryDatabase(name)
                            .EnableDetailedErrors()
                            .Options;

            return new DocumentDbContext(options);
        }

    }
}