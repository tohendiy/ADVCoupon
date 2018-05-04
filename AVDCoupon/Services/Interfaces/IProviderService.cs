using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.ProviderViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.Services
{
    public interface IProviderService
    {
        Provider ConvertFromViewModelToProvider(ProviderItemViewModel providerModel);
        ProviderItemViewModel ConvertFromProviderToViewModel(Provider provider);

        Task<Provider> GetProvider(Guid Id);
        Task<ProviderItemViewModel> GetProviderItemViewModelAsync(Guid Id);

        Task<List<Provider>> GetProvidersAsync();
        Task<List<ProviderItemViewModel>> GetProviderViewModelsAsync();

        Task<Provider> CreateProviderAsync(Provider provider);
        Task<Provider> CreateProviderAsync(ProviderItemViewModel providerModel);

        Task UpdateProviderAsync(Provider provider);
        Task UpdateProviderAsync(ProviderItemViewModel providerModel);

        Task DeleteProviderAsync(Guid Id);

        bool IsExist(Guid Id);

        SelectList GetSelectListProviders();



    }
}
