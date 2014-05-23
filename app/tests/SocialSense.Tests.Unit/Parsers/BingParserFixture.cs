﻿namespace SocialSense.Tests.Unit.Parsers
{
    using System;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Parsers;
    using SocialSense.Tests.Unit.Helpers;

    [TestFixture, Category("Parsing")]
    public class BingParserFixture
    {
        private IParser parser;

        [SetUp]
        public void SetUp()
        {
            this.parser = new BingParser();
        }

        [Test]
        public void Parse_ResultShouldNotBeEmpty()
        {
            var results = this.parser.Parse(IoHelper.ReadContentWithDecode("Parsers/BingResult-v1.htm"));
            results.Items.Count.Should().BeGreaterThan(0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Parse_ThrowsExceptionWhenArgumentIsNull()
        {
            this.parser.Parse(null);
        }

        [Test]
        public void Parse_ResultShouldBeEmptyWhenTokenIsInvalid()
        {
            var result = this.parser.Parse("<!doctype><html><body></body></html>");
			result.Items.Count.Should().Be(0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Parse_ThrowsExceptionWhenArgumentIsEmpty()
        {
            this.parser.Parse(string.Empty);
        }
    }
}
