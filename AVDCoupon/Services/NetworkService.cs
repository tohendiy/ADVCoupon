﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.NetworkViewModels;
using AVDCoupon.Data;
using AVDCoupon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ADVCoupon.Services
{
    public class NetworkService : INetworkService
    {
        private ApplicationDbContext _context;
        public NetworkService(ApplicationDbContext context)
        {
            _context = context;
        }
        public NetworkItemViewModel ConvertFromNetworkToViewModel(Network network)
        {
            var networkModel = new NetworkItemViewModel()
            {
                Id = network.Id,
                Caption = network.Caption,
                LogoImageView = network.LogoImage,
                ProductCategoryName = network.ProductCategory?.Caption
            };
            return networkModel;
        }

        public Network ConvertFromViewModelToNetwork(NetworkItemViewModel networkModel)
        {
            var network = new Network()
            {
                Id = networkModel.Id,
                Caption = networkModel.Caption,
                LogoImage = networkModel.LogoImageView
            };
            return network;
        }

        public async Task<Network> CreateNetworkAsync(Network network)
        {
            network.Id = Guid.NewGuid();
            _context.Add(network);
            await _context.SaveChangesAsync();
            return network;
        }

        public async Task<Network> CreateNetworkAsync(NetworkItemViewModel networkModel)
        {
            var network = new Network
            {
                Caption = networkModel.Caption,
                Id = Guid.NewGuid(),
                MerchantUsers = new List<ApplicationUser>(),
                ProductCategory = _context.ProductCategories.FirstOrDefault(item => item.Id == networkModel.ProductCategoryId)

            };
            if (networkModel.LogoImage != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await networkModel.LogoImage.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        network.LogoImage = memoryStream.ToArray();
                    }
                }
            }
            _context.Add(network);
            await _context.SaveChangesAsync();
            return network;

        }

        public async Task DeleteNetworkAsync(Guid Id)
        {
            var network = await _context.Networks.SingleOrDefaultAsync(m => m.Id == Id);
            _context.Networks.Remove(network);
            await _context.SaveChangesAsync();
        }

        public async Task<Network> GetNetwork(Guid Id)
        {
            var network = await _context.Networks.Include(item => item.ProductCategory)
               .SingleOrDefaultAsync(m => m.Id == Id);
            return network;
        }

        public async Task<NetworkItemViewModel> GetNetworkItemViewModelAsync(Guid Id)
        {
            var network = await _context.Networks.Include(item => item.ProductCategory)
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (network == null)
            {
                return null;
            }
            var networkModel = new NetworkItemViewModel
            {
                Id = network.Id,
                Caption = network.Caption,
                LogoImageView = network.LogoImage,
                ProductCategories = GetSelectListProductCategories(),
                ProductCategoryId = network.ProductCategory.Id,
                ProductCategoryName = network.ProductCategory.Caption
            };
            return networkModel;
        }

        public async Task<NetworkItemViewModel> GetNetworkProductCategoryListItemViewModelAsync()
        {
            var networkModel = new NetworkItemViewModel();
            networkModel.ProductCategories = GetSelectListProductCategories();
            return networkModel;
        }

        public async Task<List<Network>> GetNetworksAsync()
        {
            var networks = await _context.Networks.ToListAsync();
            return networks;
        }

        public async Task<List<Network>> GetNetworksByIdListAsync(List<Guid> guids)
        {
            var networks = _context.Networks.Where(x => guids.Contains(x.Id));
            return networks.ToList();
        }


        public async Task<List<NetworkItemViewModel>> GetNetworkViewModelsAsync()
        {
            var networks = await _context.Networks.Include(item => item.ProductCategory).ToListAsync();
            var networksListViewModel = new List<NetworkItemViewModel>(networks.Count);
            networksListViewModel = networks.Select(item => new NetworkItemViewModel
            {
                Id = item.Id,
                Caption = item.Caption,
                LogoImageView = item.LogoImage,
                ProductCategoryName = item.ProductCategory.Caption,
                ProductCategoryId = item.ProductCategory.Id,
                ProductCategories = GetSelectListProductCategories()

            }).ToList();
            return networksListViewModel;
        }

        public bool IsExist(Guid Id)
        {
            return _context.Networks.Any(e => e.Id == Id);
        }

        public async Task UpdateNetworkAsync(Network network)
        {
            _context.Update(network);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNetworkAsync(NetworkItemViewModel networkModel)
        {
            var network = await GetNetwork(networkModel.Id);
            network.Caption = networkModel.Caption;
            network.ProductCategory = _context.ProductCategories.FirstOrDefault(item => item.Id == networkModel.ProductCategoryId);

            if (networkModel.LogoImage != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await networkModel.LogoImage.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        network.LogoImage = memoryStream.ToArray();
                    }
                }
            }
            _context.Update(network);
            await _context.SaveChangesAsync();
        }

        public SelectList GetSelectListProductCategories()
        {
            var productCategories = _context.ProductCategories.Select(x => new { Id = x.Id, Value = x.Caption });

            var productCategoriesSelectList = new SelectList(productCategories, "Id", "Value");
            return productCategoriesSelectList;

        }

        public async Task<Network> GetNetworkWithAdressesAsync(Guid Id)
        {
            var network = await _context.Networks.Include(item => item.ProductCategory).Include(item=>item.NetworkPoints)
               .SingleOrDefaultAsync(m => m.Id == Id);
            return network;
        }

        public async Task<List<Network>> GetNetworksByProductCategoryAsync(Guid id)
        {
            var network = await _context.Networks.Where(item => item.ProductCategory.Id == id).ToListAsync();
            return network;
        }

        public SelectList GetSelectListNetworks()
        {
            var networks = _context.Networks.ToList();
            var networksSelectList = new SelectList(networks, "Id", "Caption");
            return networksSelectList;
        }

    }
}
