namespace SocialSense.Tests.Unit.Parsers
{
    using System;

    using NUnit.Framework;

    using SharpTestsEx;

    using SocialSense.Parsers;
    using SocialSense.Tests.Unit.Helpers;

    [TestFixture, Category("Parsing")]
    public class FacebookParserFixture
    {
        private IParser parser;

        [SetUp]
        public void SetUp()
        {
            this.parser = new FacebookParser();
        }

        [Test]
        public void Parse_ResultShouldNotBeEmpty ()
        {
            var results = this.parser.Parse (IoHelper.ReadContent ("Parsers/facebook-result-v1.json"));
            results.Items.Count.Should().Be.GreaterThan(0);
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
            result.Items.Count.Should().Be.EqualTo(0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Parse_ThrowsExceptionWhenArgumentIsEmpty()
        {
            this.parser.Parse(string.Empty);
        }
    }
}
