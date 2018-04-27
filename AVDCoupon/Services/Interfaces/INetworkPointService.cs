using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.NetworkPointViewModels;

namespace ADVCoupon.Services
{
    public interface INetworkPointService
    {
        NetworkPoint ConvertFromViewModelToNetworkPoint(NetworkPointViewModel networkPointModel);
        NetworkPointViewModel ConvertFromNetworkPointToViewModel(NetworkPoint networkPoint);

        Task<NetworkPoint> GetNetworkPoint(Guid Id);
        Task<NetworkPointViewModel> GetNetworkPointViewModelAsync(Guid Id);

        Task<List<NetworkPoint>> GetNetworkPointsAsync();
        Task<List<NetworkPointViewModel>> GetNetworkPointViewModelsAsync();
        Task<NetworkPointViewModel> GetNetworkPointNetworkListItemViewModelAsync();

        Task<NetworkPoint> CreateNetworkPointAsync(NetworkPoint networkPoint);
        Task<NetworkPoint> CreateNetworkPointAsync(NetworkPointViewModel networkPointModel);

        Task UpdateNetworkPointAsync(NetworkPoint networkPoint);
        Task UpdateNetworkPointAsync(NetworkPointViewModel networkPointModel);

        Task DeleteNetworkPointAsync(Guid Id);

        Task AddNetworkPoints(List<NetworkPoint> list);

        bool IsExist(Guid Id);
    }
}
