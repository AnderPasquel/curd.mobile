using curd.mobile.Models;
using curd.mobile.Models.DTO;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace curd.mobile.Services
{
    interface IRequestApi
    {
        [Post("/v1/products")]
        Task<ProductDTO> SubmitPost([Body]ProductDTO post);

        [Put("/v1/products/{pid}")]
        Task<ProductDTO> UpdatePost(long pid, [Body]ProductDTO post);

        [Delete("/v1/products/{pid}")]
        Task DeleteProduct(long pid);
    }
}
