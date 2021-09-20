#region - Using

using GeoSys.DAL.Models;
using GeoSys.DAL.Repository;
using GeoSys.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace GeoSys.Services.DBServices
{
    public interface IProductsServices
    {
        ProductsViewModel Get(int id);

        List<ProductsViewModel> GetList();

        void Add(ProductsViewModel viewModel);

        Products Update(ProductsViewModel viewModel);

        void Delete(int id);
    }
    public class ProductsServices : IProductsServices
    {

        private readonly EntityRepository<Products> __entityRepository;

        public ProductsServices()
        {
            __entityRepository = new EntityRepository<Products>();
        }

        public void Add(ProductsViewModel viewModel)
        {
            __entityRepository.Add(new Products()
            {
                CreationDate = DateTime.Now,
                IsItDeleted = false,
                Status = true,
                Title = viewModel.Title,
                Price = viewModel.Price,
                Categories = new Categories
                {
                    Id = viewModel.CategoriId,
                }
            });
        }

        public void Delete(int id)
        {
            __entityRepository.Delete(item => item.Id == id);
        }

        public ProductsViewModel Get(int id)
        {
            return __entityRepository.GetList().Select(item => new ProductsViewModel
            {
                Title = item.Title,
                Price = item.Price,
                CreationDate = item.CreationDate,
                DateOfUpdate = item.DateOfUpdate,
                Id = item.Id,
                IsItDeleted = item.IsItDeleted,
                Status = item.Status,
                CategoriId = item.Categories.Id,
                CategoriTitle = item.Categories.Title

            }).FirstOrDefault();
        }

        public List<ProductsViewModel> GetList()
        {
            return __entityRepository.GetList().Select(item => new ProductsViewModel
            {
                Title = item.Title,
                Price = item.Price,
                CreationDate = item.CreationDate,
                DateOfUpdate = item.DateOfUpdate,
                Id = item.Id,
                IsItDeleted = item.IsItDeleted,
                Status = item.Status,
                CategoriId = item.Categories.Id,
                CategoriTitle = item.Categories.Title

            }).ToList();
        }

        public Products Update(ProductsViewModel viewModel)
        {
            var result = __entityRepository.Get(item => item.Id == viewModel.Id);

            result.Title = viewModel.Title;
            result.Price = viewModel.Price;
            result.DateOfUpdate = DateTime.Now;
            result.IsItDeleted = viewModel.IsItDeleted;
            result.Status = viewModel.Status;
            result.Categories = new Categories
            {
                Id = viewModel.Id,
                Title = viewModel.Title
            };

            __entityRepository.Update(result);

            return result;
        }
    }
}
