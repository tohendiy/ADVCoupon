using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.NetworkViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.Services
{
    public interface INetworkService
    {
        Network ConvertFromViewModelToNetwork(NetworkItemViewModel networkModel);
        NetworkItemViewModel ConvertFromNetworkToViewModel(Network network);

        Task<Network> GetNetwork(Guid Id);
        Task<NetworkItemViewModel> GetNetworkItemViewModelAsync(Guid Id);
        Task<NetworkItemViewModel> GetNetworkProductCategoryListItemViewModelAsync();
        Task<Network> GetNetworkWithAdressesAsync(Guid Id);

        Task<List<Network>> GetNetworksAsync();
        Task<List<NetworkItemViewModel>> GetNetworkViewModelsAsync();
        Task<List<Network>> GetNetworksByProductCategoryAsync(Guid id);
        Task<List<Network>> GetNetworksByIdListAsync(List<Guid> guids);

        Task<Network> CreateNetworkAsync(Network network);
        Task<Network> CreateNetworkAsync(NetworkItemViewModel networkModel);

        Task UpdateNetworkAsync(Network network);
        Task UpdateNetworkAsync(NetworkItemViewModel networkModel);

        Task DeleteNetworkAsync(Guid Id);

        bool IsExist(Guid Id);
        SelectList GetSelectListProductCategories();
        SelectList GetSelectListNetworks();


    }
}
