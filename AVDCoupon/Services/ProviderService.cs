using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.ProviderViewModels;
using AVDCoupon.Data;
using AVDCoupon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.Services
{
    public class ProviderService : IProviderService
    {

        private ApplicationDbContext _context;
        public ProviderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProviderItemViewModel ConvertFromProviderToViewModel(Provider provider)
        {
            var providerModel = new ProviderItemViewModel()
            {
                Id = provider.Id,
                Name = provider.Name,
                LogoImageView = provider.LogoImage
            };
            return providerModel;
        }

        public Provider ConvertFromViewModelToProvider(ProviderItemViewModel providerModel)
        {
            var provider = new Provider()
            {
                Id = providerModel.Id,
                Name = providerModel.Name,
                LogoImage = providerModel.LogoImageView
            };
            return provider;
        }

        public async Task<Provider> CreateProviderAsync(Provider provider)
        {
            provider.Id = Guid.NewGuid();
            _context.Add(provider);
            await _context.SaveChangesAsync();
            return provider;
        }

        public async Task<Provider> CreateProviderAsync(ProviderItemViewModel providerModel)
        {
            var provider = new Provider
            {
                Name = providerModel.Name,
                Id = Guid.NewGuid(),
                RetailUsers = new List<ApplicationUser>()

            };
            if (providerModel.LogoImage != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await providerModel.LogoImage.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        provider.LogoImage = memoryStream.ToArray();
                    }
                }
            }
            _context.Add(provider);
            await _context.SaveChangesAsync();
            return provider;

        }

        public async Task DeleteProviderAsync(Guid Id)
        {
            var provider = await _context.Providers.SingleOrDefaultAsync(m => m.Id == Id);
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
        }

        public async Task<Provider> GetProvider(Guid Id)
        {
            var provider = await _context.Providers
               .SingleOrDefaultAsync(m => m.Id == Id);
            return provider;
        }

        public async Task<ProviderItemViewModel> GetProviderItemViewModelAsync(Guid Id)
        {
            var provider = await _context.Providers
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (provider == null)
            {
                return null;
            }
            var providerModel = new ProviderItemViewModel
            {
                Id = provider.Id,
                Name = provider.Name,
                LogoImageView = provider.LogoImage
            };
            return providerModel;
        }

        public async Task<List<Provider>> GetProvidersAsync()
        {
            var providers = await _context.Providers.ToListAsync();
            return providers;
        }

        public async Task<List<ProviderItemViewModel>> GetProviderViewModelsAsync()
        {
            var providers = await _context.Providers.ToListAsync();
            var providersListViewModel = new List<ProviderItemViewModel>(providers.Count);
            providersListViewModel = providers.Select(item => new ProviderItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                LogoImageView = item.LogoImage

            }).ToList();
            return providersListViewModel;
        }

        public bool IsExist(Guid Id)
        {
            return _context.Providers.Any(e => e.Id == Id);
        }

        public async Task UpdateProviderAsync(Provider provider)
        {
            _context.Update(provider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProviderAsync(ProviderItemViewModel providerModel)
        {
            var provider = await GetProvider(providerModel.Id);
            provider.Name = providerModel.Name;
            if (providerModel.LogoImage != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await providerModel.LogoImage.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        provider.LogoImage = memoryStream.ToArray();
                    }
                }
            }
            _context.Update(provider);
            await _context.SaveChangesAsync();
        }

        public SelectList GetSelectListProviders()
        {
            var providers = _context.Providers.ToList();
            var providersSelectList = new SelectList(providers, "Id", "Name");
            return providersSelectList;
        }
    }
}
