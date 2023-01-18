using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAppAPI.DataSet;
using StoreAppAPI.DTOs;
using StoreAppAPI.Model;

namespace StoreAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Products
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProduct_Details()
        {
            try
            {
                return await _context.Product_Details.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Products/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductModel(int id)
        {
            try
            {
                if (id == -1)
                    return await _context.Product_Details.Where(a => a.ProductStatus == true).ToListAsync();
                else if (id == -2)
                    return await _context.Product_Details.Where(a => a.ProductStatus == false).ToListAsync();
                else
                {
                    var productModel = await _context.Product_Details.Where(a => a.Id == id).ToListAsync();

                    if (productModel == null)
                    {
                        return NotFound();
                    }

                    return productModel;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(int id, ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(productModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductModel(ProductdetailsDTO productModel)
        {
            try
            {
                var duplicate = (from d in _context.Product_Details where d.ProductName == productModel.ProductName select d).ToList();
                var duplicate_1 = (from d in _context.Product_Details where d.ProductCode == productModel.ProductCode select d).ToList();

                if (duplicate.Count > 0 && duplicate_1.Count > 0)
                {
                    return BadRequest("Product Name & Product Code already present");
                }
                else if (duplicate_1.Count > 0)
                {
                    return BadRequest(productModel.ProductCode + " Product Code already present");
                }
                else if (duplicate.Count > 0)
                {
                    return BadRequest(productModel.ProductName + " Product Name already present");
                }

                var productdetails = mapper.Map<ProductModel>(productModel);
                _context.Product_Details.Add(productdetails);
                await _context.SaveChangesAsync();

                return productdetails;
                //CreatedAtAction("GetProductModel", new { id = productdetails.Id }, productModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            try
            {
                var productModel = await _context.Product_Details.FindAsync(id);
                if (productModel == null)
                {
                    return NotFound("Product not found");
                }
                productModel.ProductStatus = false;

                //_context.Product_Details.Remove(productModel);
                await _context.SaveChangesAsync();

                return Ok(productModel);
            }
            catch (Exception ex)
            {
                   return BadRequest(ex.Message);
            }
        }

        private bool ProductModelExists(int id)
        {
            return _context.Product_Details.Any(e => e.Id == id);
        }
    }
}
