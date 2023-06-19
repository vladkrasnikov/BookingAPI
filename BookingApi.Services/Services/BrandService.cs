using BookingApi.Data.Helpers.Interfaces;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Brand;
using BookingApi.Services.Extensions;
using FluentResults;
using Mapster;
using SixLabors.ImageSharp.Formats.Png;

namespace BookingApi.Services.Services;

public class BrandService : IBrandService
{
    private readonly IUnitOfWork _unitOfWork;

    public BrandService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BrandModel>> GetAsync(Guid brandId)
    {
        var brandEntity = await _unitOfWork.Brand.GetAsync(brandId);
        
        if (brandEntity.IsFailed)
            return brandEntity.ToResult();
        
        return brandEntity.Value.Adapt<BrandModel>();
    }

    public async Task<Result<IEnumerable<BrandModel>>> GetAsync()
    {
        var brands = await _unitOfWork.Brand.GetAsync();
        return brands.EntityToModel<IEnumerable<Brand>, IEnumerable<BrandModel>>();
    }

    public async Task<Result<BrandModel>> CreateAsync(Guid userId, AddOrUpdateBrandModel brand)
    {
        var company = await _unitOfWork.Company.GetAsync(brand.CompanyId);
        if(company.IsFailed)
        {
            return company.ToResult();
        }
        
        var brandEntity = await _unitOfWork.Brand.GetBrandInCompanyAsync(brand.Adapt<Brand>());
        // Check if brand with the same name already exists, return 400 not 500
        if(brandEntity.IsSuccess)
        {
            return Result.Fail("Brand with the same name already exists in this company");
        }

        //check if user is owner of company to create brand in this company
        if (company.Value.UserId != userId)
        {
            return Result.Fail("User is not authorized to create brand in this company");
        }
        
        if(!String.IsNullOrEmpty(brand.Image))
            brand.Image = ResizeImageTo1080pAndConvertToBase64(brand.Image);
        
        var brandResult = await _unitOfWork.Brand.CreateAsync(brand.Adapt<Brand>());
        return brandResult.Value.Adapt<BrandModel>();
    }

    public async Task<Result<BrandModel>> UpdateAsync(Guid id, AddOrUpdateBrandModel brand)
    {
        var brandEntity = await _unitOfWork.Brand.GetAsync(id);
        if (brandEntity.IsFailed)
            return brandEntity.ToResult();

        if(!String.IsNullOrEmpty(brand.Image))
            brand.Image = ResizeImageTo1080pAndConvertToBase64(brand.Image);
        
        var mappedBrand = brand.Adapt<Brand>();
        mappedBrand.Id = id;
        
        var result = _unitOfWork.Brand.UpdateAsync(mappedBrand);

        await _unitOfWork.SaveAsync();
        
        return result.Value.Adapt<BrandModel>();
    }

    public async Task<Result> DeleteAsync(Guid brandId)
    {
        var brandEntity = await _unitOfWork.Brand.GetAsync(brandId);
        if (brandEntity.IsFailed)
            return brandEntity.ToResult();
        
        return await _unitOfWork.Brand.DeleteAsync(brandId);
    }
    
    private string ResizeImageTo1080pAndConvertToBase64(string base64Input)
    {
        // Convert base64 string to byte array
        byte[] imageBytes = Convert.FromBase64String(base64Input);

        using (MemoryStream inputMs = new MemoryStream(imageBytes))
        {
            using (Image image = Image.Load(inputMs))
            {
                double scaleFactor = Math.Min(1920 / (double)image.Width, 1080 / (double)image.Height);

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Stretch,
                    Size = new Size((int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor))
                }));

                using (MemoryStream outputMs = new MemoryStream())
                {
                    image.Save(outputMs, new PngEncoder());
                    byte[] resizedImageBytes = outputMs.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(resizedImageBytes);
                    return base64String;
                }
            }
        }
    }
}