using Models.ModelMinimal;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Business.ServicesMinimal;
using System.Net.Http.Json;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace TestAPI
{
    public class TU_EleveMini : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TU_EleveMini(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private Mock<IEleveServiceMini> CreateMockEleveServiceMini()
        {
            var mockEleveServices = new Mock<IEleveServiceMini>();
            return mockEleveServices;
        }
        private Mock<ISchoolServiceMini> CreateMockSchoolServiceMini()
        {
            var mockSchoolServices = new Mock<ISchoolServiceMini>();
            return mockSchoolServices;
        }

        [Fact]
        public async Task TestGetEleves()
        {
            // Arrange
            var mockEleveServices = CreateMockEleveServiceMini();
            mockEleveServices.Setup(s => s.GetListEleveAsync()).ReturnsAsync(new List<EleveMini>
            {
                new EleveMini { Nom = "Dupont", Prenom = "Jean", Age = 12, Sexe = true }
            });

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => mockEleveServices.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/ListEleve");
            response.EnsureSuccessStatusCode();
            var eleves = await response.Content.ReadFromJsonAsync<List<EleveMini>>();

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
            var mockEleveServices = CreateMockEleveServiceMini();
            var eleve = new EleveMini { Nom = "Dupont", Prenom = "Jean", Age = 12, Sexe = true };
            mockEleveServices.Setup(s => s.GetEleveByIdAsync(It.IsAny<int>())).ReturnsAsync(eleve);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => mockEleveServices.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/eleve/1");
            response.EnsureSuccessStatusCode();
            var eleveResult = await response.Content.ReadFromJsonAsync<EleveMini>();

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
            var mockEleveServices = CreateMockEleveServiceMini();
            var newEleve = new EleveMini { Nom = "Martin", Prenom = "Paul", Age = 14, Sexe = false, SchoolId = 1 };
            mockEleveServices.Setup(s => s.PostEleveAsync(It.IsAny<EleveMini>(), It.IsAny<string>())).ReturnsAsync(newEleve);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => mockEleveServices.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/eleve?schoolName=TestSchool", newEleve);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Response status code does not indicate success: {response.StatusCode} ({response.ReasonPhrase}). Content: {errorContent}");
            }
            var eleve = await response.Content.ReadFromJsonAsync<EleveMini>();

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
            var mockEleveServices = CreateMockEleveServiceMini();
            var updatedEleve = new EleveMini { Nom = "Dupont", Prenom = "Jean-Paul", Age = 13, Sexe = true };
            mockEleveServices.Setup(s => s.UpdateEleveByNameAsync(It.IsAny<string>(), It.IsAny<EleveMini>(), It.IsAny<string>())).ReturnsAsync(updatedEleve);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => mockEleveServices.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.PutAsJsonAsync("/eleve/updateByName/Dupont?newSchoolName=NewSchool", updatedEleve);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Response status code does not indicate success: {response.StatusCode} ({response.ReasonPhrase}). Content: {errorContent}");
            }
            var eleveResult = await response.Content.ReadFromJsonAsync<EleveMini>();

            // Assert
            Assert.NotNull(eleveResult);
            Assert.Equal("Dupont", eleveResult.Nom);
            Assert.Equal("Jean-Paul", eleveResult.Prenom);
            Assert.Equal(13, eleveResult.Age);
            Assert.True(eleveResult.Sexe);
        }



        [Fact]
        public async Task TestDeleteEleveById()
        {
            // Arrange
            var mockEleveServices = CreateMockEleveServiceMini();
            mockEleveServices.Setup(s => s.DeleteEleveAsync(It.IsAny<int>())).ReturnsAsync(true);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => mockEleveServices.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.DeleteAsync("/eleve/1");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        //test sur GetListSchoolsAsync
        public async Task TestGetListSchools()
        {
            // Arrange
            var mockSchoolServices = CreateMockSchoolServiceMini();
            mockSchoolServices.Setup(s => s.GetListSchoolsAsync()).ReturnsAsync(new List<SchoolMini>
            {
                new SchoolMini { Nom = "Ecole1", NmbEleve = 10 },
                new SchoolMini { Nom = "Ecole2", NmbEleve = 20 }
            });
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => mockSchoolServices.Object);
                });
            }).CreateClient();
            // Act
            var response = await client.GetAsync("/ListSchools");
            response.EnsureSuccessStatusCode();
            var schools = await response.Content.ReadFromJsonAsync<List<SchoolMini>>();
            // Assert
            Assert.NotNull(schools);
            Assert.Equal(2, schools.Count);
            Assert.Equal("Ecole1", schools[0].Nom);
            Assert.Equal(10, schools[0].NmbEleve);
            Assert.Equal("Ecole2", schools[1].Nom);
            Assert.Equal(20, schools[1].NmbEleve);
        }
    }
}
