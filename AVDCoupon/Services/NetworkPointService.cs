using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.NetworkPointViewModels;
using AVDCoupon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.Services
{
    public class NetworkPointService : INetworkPointService
    {
        private ApplicationDbContext _context;
        public NetworkPointService(ApplicationDbContext context)
        {
            _context = context;
        }
        public NetworkPointViewModel ConvertFromNetworkPointToViewModel(NetworkPoint networkPoint)
        {
            var networkPointModel = new NetworkPointViewModel()
            {
                Id = networkPoint.Id,
                Name = networkPoint.Name,
                Latitude = networkPoint.Geoposition?.Latitude,
                Longitude = networkPoint.Geoposition?.Longitude,
                Accuracy = networkPoint.Geoposition?.Accuracy,
                Country = networkPoint.Geoposition?.Country,
                City = networkPoint.Geoposition?.City,
                Street = networkPoint.Geoposition?.Street,
                Building = networkPoint.Geoposition?.Building
            };
            return networkPointModel;
        }

        public NetworkPoint ConvertFromViewModelToNetworkPoint(NetworkPointViewModel networkPointModel)
        {
            var networkPoint = new NetworkPoint()
            {
                Id = networkPointModel.Id,
                Name = networkPointModel.Name,
                Geoposition = new Geoposition()
                {
                    Accuracy = networkPointModel.Accuracy,
                    Longitude = networkPointModel.Longitude,
                    Latitude = networkPointModel.Latitude,
                    Country = networkPointModel.Country,
                    City = networkPointModel.City,
                    Street = networkPointModel.Street,
                    Building = networkPointModel.Building,
                }
            };
            return networkPoint;
        }

        public async Task<NetworkPoint> CreateNetworkPointAsync(NetworkPoint networkPoint)
        {
            networkPoint.Id = Guid.NewGuid();
            _context.Add(networkPoint);
            await _context.SaveChangesAsync();
            return networkPoint;
        }

        public async Task<NetworkPoint> CreateNetworkPointAsync(NetworkPointViewModel networkPointModel)
        {
            var networkPoint = new NetworkPoint
            {
                Name = networkPointModel.Name,
                Id = Guid.NewGuid(),
                Geoposition = new Geoposition()
                {
                    Accuracy = networkPointModel.Accuracy,
                    Longitude = networkPointModel.Longitude,
                    Latitude = networkPointModel.Latitude,
                    Country = networkPointModel.Country,
                    City = networkPointModel.City,
                    Street = networkPointModel.Street,
                    Building = networkPointModel.Building,
                    Id = Guid.NewGuid()
                },
                Network = _context.Networks.FirstOrDefault(item=> item.Id == networkPointModel.NetworkId)

            };

            _context.Add(networkPoint);
            await _context.SaveChangesAsync();
            return networkPoint;

        }

        public async Task DeleteNetworkPointAsync(Guid Id)
        {
            var networkPoint = await _context.NetworkPoints.SingleOrDefaultAsync(m => m.Id == Id);
            _context.NetworkPoints.Remove(networkPoint);
            await _context.SaveChangesAsync();
        }

        public async Task<NetworkPoint> GetNetworkPoint(Guid Id)
        {
            var networkPoint = await _context.NetworkPoints.Include(item => item.Geoposition).Include(item=>item.Network)
               .SingleOrDefaultAsync(m => m.Id == Id);
            return networkPoint;
        }

        public async Task<NetworkPointViewModel> GetNetworkPointViewModelAsync(Guid Id)
        {
            var networkPoint = await _context.NetworkPoints.Include(item => item.Geoposition).Include(item => item.Network)
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (networkPoint == null)
            {
                return null;
            }
            var networkPointModel = new NetworkPointViewModel
            {
                Id = networkPoint.Id,
                Name = networkPoint.Name,
                Latitude = networkPoint.Geoposition?.Latitude,
                Longitude = networkPoint.Geoposition?.Longitude,
                Accuracy = networkPoint.Geoposition?.Accuracy,
                Country = networkPoint.Geoposition?.Country,
                City = networkPoint.Geoposition?.City,
                Street = networkPoint.Geoposition?.Street,
                Building = networkPoint.Geoposition?.Building,
                Networks = GetSelectListNetworks(),
                NetworkId = networkPoint.Network.Id
            };
            return networkPointModel;
        }

        public async Task<List<NetworkPoint>> GetNetworkPointsAsync()
        {
            var networkPoints = await _context.NetworkPoints.Include(item => item.Geoposition).ToListAsync();
            return networkPoints;
        }

        public async Task<NetworkPointViewModel> GetNetworkPointNetworkListItemViewModelAsync()
        {
            var networkModel = new NetworkPointViewModel();
            networkModel.Networks = GetSelectListNetworks();
            return networkModel;
        }

        public async Task<List<NetworkPointViewModel>> GetNetworkPointViewModelsAsync()
        {
            var networkPoints = await _context.NetworkPoints.Include(item => item.Geoposition).ToListAsync();
            var networkPointsListViewModel = new List<NetworkPointViewModel>(networkPoints.Count);
            networkPointsListViewModel = networkPoints.Select(item => new NetworkPointViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Latitude = item.Geoposition?.Latitude,
                Longitude = item.Geoposition?.Longitude,
                Accuracy = item.Geoposition?.Accuracy,
                Country = item.Geoposition?.Country,
                City = item.Geoposition?.City,
                Street = item.Geoposition?.Street,
                Building = item.Geoposition?.Building

            }).ToList();
            return networkPointsListViewModel;
        }

        public bool IsExist(Guid Id)
        {
            return _context.NetworkPoints.Any(e => e.Id == Id);
        }

        public async Task UpdateNetworkPointAsync(NetworkPoint networkPoint)
        {
            _context.Update(networkPoint);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNetworkPointAsync(NetworkPointViewModel networkPointModel)
        {
            var networkPoint = await GetNetworkPoint(networkPointModel.Id);
            networkPoint.Name = networkPointModel.Name;
            networkPoint.Geoposition.Accuracy = networkPointModel.Accuracy;
            networkPoint.Geoposition.Longitude = networkPointModel.Longitude;
            networkPoint.Geoposition.Latitude = networkPointModel.Latitude;
            networkPoint.Geoposition.Country = networkPointModel.Country;
            networkPoint.Geoposition.City = networkPointModel.City;
            networkPoint.Geoposition.Street = networkPointModel.Street;
            networkPoint.Geoposition.Building = networkPointModel.Building;
            networkPoint.Network = _context.Networks.FirstOrDefault(item => item.Id == networkPointModel.NetworkId);
            _context.Update(networkPoint);
            await _context.SaveChangesAsync();
        }

        private SelectList GetSelectListNetworks()
        {
            var networks = _context.Networks.Select(x => new { Id = x.Id, Value = x.Caption });
            var networksSelectList = new SelectList(networks, "Id", "Value");
            return networksSelectList;
        }
    }
}
