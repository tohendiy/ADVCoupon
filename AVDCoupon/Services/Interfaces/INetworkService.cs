using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.NetworkViewModels;

namespace ADVCoupon.Services
{
    public interface INetworkService
    {
        Network ConvertFromViewModelToNetwork(NetworkItemViewModel networkModel);
        NetworkItemViewModel ConvertFromNetworkToViewModel(Network network);

        Task<Network> GetNetwork(Guid Id);
        Task<NetworkItemViewModel> GetNetworkItemViewModelAsync(Guid Id);
        Task<NetworkItemViewModel> GetNetworkProductCategoryListItemViewModelAsync();

        Task<List<Network>> GetNetworksAsync();
        Task<List<NetworkItemViewModel>> GetNetworkViewModelsAsync();

        Task<Network> CreateNetworkAsync(Network network);
        Task<Network> CreateNetworkAsync(NetworkItemViewModel networkModel);

        Task UpdateNetworkAsync(Network network);
        Task UpdateNetworkAsync(NetworkItemViewModel networkModel);

        Task DeleteNetworkAsync(Guid Id);

        bool IsExist(Guid Id);
    }
}
