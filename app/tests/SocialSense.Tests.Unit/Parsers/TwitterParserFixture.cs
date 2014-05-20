﻿namespace SocialSense.Tests.Unit.Parsers
{
    using System;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Parsers;
    using SocialSense.Tests.Unit.Helpers;

    [TestFixture, Category("Parsing")]
    public class TwitterParserFixture
    {
        private IParser parser;

        [SetUp]
        public void SetUp()
        {
            this.parser = new TwitterParser();
        }
        
        [Test]
        public void Parse_ResultShouldNotBeEmpty()
        {
            var results = this.parser.Parse(IoHelper.ReadContent("Parsers/twitter-result-v1.json"));
            results.Items.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Parse_ThrowsExceptionWhenArgumentIsNull()
        {
            this.parser.Parse(null);
        }

        [Test]
        public void Parse_ResultShouldBeEmptyWhenTokenIsInvalid()
        {
            var result = this.parser.Parse("{invalid_token:'invalid_token'}");
            result.Items.Count.Should().Be(0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Parse_ThrowsExceptionWhenIsEmpty()
        {
            this.parser.Parse(string.Empty);
        }
    }
}
