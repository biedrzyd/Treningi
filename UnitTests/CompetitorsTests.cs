using NUnit.Framework;
using Moq;
using Treningi.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Treningi.Core;
using System.Collections.Generic;
using Treningi.Infrastructure.Services;
using Treningi.Infrastructure.Commands;
using System.Linq;
using Assert = NUnit.Framework.Assert;
using System.Threading.Tasks;

namespace Treningi
{
    [TestClass]
    public class Tests
    {
        protected Mock<ICompetitorsRepository> _competitorsRepository = new Mock<ICompetitorsRepository>();
        protected List<Competitor> _competitorsListMock = new List<Competitor>();
        protected CompetitorService _competitorService;

        [TestInitialize]
        public void TestInitialize()
        {
            _competitorService = new CompetitorService(_competitorsRepository.Object);
        }

        [TestMethod]
        public async Task AddAsync_StandardCase_CompetitorRepositorySizeShouldIncreaseBy1()
        {
            int expectedSize = 1;
            var competitor = new CreateCompetitor()
            {
                CoachId = "1",
                Height = 200,
                Country = "PL" 
            };
            _competitorsRepository.Setup(x => x.AddAsync(It.IsAny<Competitor>())).Callback<Competitor>((c) => _competitorsListMock.Add(c));

            await _competitorService.Add(competitor);

            Assert.AreEqual(2, _competitorsListMock.Count());
        }
    }
}