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
    public interface ICategoriesServices
    {
        CategoriesViewModel Get(int id);

        List<CategoriesViewModel> GetList();

        void Add(CategoriesViewModel viewModel);

        Categories Update(CategoriesViewModel viewModel);

        void Delete(int id);
    }

    public class CategoriesServices : ICategoriesServices
    {

        private readonly EntityRepository<Categories> __entityRepository;

        public CategoriesServices()
        {
            __entityRepository = new EntityRepository<Categories>();
        }

        public void Add(CategoriesViewModel viewModel)
        {
            __entityRepository.Add(new Categories()
            {
                CreationDate = DateTime.Now,
                IsItDeleted = false,
                Status = true,
                Title = viewModel.Title,
                TopCategoriId = viewModel.TopCategoriId
            });
        }

        public void Delete(int id)
        {
            __entityRepository.Delete(item => item.Id == id);
        }

        public CategoriesViewModel Get(int id)
        {
            return __entityRepository.GetList().Select(item => new CategoriesViewModel
            {
                Title = item.Title,
                CreationDate = item.CreationDate,
                DateOfUpdate = item.DateOfUpdate,
                Id = item.Id,
                IsItDeleted = item.IsItDeleted,
                Status = item.Status,
                TopCategoriId = item.TopCategoriId,
                TopCategoriTitle = item.TopCategori.Title
            }).FirstOrDefault();
        }

        public List<CategoriesViewModel> GetList()
        {
            return __entityRepository.GetList().Select(item => new CategoriesViewModel
            {
                Title = item.Title,
                CreationDate = item.CreationDate,
                DateOfUpdate = item.DateOfUpdate,
                Id = item.Id,
                IsItDeleted = item.IsItDeleted,
                Status = item.Status,
                TopCategoriId = item.TopCategoriId,
                TopCategoriTitle = item.TopCategori.Title
            }).ToList();
        }

        public Categories Update(CategoriesViewModel viewModel)
        {
            var result = __entityRepository.Get(item => item.Id == viewModel.Id);

            result.Title = viewModel.Title;
            result.DateOfUpdate = DateTime.Now;
            result.IsItDeleted = viewModel.IsItDeleted;
            result.Status = viewModel.Status;
            result.TopCategoriId = viewModel.TopCategoriId;

            __entityRepository.Update(result);

            return result;
        }
    }
}
