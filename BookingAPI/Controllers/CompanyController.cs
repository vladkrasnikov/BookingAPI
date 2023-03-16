using BookingAPI.Models.Company;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Company;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Get list of companies
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetCompanyResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanies()
        {
            var result = await _companyService.GetListAsync();
            var mappedResult = result.Value.Adapt<IEnumerable<GetCompanyResponse>>();
            return Ok(mappedResult);
        }

        /// <summary>
        /// Get single company
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Company found", typeof(GetCompanyResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Company not found")]
        public async Task<IActionResult> GetCompanies([FromRoute] Guid id)
        {
            var result = await _companyService.GetAsync(id);
            var mappedResult = result.Value.Adapt<GetCompanyResponse>();
            return Ok(mappedResult);
        }

        /// <summary>
        /// Create a company
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Company created", typeof(GetCompanyResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            var result = await _companyService.CreateAsync(request.Adapt<CreateCompanyModel>());
            var mappedResult = result.Value.Adapt<GetCompanyResponse>();
            return Ok(mappedResult);
        }
    }
}