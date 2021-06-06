using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product_Catalog.Entities;
using Product_Catalog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Product_Catalog.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetProducts();
            return new ObjectResult(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductById(string id)
         {
            var product = await _repository.GetProductById(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found");
                return new NotFoundResult();
            }

            return new ObjectResult(product);

        }

        [HttpGet]
        [Route("[action]/{category}")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            var product = await _repository.GetProductByCategory(category);

            return new ObjectResult(product);
        }

        [HttpGet]
        [Route("[action]/{name}")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var product = await _repository.GetProductByName(name);

            return new ObjectResult(product);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", product.Id);

        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var result = await _repository.UpdateProduct(product);
            return new ObjectResult(new {result=result,Msg="Updated Successfully" });
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _repository.DeleteProduct(id);
            return new ObjectResult(result);
        }

    }
}

