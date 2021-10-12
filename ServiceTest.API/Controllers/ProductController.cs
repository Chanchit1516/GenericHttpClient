using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceTest.API.Data;
using ServiceTest.API.DTOs;
using ServiceTest.API.DTOs.Products;
using ServiceTest.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServiceTest.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ProductController(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseMessage<List<Product>> GetAll()
        {
            try
            {
                var products = _context.Products.ToList();

                return new ResponseMessage<List<Product>>()
                {
                    data = products,
                    status = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<List<Product>>()
                {
                    message = ex.Message,
                    status = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpGet]
        public ResponseMessage<Product> GetById(int id)
        {
            try
            {
                var product = _context.Products.Where(s => s.Id == id).FirstOrDefault();

                return new ResponseMessage<Product>()
                {
                    data = product,
                    status = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<Product>()
                {
                    message = ex.Message,
                    status = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost]
        public ResponseMessage<bool> Insert([FromBody] CreateProductRequest request)
        {
            try
            {
                var profile = _mapper.Map<Product>(request);

                var product = _context.Products.Add(profile);
                _context.SaveChanges();

                return new ResponseMessage<bool>()
                {
                    data = true,
                    status = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return new ResponseMessage<bool>()
                {
                    message = ex.Message,
                    status = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPut]
        public ResponseMessage<bool> Update([FromBody] UpdateProductRequest request)
        {
            try
            {
                var profile = _context.Products.Where(s => s.Id == request.Id).FirstOrDefault();
                profile.Name = request.Name;
                profile.Description = request.Description;
                profile.Price = request.Price;
                profile.UpdatedBy = request.UpdatedBy;
                profile.UpdatedDateTime = request.UpdatedDateTime;
                _context.SaveChanges();

                return new ResponseMessage<bool>()
                {
                    data = true,
                    status = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return new ResponseMessage<bool>()
                {
                    message = ex.Message,
                    status = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpDelete]
        public ResponseMessage<bool> Delete(int id)
        {
            try
            {
                var product = _context.Products.Where(s => s.Id == id).FirstOrDefault();
                _context.Products.Remove(product);
                _context.SaveChanges();

                return new ResponseMessage<bool>()
                {
                    data = true,
                    status = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return new ResponseMessage<bool>()
                {
                    message = ex.Message,
                    status = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
