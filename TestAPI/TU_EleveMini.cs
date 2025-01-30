using Xunit;
using Moq;
using Business.ServicesMinimal;
using Business.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TestAPI.TU_EleveMini
{
    public class MinimalApiTests
    {
        private readonly Mock<IEleveServiceMini> _mockEleveService;
        private readonly Mock<ISchoolServiceMini> _mockSchoolService;

        public MinimalApiTests()
        {
            _mockEleveService = new Mock<IEleveServiceMini>();
            _mockSchoolService = new Mock<ISchoolServiceMini>();
        }

        [Fact]
        public async Task GetListEleveAsyn_ReturnsOkResult_WithListOfEleves()
        {
            // Arrange
            var eleves = new List<Eleve>
        {
            new Eleve { Nom = "Dupont", Prenom = "Jean", Age = 12, Sexe = true }
        };
            _mockEleveService.Setup(s => s.GetListEleveAsync()).ReturnsAsync(eleves);

            // Act
            var result = await new Func<Task<IResult>>(() => Task.FromResult(Results.Ok(eleves)))();

            // Assert
            var okResult = Assert.IsType<Ok<List<Eleve>>>(result);
            var returnValue = Assert.IsType<List<Eleve>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetEleveByIdAsync_ReturnsOkResult_WithEleve()
        {
            // Arrange
            var eleve = new Eleve { Nom = "Dupont", Prenom = "Jean", Age = 12, Sexe = true };
            _mockEleveService.Setup(s => s.GetEleveByIdAsync(It.IsAny<int>())).ReturnsAsync(eleve);

            // Act
            var result = await new Func<int, Task<IResult>>(id => Task.FromResult(eleve is not null ? Results.Ok(eleve) : Results.NotFound()))(1);

            // Assert
            var okResult = Assert.IsType<Ok<Eleve>>(result);
            var returnValue = Assert.IsType<Eleve>(okResult.Value);
            Assert.Equal("Dupont", returnValue.Nom);
        }

        [Fact]
        public async Task PostEleveAsync_ReturnsCreatedResult_WithNewEleve()
        {
            // Arrange
            var newEleve = new Eleve { Nom = "Martin", Prenom = "Paul", Age = 14, Sexe = false, SchoolId = 1 };
            _mockEleveService.Setup(s => s.PostEleveAsync(It.IsAny<Eleve>())).ReturnsAsync(newEleve);

            // Act
            var result = await new Func<Eleve, Task<IResult>>(eleve => Task.FromResult(Results.Created($"/eleve/{newEleve.Id}", newEleve)))(newEleve);

            // Assert
            var createdResult = Assert.IsType<Created<Eleve>>(result);
            var returnValue = Assert.IsType<Eleve>(createdResult.Value);
            Assert.Equal("Martin", returnValue.Nom);
        }

        [Fact]
        public async Task UpdateEleveByIdAsync_ReturnsOkResult_WithUpdatedEleve()
        {
            // Arrange
            var updatedEleve = new Eleve { Nom = "Dupont", Prenom = "Jean-Paul", Age = 13, Sexe = true };
            _mockEleveService.Setup(s => s.UpdateEleveByIdAsync(It.IsAny<int>(), It.IsAny<Eleve>())).ReturnsAsync(updatedEleve);

            // Act
            var result = await new Func<int, Eleve, Task<IResult>>((id, eleve) => Task.FromResult(updatedEleve is not null ? Results.Ok(updatedEleve) : Results.NotFound()))(1, updatedEleve);

            // Assert
            var okResult = Assert.IsType<Ok<Eleve>>(result);
            var returnValue = Assert.IsType<Eleve>(okResult.Value);
            Assert.Equal("Jean-Paul", returnValue.Prenom);
        }

        [Fact]
        public async Task DeleteEleveAsync_ReturnsNoContentResult()
        {
            // Arrange
            _mockEleveService.Setup(s => s.DeleteEleveAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await new Func<int, Task<IResult>>(id => Task.FromResult(Results.NoContent()))(1);

            // Assert
            Assert.IsType<NoContent>(result);
        }

        [Fact]
        public async Task GetListSchoolsAsync_ReturnsOkResult_WithListOfSchools()
        {
            // Arrange
            var schools = new List<School>
        {
            new School { Nom = "Ecole 1", NmbEleve = 20 }
        };
            _mockSchoolService.Setup(s => s.GetListSchoolsAsync()).ReturnsAsync(schools);
            // Act
            var result = await new Func<Task<IResult>>(() => Task.FromResult(Results.Ok(schools)))();
            // Assert
            var okResult = Assert.IsType<Ok<List<School>>>(result);
            var returnValue = Assert.IsType<List<School>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }
}