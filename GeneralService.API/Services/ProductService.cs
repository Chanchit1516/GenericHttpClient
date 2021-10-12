using GeneralService.API.DTOs;
using GeneralService.API.DTOs.Products.Requests;
using GeneralService.API.DTOs.Products.Responses;
using GeneralService.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Rest;

namespace GeneralService.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IRestUtility _restUtility;
        public ProductService(IConfiguration configuration, IRestUtility restUtility)
        {
            _restUtility = restUtility;
        }

        public async Task<ResponseMessage<IEnumerable<GetAllProductResponse>>> GetAll()
        {
            var url = $"/api/v1/Product/GetAll";
            return await _restUtility.GetAsync<ResponseMessage<IEnumerable<GetAllProductResponse>>>(url);
        }

        public async Task<ResponseMessage<GetByIdProductResponse>> GetById(int id)
        {
            var url = $"/api/v1/Product/GetById?id=${id}";
            return await _restUtility.GetAsync<ResponseMessage<GetByIdProductResponse>>(url);
        }

        public async Task<ResponseMessage<bool>> Insert(CreateProductRequest request)
        {
            var url = $"/api/v1/Product/Insert";
            return await _restUtility.PostAsync<ResponseMessage<bool>>(url, request);
        }

        public async Task<ResponseMessage<bool>> Update(UpdateProductRequest request)
        {
            var url = $"/api/v1/Product/Update";
            return await _restUtility.PutAsync<ResponseMessage<bool>>(url, request);
        }

        public async Task<ResponseMessage<bool>> Delete(int id)
        {
            var url = $"/api/v1/Product/Delete?id={id}";
            return await _restUtility.DeleteAsync<ResponseMessage<bool>>(url);
        }
    }
}
