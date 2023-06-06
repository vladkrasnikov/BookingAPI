using BookingApi.Data.Helpers.Interfaces;
using BookingApi.Data.Models;
using BookingApi.Services.Model.Brand;
using BookingApi.Services.Services;
using FluentResults;
using Moq;
using Xunit;

namespace BookingApi.Tests;

public class BrandServiceTests
{
    [Fact]
    public async Task GetAsync_WithValidBrandId_ReturnsBrandModel()
    {
        //Arrange
        var brandId = Guid.NewGuid();
        var brand = new Brand { Id = brandId, Name = "Test Brand" };
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Brand.GetAsync(brandId)).ReturnsAsync(Result.Ok(brand));
        var brandService = new BrandService(unitOfWorkMock.Object);
        
        //Act
        var result = await brandService.GetAsync(brandId);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(brand.Name, result.Value.Name);
        Assert.Equal(brand.Id, result.Value.Id);
    }
    
    [Fact]
    public async Task GetAsync_WithInvalidBrandId_ReturnsError()
    {
        //Arrange
        var brandId = Guid.NewGuid();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Brand.GetAsync(brandId)).ReturnsAsync(Result.Fail<Brand>("Brand not found"));
        var brandService = new BrandService(unitOfWorkMock.Object);
        //Act
        var result = await brandService.GetAsync(brandId);
        //Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Brand not found", result.Errors.First().Message);
    }
    
    [Fact]
    public async Task GetAsync_WhenCalled_ReturnsBrandModels()
    {
        //Arrange
        var brands = new List<Brand> { new Brand { Id = Guid.NewGuid(), Name = "Test Brand 1" }, new Brand { Id = Guid.NewGuid(), Name = "Test Brand 2" } };
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Brand.GetAsync()).ReturnsAsync(brands);
        var brandService = new BrandService(unitOfWorkMock.Object);
        //Act
        var result = await brandService.GetAsync();
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(brands.Count, result.Value.Count());
        Assert.Equal(brands.First().Name, result.Value.First().Name);
        Assert.Equal(brands.Last().Id, result.Value.Last().Id);
    }
    
    [Fact]
    public async Task CreateAsync_WithValidData_ReturnsBrandModel()
    {
        //Arrange
        var brandToAdd = new AddOrUpdateBrandModel { Name = "Test Brand", CompanyId = Guid.NewGuid() };
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userId = Guid.NewGuid();
        unitOfWorkMock.Setup(uow => uow.Company.GetAsync(brandToAdd.CompanyId)).ReturnsAsync(Result.Ok(new Company { Id = brandToAdd.CompanyId, UserId = userId}));
        unitOfWorkMock.Setup(uow => uow.Brand.GetBrandInCompanyAsync(It.IsAny<Brand>())).ReturnsAsync(Result.Fail<Brand>("Brand not found"));
        unitOfWorkMock.Setup(uow => uow.Brand.CreateAsync(It.IsAny<Brand>())).ReturnsAsync(Result.Ok(new Brand { Id = Guid.NewGuid(), Name = brandToAdd.Name, CompanyId = brandToAdd.CompanyId }));
        var brandService = new BrandService(unitOfWorkMock.Object);
        //Act
        var result = await brandService.CreateAsync(userId, brandToAdd);
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(brandToAdd.Name, result.Value.Name);
        Assert.Equal(brandToAdd.CompanyId, result.Value.CompanyId);
    }
    
    [Fact]
    public async Task CreateAsync_WithInvalidCompanyId_ReturnsError()
    {
        //Arrange
        var brandToAdd = new AddOrUpdateBrandModel { Name = "Test Brand", CompanyId = Guid.NewGuid() };
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Company.GetAsync(brandToAdd.CompanyId)).ReturnsAsync(Result.Fail<Company>("Company not found"));
        var brandService = new BrandService(unitOfWorkMock.Object);
        //Act
        var result = await brandService.CreateAsync(Guid.NewGuid(), brandToAdd);
        //Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Company not found", result.Errors.First().Message);
    }
    
    [Fact]
    public async Task UpdateAsync_WithValidData_ReturnsUpdatedBrandModel()
    {
        //Arrange
        var brandToUpdate = new AddOrUpdateBrandModel { Name = "Updated Brand" };
        var brand = new Brand { Id = Guid.NewGuid(), Name = "Test Brand" };
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Brand.GetAsync(brand.Id)).ReturnsAsync(Result.Ok(brand));
        unitOfWorkMock.Setup(uow => uow.Brand.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(Result.Ok(new Brand { Id = brand.Id, Name = brandToUpdate.Name }));
        var brandService = new BrandService(unitOfWorkMock.Object);
        //Act
        var result = await brandService.UpdateAsync(brand.Id, brandToUpdate);
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(brandToUpdate.Name, result.Value.Name);
        Assert.Equal(brand.Id, result.Value.Id);
    }
    
    [Fact]
    public async Task UpdateAsync_WithInvalidBrandId_ReturnsError()
    {
        //Arrange
        var brandToUpdate = new AddOrUpdateBrandModel { Name = "Updated Brand" };
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Brand.GetAsync(It.IsAny<Guid>())).ReturnsAsync(Result.Fail<Brand>("Brand not found"));
        var brandService = new BrandService(unitOfWorkMock.Object);
        //Act
        var result = await brandService.UpdateAsync(Guid.NewGuid(), brandToUpdate);
        //Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Brand not found", result.Errors.First().Message);
    }
    
    [Fact]
    public async Task DeleteAsync_WithValidBrandId_ReturnsSuccess()
    {
        //Arrange
        var brandId = Guid.NewGuid();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Brand.GetAsync(brandId)).ReturnsAsync(Result.Ok(new Brand { Id = brandId, Name = "Test Brand" }));
        unitOfWorkMock.Setup(uow => uow.Brand.DeleteAsync(brandId)).ReturnsAsync(Result.Ok());
        var brandService = new BrandService(unitOfWorkMock.Object);
        //Act
        var result = await brandService.DeleteAsync(brandId);
        //Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task DeleteAsync_WithInvalidBrandId_ReturnsError()
    {
        //Arrange
        var brandId = Guid.NewGuid();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(uow => uow.Brand.GetAsync(brandId)).ReturnsAsync(Result.Fail<Brand>("Brand not found"));
        var brandService = new BrandService(unitOfWorkMock.Object);
        //Act
        var result = await brandService.DeleteAsync(brandId);
        //Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Brand not found", result.Errors.First().Message);
    }
}