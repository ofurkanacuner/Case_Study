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
    public class CategoriesController : Controller
    {
        #region - Variable

        private readonly ICategoriesServices _categoriesServices;
        private readonly ILogger<CategoriesController> _logger;
        ApiResponse apiResponse = new ApiResponse
        {
            IsSuccess = false,
            StatusCode = 400
        };

        #endregion

        #region - Ctor

        public CategoriesController(ICategoriesServices categoriesServices, ILogger<CategoriesController> logger)
        {
            _categoriesServices = categoriesServices;
            _logger = logger;
        }

        #endregion

        #region - Categori Get

        /// <summary>
        /// Verilen ID'ye ait kategori öğesini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CategoriesViewModel</returns>
        /// <response code="200">Added</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="404">Not Found</response> 
        [ActionName("Get")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriesViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                _logger.LogInformation("Kategori getirme işlemi başladı.");
                var result = _categoriesServices.Get(id);
                if (result != null)
                {
                    _logger.LogInformation("Kategori getirildi.");
                    apiResponse.IsSuccess = true;
                    apiResponse.StatusCode = 200;
                    apiResponse.Data = JsonConvert.SerializeObject(result);
                    apiResponse.DataObject = result;
                    return Ok(apiResponse);
                }
                else
                {
                    _logger.LogWarning("Kategori getirilemedi");
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

        #region - Categori Add

        /// <summary>
        /// Sisteme yeni bir kategori öğesi ekler.
        /// </summary>
        /// <param name="CategoriesViewModel"></param>
        /// <returns>void</returns>
        /// <response code="200">Added</response> 
        /// <response code="400">Bad Request</response> 
        [ActionName("Add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromForm] CategoriesViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("Kategori ekleme işlemi başladı.");
                _categoriesServices.Add(viewModel);
                _logger.LogInformation("Kategori eklendi.");
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

        #region - Categori List

        /// <summary>
        /// Sistemde var olan bütün kategori öğelerini listeler.
        /// </summary>
        /// <param name=""></param>
        /// <returns>CategoriesViewModel List</returns>
        [ActionName("List")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult List()
        {
            try
            {
                _logger.LogInformation("Kategori getirme işlemi başladı.");
                var result = _categoriesServices.GetList();
                if (result != null)
                {
                    _logger.LogInformation("Kategori getirildi.");
                    apiResponse.IsSuccess = true;
                    apiResponse.StatusCode = 200;
                    apiResponse.Data = JsonConvert.SerializeObject(result);
                    apiResponse.DataObject = result;
                    return Ok(apiResponse);
                }
                else
                {
                    _logger.LogWarning("Kategori getirilemedi");
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

        #region - Categori Delete

        /// <summary>
        /// Verilen ID'ye ait kategori öğesini sistemden siler.
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
                _logger.LogInformation("Kategori silme işlemi başladı.");
                _categoriesServices.Delete(id);
                _logger.LogInformation("Kategori silindi.");
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

        #region - Categori Update

        /// <summary>
        /// Verilen bilgiler ile kategori'yi bulup yeni veriler ile günceller
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [ActionName("Update")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(CategoriesViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("Kategori güncelleme işlemi başladı.");
                _categoriesServices.Update(viewModel);
                _logger.LogInformation("Kategori güncellendi.");
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
