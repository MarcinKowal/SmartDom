using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SmartDom.Client.UnitTests
{
    [TestFixture]
    public class ClientFactoryTests
    {
        private IClientFactory classUnderTests;
        private IFixture fixture;
        private string serverUri;
        private ClientCredentials credentials;

        [SetUp]
        public void Init()
        {
            fixture = new Fixture();
            credentials = fixture.Create<ClientCredentials>();
            serverUri = fixture.Create<Uri>().ToString();
            classUnderTests = fixture.Create<ClientFactory>();
        }

        [Test]
        public void ShallCreateXmlClient()
        {
            var client = classUnderTests.Create(serverUri, ClientFormat.Xml, credentials);
            Assert.IsNotNull(client);
        }

        [Test]
        public void ShallCreateJsonClient()
        {
            var client = classUnderTests.Create(serverUri, ClientFormat.Json, credentials);
            Assert.IsNotNull(client);
        }
    }
}
