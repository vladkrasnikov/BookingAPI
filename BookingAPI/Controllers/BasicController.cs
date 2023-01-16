using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        public BasicController() { }

        /// <summary>
        /// Get account contact
        /// </summary>
        [HttpGet("{accountContactId:guid}")]
        [ProducesResponseType(typeof(GetAccountContactResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountContact(
            [FromCustom] TenantModel tenant,
            [FromRoute] string accountReference,
            [FromRoute] Guid accountContactId
        )
        {
            var result = await _accountContactService.GetContactAsync(
                tenant.Id,
                accountReference,
                accountContactId
            );

            return result
                .BindMap(model => model.ToGetAccountContactResponse())
                .ToActionResult();
        }
    }
}
