using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treningi.Core;
using Treningi.Core.Repositories;
using Treningi.Infrastructure.Commands;
using Treningi.Infrastructure.Services;
using Assert = NUnit.Framework.Assert;

namespace Treningi.UnitTests
{
    [TestClass()]
    public class Tests
    {
        protected Mock<ICompetitorsRepository> _competitorsRepositoryMock = new Mock<ICompetitorsRepository>();
        protected List<Competitor> _competitorsListMock;
        protected CompetitorService _competitorService;

        [TestInitialize()]
        public void TestInitialize()
        {
            _competitorsListMock = new List<Competitor>();
            _competitorService = new CompetitorService(_competitorsRepositoryMock.Object);
        }

        [Test]
        public async Task AddNewCompetitor_ListSizeShouldEqualOne()
        {
            TestInitialize();
            int expectedSize = 1;
            var competitor = new CreateCompetitor()
            {
                Forename = "A",
                Surname = "B",
                Height = 100,
                Weight = 100
            };
            _competitorsRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Competitor>())).Callback<Competitor>((s) => _competitorsListMock.Add(s));

            await _competitorService.Add(competitor);

            Assert.AreEqual(expectedSize, _competitorsListMock.Count());
        }

        [Test]
        public async Task AddNewCompetitor_ListSizeShouldIncrease()
        {
            TestInitialize();
            var competitor = new CreateCompetitor()
            {
                Forename = "A",
                Surname = "B",
                Height = 100,
                Weight = 100
            };
            int sizeBeforeAdding;
            int sizeAfterAdding;
            _competitorsRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Competitor>())).Callback<Competitor>((s) => _competitorsListMock.Add(s));

            _competitorsListMock.Add(new Competitor());
            sizeBeforeAdding = _competitorsListMock.Count();
            await _competitorService.Add(competitor);
            sizeAfterAdding = _competitorsListMock.Count();

            Assert.AreEqual(sizeBeforeAdding, sizeAfterAdding - 1);
        }

        [Test]
        public async Task UpdateCompetitor_StandardCase_DataShouldChange()
        {
            TestInitialize();
            _competitorsRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Competitor>())).Verifiable();
            UpdateCompetitor u = new UpdateCompetitor(10, "A", "B", 100, 100, "20", new System.DateTime(), "30");

            await _competitorService.Update(u);

            _competitorsRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Competitor>()), Times.Once());
        }

        [Test]
        public async Task DeleteCompetitor_StandardCase_ShouldNoLongerExistInList()
        {
            TestInitialize();
            int idToDelete = 1;
            _competitorsRepositoryMock.Setup(x => x.DelAsync(It.IsAny<int>())).Verifiable();

            await _competitorService.Delete(idToDelete);

            _competitorsRepositoryMock.Verify(x => x.DelAsync(It.IsAny<int>()), Times.Once());
        }
        [Test]
        public async Task GetCompetitor_StandardCase_ShouldCallGetOnce()
        {
            TestInitialize();
            int competitorId = 10;
            _competitorsRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(new Competitor()
            {
                ID = competitorId,
                Forename = "Test"
            }));

            await _competitorService.Get(competitorId);

            _competitorsRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once());
        }

    }
}