using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleViewer.SharedObjects;
using PersonRepository.Interface;

namespace PeopleViewer.Presentation.Test
{
    [TestClass]
    public class UnitTest1
    {
        IPersonRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var people = new List<Person>()
            {
                new Person(){FirstName = "John", LastName="Smith", Rating=7, StartDate=DateTime.Parse("10/1/2000")},
                new Person(){FirstName = "Mary", LastName="Thomas", Rating=9, StartDate=DateTime.Parse("07/23/1971")}
            };

            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(r => r.GetPeople()).Returns(people);
            _repository = repoMock.Object;

        }

        [TestMethod]
        public void People_OnRefreshCommand_IsPopulated()
        {

           
            // Arrange
            var vm = new PeopleViewerViewModel(_repository);

            // Act
            vm.RefreshPeopleCommand.Execute(null);

            // Assert
            Assert.IsNotNull(vm.People);
            Assert.AreEqual(2, vm.People.Count());

        }

        [TestMethod]
        public void People_OnClearCommand_IsEmpty()
        {
            // Arrange
            var vm = new PeopleViewerViewModel(_repository);
            vm.RefreshPeopleCommand.Execute(null);
            Assert.AreEqual(2, vm.People.Count(), "Invalid Arrangement");

            // Act
            vm.ClearPeopleCommand.Execute(null);

            // Assert
            Assert.AreEqual(0, vm.People.Count());

        }

    }
}
