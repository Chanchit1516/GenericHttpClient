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
using Utilities.RestApi;
using static Utilities.RestApi.RestUtility;

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
            var url = $"/api/v1/Product/GetById?id={id}";
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

        public async Task<ResponseMessage<bool>> PostWithFormData()
        {
            FormBodyRequest request = new FormBodyRequest();
            request.product_name = "oattest";
            request.product_code = "oat01";

            request.list = new List<FormBodyList>()
            {
                new FormBodyList() { key = "C01", value = "1" , number1 = 1 , number2 = 2 , decimal1 = Decimal.Parse("1.11") , decimal2 = Decimal.Parse("2.22") , datetime1 = DateTime.Now , datetime2 = DateTime.Now , bool1 = true , bool2 = false},
                new FormBodyList() { key = "C02", value = "2" , number1 = null , number2 = 2 , decimal1 = null , decimal2 = Decimal.Parse("2.22") , datetime1 = null , datetime2 = DateTime.Now , bool1 = true , bool2 = null , list = new List<FormBodyListInList>{ new FormBodyListInList() { test1 = "t1-1" , test2 = "t1-1" } , new FormBodyListInList() { test1 = "t1-2", test2 = "t1-2" } } },
            };

            var url = $"/api/v1/Product/FormData";

            return await _restUtility.PostAsync<ResponseMessage<bool>>(url, request, HttpContentType.XForm);
        }

    }
}
