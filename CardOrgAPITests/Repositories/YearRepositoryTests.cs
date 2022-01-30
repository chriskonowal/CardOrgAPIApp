using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPITests.Repositories
{
    [TestClass]
    public class YearRepositoryTests
    {
        [TestMethod]
        public void TestGetYears()
        {
            //Arrange
            var years = CreateYears();

            var yearRepository = new Mock<IYearRepository>();
            yearRepository.Setup(x => x.GetYearsAsync(1, CancellationToken.None)).Returns(Task.FromResult(years));

            //Act
            var getYearsResults =  yearRepository.Object.GetYearsAsync(1, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();

            //Assert
            getYearsResults.Should().NotBeNull();
            getYearsResults.Count().Should().Be(1);
            getYearsResults.First().YearId.Should().Be(2);
        }

        public IEnumerable<Year> CreateYears()
        { 
            return new List<Year>() {
                new Year() {
                YearId= 2,
                BeginningYear= 2002,
                EndingYear= 2003,
                Cards= new Card [] { }
                }
            };
        }
    }
}
