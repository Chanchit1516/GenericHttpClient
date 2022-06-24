using GeneralService.API.DTOs;
using GeneralService.API.DTOs.Products.Requests;
using GeneralService.API.DTOs.Products.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralService.API.Interfaces
{
    public interface IProductService
    {
        Task<ResponseMessage<IEnumerable<GetAllProductResponse>>> GetAll();
        Task<ResponseMessage<GetByIdProductResponse>> GetById(int id);
        Task<ResponseMessage<bool>> Insert(CreateProductRequest request);
        Task<ResponseMessage<bool>> Update(UpdateProductRequest request);
        Task<ResponseMessage<bool>> Delete(int id);
        Task<ResponseMessage<bool>> PostWithFormData();
    }
}
