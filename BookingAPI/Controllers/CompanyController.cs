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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            var result = await _companyService.CreateAsync(request.Adapt<AddOrUpdateCompanyModel>());
            var mappedResult = result.Value.Adapt<GetCompanyResponse>();
            return Ok(mappedResult);
        }
        
        /// <summary>
        /// Updates a company
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Company updated", typeof(GetCompanyResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Company not found")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCompany([FromRoute] Guid id, [FromBody] AddOrUpdateCompanyModel request)
        {
            var result = await _companyService.UpdateAsync(id, request);
            var mappedResult = result.Value.Adapt<GetCompanyResponse>();
            return Ok(mappedResult);
        }
        
        /// <summary>
        /// Deletes a company
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Company deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Company not found")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompany([FromRoute] Guid id)
        {
            return Ok(await _companyService.DeleteAsync(id));
        }
        
        /// <summary>
        /// Get copanies by user id
        /// </summary>
        [HttpGet("user/{userId}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Companies found", typeof(IEnumerable<GetCompanyResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Companies not found")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompaniesByUserId([FromRoute] Guid userId)
        {
            var result = await _companyService.GetByUserIdAsync(userId);
            var mappedResult = result.Value.Adapt<IEnumerable<GetCompanyResponse>>();
            return Ok(mappedResult);
        }
    }
}