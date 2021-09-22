#region - Using

using GeoSys.Domain.Helpers;
using GeoSys.Domain.ViewModels;
using GeoSys.Services.DBServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;

#endregion

namespace GeoSys.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        #region - Variable

        private readonly IProductsServices _productsServices;
        private readonly ILogger<ProductsController> _logger;
        ApiResponse apiResponse = new ApiResponse
        {
            IsSuccess = false,
            StatusCode = 400
        };

        #endregion

        #region - Ctor

        public ProductsController(IProductsServices productsServices, ILogger<ProductsController> logger)
        {
            _productsServices = productsServices;
            _logger = logger;
        }

        #endregion

        #region - Product Get

        /// <summary>
        /// Verilen ID'ye ait ürün öğesini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ProductsViewModel</returns>
        [ActionName("Get")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductsViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                _logger.LogInformation("Ürün getirme işlemi başladı.");
                var result = _productsServices.Get(id);
                if (result != null)
                {
                    _logger.LogInformation("Ürün getirildi.");
                    apiResponse.IsSuccess = true;
                    apiResponse.StatusCode = 200;
                    apiResponse.Data = JsonConvert.SerializeObject(result);
                    apiResponse.DataObject = result;
                    return Ok(apiResponse);
                }
                else
                {
                    _logger.LogWarning("Ürün getirilemedi");
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = 404;
                    return NotFound(apiResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Hata Mesajı : " + ex.Message);
                return BadRequest(apiResponse);
            }
        }
        #endregion

        #region - Product Add

        /// <summary>
        /// Sisteme yeni bir ürün öğesi ekler.
        /// </summary>
        /// <param name="ProductsViewModel"></param>
        /// <returns>void</returns>
        [ActionName("Add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Add([FromForm] ProductsViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("Ürün ekleme işlemi başladı.");
                _productsServices.Add(viewModel);
                _logger.LogInformation("Ürün eklendi.");
                apiResponse.IsSuccess = true;
                apiResponse.StatusCode = 200;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Hata Mesajı : " + ex.Message);
                return BadRequest(apiResponse);
            }
        }

        #endregion

        #region - Products List

        /// <summary>
        /// Sistemde var olan bütün ürün öğelerini listeler.
        /// </summary>
        /// <param name=""></param>
        /// <returns>ProductsViewModel List</returns>
        [ActionName("List")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult List()
        {
            try
            {
                _logger.LogInformation("Ürün getirme işlemi başladı.");
                var result = _productsServices.GetList();
                if (result != null)
                {
                    _logger.LogInformation("Ürün getirildi.");
                    apiResponse.IsSuccess = true;
                    apiResponse.StatusCode = 200;
                    apiResponse.Data = JsonConvert.SerializeObject(result);
                    apiResponse.DataObject = result;
                    return Ok(apiResponse);
                }
                else
                {
                    _logger.LogWarning("Ürün getirilemedi");
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = 404;
                    return NotFound(apiResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Hata Mesajı : " + ex.Message);
                return BadRequest(apiResponse);
            }
        }

        #endregion

        #region - Product Delete

        /// <summary>
        /// Verilen ID'ye ait ürün öğesini sistemden siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>void</returns>
        [ActionName("Delete")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("Ürün silme işlemi başladı.");
                _productsServices.Delete(id);
                _logger.LogInformation("Ürün silindi.");
                apiResponse.IsSuccess = true;
                apiResponse.StatusCode = 200;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Hata Mesajı : " + ex.Message);
                return BadRequest(apiResponse);
            }
        }

        #endregion

        #region - Product Update

        /// <summary>
        /// Verilen bilgiler ile ürün'yi bulup yeni veriler ile günceller
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [ActionName("Update")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(ProductsViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("Ürün güncelleme işlemi başladı.");
                _productsServices.Update(viewModel);
                _logger.LogInformation("Ürün güncellendi.");
                apiResponse.IsSuccess = true;
                apiResponse.StatusCode = 200;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Hata Mesajı : " + ex.Message);
                return BadRequest(apiResponse);
            }
        }

        #endregion

    }
}
