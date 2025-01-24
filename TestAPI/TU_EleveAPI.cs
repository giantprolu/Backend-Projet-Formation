using Models.ModelAPI;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Business.ServicesAPI;

namespace TestAPI.TU_EleveAPI { 
public class TU_EleveAPI
{
    private Mock<IEleveServiceAPI> CreateMockEleveServices()
    {
        var mockEleveServices = new Mock<IEleveServiceAPI>();
        return mockEleveServices;
    }

    [Fact]
    public async Task TestGetEleves()
    {
        // Arrange
        var mockEleveServices = CreateMockEleveServices();
        mockEleveServices.Setup(s => s.GetElevesAsync()).ReturnsAsync(new List<EleveAPI>
    {
        new EleveAPI { Nom = "Dupont", Prenom = "Jean", Age = 12, Sexe = true }
    });

        var controller = new EleveController(mockEleveServices.Object);

        // Act
        var result = await controller.GetEleves();
        var okResult = result.Result as OkObjectResult;
        var eleves = okResult?.Value as List<EleveAPI>;

        // Assert
        Assert.NotNull(eleves);
        Assert.Single(eleves);
        Assert.Equal("Dupont", eleves[0].Nom);
        Assert.Equal("Jean", eleves[0].Prenom);
        Assert.Equal(12, eleves[0].Age);
        Assert.True(eleves[0].Sexe);
    }


    [Fact]
    public async Task TestGetEleveById()
    {
        // Arrange
        var mockEleveServices = CreateMockEleveServices();
        var eleve = new EleveAPI { Nom = "Dupont", Prenom = "Jean", Age = 12, Sexe = true };
        mockEleveServices.Setup(s => s.GetEleveByIdAsync(It.IsAny<int>())).ReturnsAsync(eleve);

        var controller = new EleveController(mockEleveServices.Object);

        // Act
        var result = await controller.GetEleve(1);
        var okResult = result.Result as OkObjectResult;
        var eleveResult = okResult?.Value as EleveAPI;

        // Assert
        Assert.NotNull(eleveResult);
        Assert.Equal("Dupont", eleveResult.Nom);
        Assert.Equal("Jean", eleveResult.Prenom);
        Assert.Equal(12, eleveResult.Age);
        Assert.True(eleveResult.Sexe);
    }


    [Fact]
    public async Task TestAddEleve()
    {
        // Arrange
        var mockEleveServices = CreateMockEleveServices();
        var newEleve = new EleveAPI { Nom = "Martin", Prenom = "Paul", Age = 14, Sexe = false };
        mockEleveServices.Setup(s => s.AddEleveAsync(It.IsAny<EleveAPI>())).ReturnsAsync(newEleve);

        var controller = new EleveController(mockEleveServices.Object);

        // Act
        var result = await controller.PostEleve(newEleve);
        var createdResult = result.Result as CreatedAtActionResult;
        var eleve = createdResult?.Value as EleveAPI;

        // Assert
        Assert.NotNull(eleve);
        Assert.Equal("Martin", eleve.Nom);
        Assert.Equal("Paul", eleve.Prenom);
        Assert.Equal(14, eleve.Age);
        Assert.False(eleve.Sexe);
    }

    [Fact]
    public async Task TestUpdateEleveByName()
    {
        // Arrange
        var mockEleveServices = CreateMockEleveServices();
        mockEleveServices.Setup(s => s.UpdateEleveByNameAsync(It.IsAny<string>(), It.IsAny<EleveAPI>())).ReturnsAsync(true);

        var controller = new EleveController(mockEleveServices.Object);
        var updatedEleve = new EleveAPI { Nom = "Dupont", Prenom = "Jean-Paul", Age = 13, Sexe = true };

        // Act
        var result = await controller.PutEleveByName("Dupont", updatedEleve) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task TestDeleteEleveById()
    {
        // Arrange
        var mockEleveServices = CreateMockEleveServices();
        mockEleveServices.Setup(s => s.DeleteEleveByIdAsync(It.IsAny<int>())).ReturnsAsync(true);

        var controller = new EleveController(mockEleveServices.Object);

        // Act
        var result = await controller.DeleteEleveById(1) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }
}
}