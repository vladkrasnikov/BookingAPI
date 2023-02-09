using BookingAPI.Models.Company;
using BookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
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
    }
}