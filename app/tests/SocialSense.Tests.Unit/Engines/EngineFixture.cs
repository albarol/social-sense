namespace SocialSense.Tests.Unit.Engines
{
    using System;
    using System.Collections.Generic;

    using Moq;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Shared;
    using SocialSense.Spiders;
    using SocialSense.UrlBuilders;

    [TestFixture, Category("Engines")]
    public class EngineFixture
    {
        private Engine engine;
        private Mock<IEngineConfiguration> configuration;
        private Mock<IParser> parser;
        private Mock<IUrlBuilder> urlBuilder;
        private Mock<Spider> spider;

        [SetUp]
        public void SetUp()
        {
            this.configuration = new Mock<IEngineConfiguration>();
            this.parser = new Mock<IParser>();
            this.urlBuilder = new Mock<IUrlBuilder>();
            this.spider = new Mock<Spider>();


            this.configuration.Setup(c => c.Parser).Returns(this.parser.Object);
            this.configuration.Setup(c => c.UrlBuilder).Returns(this.urlBuilder.Object);
            this.configuration.Setup(c => c.Spider).Returns(this.spider.Object);
            this.engine = new Engine(this.configuration.Object);
        }

        [Test]
        public void Search_GetUrlFromUrlBuilder()
        {
            // Arrange:
            var query = new Query { Term = "cultura", MaxResults = 10 };


            // Act:
            this.engine.Search(query);
            
            // Assert:
            this.urlBuilder.Verify(u => u.WithQuery(It.IsAny<Query>()), Times.Once());
        }

        [Test]
        public void Search_GetUrlInNextPage()
        {
            // Arrange:
            var query = new Query { Term = "cultura", MaxResults = 20 };
            this.urlBuilder.Setup(u => u.WithQuery(It.IsAny<Query>())).Returns(string.Empty);
            this.spider.Setup(s => s.DownloadContent(It.IsAny<string>())).Returns(string.Empty);
            this.parser.Setup(p => p.Parse(It.IsAny<string>())).Returns(new SearchResult
            {
                HasNextPage = true, 
                Items = new List<ResultItem>
                {
                    new ResultItem(),        
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem()
                }
            });

            // Act:
            this.engine.Search(query);

            // Assert:
            this.urlBuilder.Verify(u => u.WithQuery(It.IsAny<Query>()), Times.Exactly(4));
        }

        [Test]
        public void Search_InvokeParserWhenContentExists()
        {
            // Arrange:
            this.urlBuilder.Setup(u => u.WithQuery(It.IsAny<Query>())).Returns(string.Empty);
            this.spider.Setup(s => s.DownloadContent(It.IsAny<string>())).Returns(string.Empty);

            // Act:
            this.engine.Search(new Query());
            
            // Assert:
            this.parser.Verify(p => p.Parse(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Search_InvokeParserInNextPage()
        {
            // Arrange:
            var query = new Query { Term = "cultura", MaxResults = 20 };
            this.urlBuilder.Setup(u => u.WithQuery(It.IsAny<Query>())).Returns(string.Empty);
            this.spider.Setup(s => s.DownloadContent(It.IsAny<string>())).Returns(string.Empty);
            this.parser.Setup(p => p.Parse(It.IsAny<string>())).Returns(new SearchResult
            {
                HasNextPage = true,
                Items = new List<ResultItem>
                {
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem()
                }
            });

            // Act:
            this.engine.Search(query);

            // Assert:
            this.parser.Verify(u => u.Parse(It.IsAny<string>()), Times.Exactly(4));
        }

        [Test]
        public void Search_InvokeSpider()
        {
            // Arrange:
            this.urlBuilder.Setup(u => u.WithQuery(It.IsAny<Query>())).Returns(string.Empty);

            // Act:
            this.engine.Search(new Query());

            // Assert:
            this.spider.Verify(p => p.DownloadContent(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Search_ReturnCorrectNumberOfResults()
        {
            // Arrange:
            var query = new Query { Term = "cultura", MaxResults = 7 };
            this.urlBuilder.Setup(u => u.WithQuery(It.IsAny<Query>())).Returns(string.Empty);
            this.spider.Setup(s => s.DownloadContent(It.IsAny<string>())).Returns(string.Empty);
            this.parser.Setup(p => p.Parse(It.IsAny<string>())).Returns(new SearchResult
            {
                HasNextPage = true,
                Items = new List<ResultItem>
                {
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem()
                }
            });

            // Act:
            var results = this.engine.Search(query);

            // Assert:
            results.Count.Should().Be(7);
        }

        [Test]
        public void Search_ReturnCurrentResultsWhenThrowsException()
        {
            // Arrange:
            var count = 0;
            this.urlBuilder.Setup(u => u.WithQuery(It.IsAny<Query>())).Returns(string.Empty).Callback(() =>
            {
                count++; 
                if (count == 2)
                {
                    throw new Exception();
                }
            });
            this.spider.Setup(s => s.DownloadContent(It.IsAny<string>())).Returns(string.Empty);
            this.parser.Setup(p => p.Parse(It.IsAny<string>())).Returns(new SearchResult
            {
                HasNextPage = true,
                Items = new List<ResultItem>
                {
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem(),
                    new ResultItem()
                }
            });

            // Act:
            var results = this.engine.Search(new Query());

            // Assert:
            results.Count.Should().Be(5);
        }
    }
}
