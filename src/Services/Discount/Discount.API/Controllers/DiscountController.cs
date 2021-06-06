using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;
        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("[action]/{productName}")]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            return new ObjectResult(await _repository.GetDiscount(productName));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateDiscount([FromBody]Coupon coupon)
        {
            return new ObjectResult(await _repository.CreateDiscount(coupon));
        }
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            return new ObjectResult(await _repository.UpdateDiscount(coupon));
        }

        [HttpPost]
        [Route("[action]/{productName}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            return new ObjectResult(await _repository.DeleteDiscount(productName));
        }
    }
}
