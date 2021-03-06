﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADVCoupon.Models;
using AVDCoupon.Data;
using ADVCoupon.Services;
using ADVCoupon.ViewModel.NetworkViewModels;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using ADVCoupon.Helpers;

namespace ADVCoupon.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = Constants.ADMIN_ROLE)]
    public class NetworksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INetworkService _service;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly INetworkPointService _networkPointService;

        public NetworksController(ApplicationDbContext context, INetworkPointService networkPointService, INetworkService service, IHostingEnvironment hostingEnvironment)
        {
            _service = service;
            _context = context;
            _networkPointService = networkPointService;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Networks
        public async Task<IActionResult> Index()
        {
            var networksModel = await _service.GetNetworkViewModelsAsync();
            return View(networksModel);        
        }

        // GET: Networks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkModel = await _service.GetNetworkItemViewModelAsync(id.Value);
            if (networkModel == null)
            {
                return NotFound();
            }
            return View(networkModel);
        }

        // GET: Networks/Create
        public async Task<IActionResult> Create()
        {
            var productCategoriesModel = await _service.GetNetworkProductCategoryListItemViewModelAsync();
            return View(productCategoriesModel);
        }

        // POST: Networks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NetworkItemViewModel networkModel)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateNetworkAsync(networkModel);
                return RedirectToAction(nameof(Index));
            }
            networkModel.ProductCategories = _service.GetSelectListProductCategories();
            return View(networkModel);
        }

        // GET: Networks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkModel = await _service.GetNetworkItemViewModelAsync(id.Value);
            if (networkModel == null)
            {
                return NotFound();
            }
            return View(networkModel);
        }

        // POST: Networks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NetworkItemViewModel networkModel)
        {
            if (id != networkModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateNetworkAsync(networkModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetworkExists(networkModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            networkModel.ProductCategories = _service.GetSelectListProductCategories();
            return View(networkModel);
        }

        // GET: Networks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkModel = await _service.GetNetworkItemViewModelAsync(id.Value);
            if (networkModel == null)
            {
                return NotFound();
            }

            return View(networkModel);
        }

        // POST: Networks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteNetworkAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Import(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.NetworkId = id;
            
            return View();
        }
        
        public async Task<ActionResult> OnPostImport()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                var networkId = Request.Form["networkId"].ToString();

                //fdata.append('networkName', $('#networkName').val());
                //fdata.append('networkNameCell', $('#networkNameCell').val());
                //fdata.append('nameCell', $('#nameCell').val());
                //fdata.append('cityCell', $('#cityCell').val());
                //fdata.append('addressCell', $('#addressCell').val());
                //fdata.append('regionCell', $('#regionCell').val());

                int nameCell = Convert.ToInt32(Request.Form["nameCell"]);
                int addressCell = Convert.ToInt32(Request.Form["addressCell"].ToString());
                int cityCell = Convert.ToInt32(Request.Form["cityCell"].ToString());
                int regionCell = Convert.ToInt32(Request.Form["regionCell"].ToString());
                int networkNameCell = Convert.ToInt32(Request.Form["networkNameCell"].ToString());
                string networkName = Request.Form["networkName"].ToString();

                //int nameCell = 2;
                //int addressCell = 4;
                //int cityCell = 3;
                //int regionCell = 5;
                //int networkNameCell = 1;
                //string networkName = "АНЦ";

                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                StringBuilder sb = new StringBuilder();
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                var network = await _service.GetNetwork(new Guid(networkId));

                if (file.Length > 0)
                {
                    string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                    ISheet sheet;
                    string fullPath = Path.Combine(newPath, file.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;
                        if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }
                        IRow headerRow = sheet.GetRow(0); //Get Header Row
                        int cellCount = headerRow.LastCellNum;
                        
                        var networkPointList = new List<NetworkPoint>();

                        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                        {

                            IRow row = sheet.GetRow(i);
                            if (row == null) continue;
                            if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                            var currentPoint = new NetworkPoint()
                            {
                                Network = network
                            };

                            var currentNetworkName = row.GetCell(networkNameCell)?.ToString();

                            if(currentNetworkName != networkName)
                            {
                                continue;
                            }

                            currentPoint.Name = row.GetCell(nameCell).ToString();

                            currentPoint.Geoposition = new Geoposition()
                            {
                                Country = "Україна",
                                City = row.GetCell(cityCell)?.ToString(),
                                Region = row.GetCell(regionCell)?.ToString(),
                                Address = row.GetCell(addressCell)?.ToString()
                            };

                            currentPoint.Geoposition = await Helpers.GeocodingHelper.GetCoordinatesByAddressAsync(currentPoint.Geoposition);
                            networkPointList.Add(currentPoint);
                        }

						await _networkPointService.AddNetworkPoints(new Guid(networkId), networkPointList);

                    }

                    Directory.Delete(newPath, true);
                }
                return Content("Import finished.");

            }
            catch(Exception ex)
            {
                return Content("Import failed." + ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<PartialViewResult> IndexGrid()
        {
            var networksModel = await _service.GetNetworkViewModelsAsync();

            return PartialView("_IndexGrid", networksModel);
        }

        private bool NetworkExists(Guid id)
        {
            return _service.IsExist(id);
        }


        // it's possible to provide dynamic headers / keep in mind. AO

        //for (int j = 0; j < cellCount; j++)
        //{
        //    ICell cell = headerRow.GetCell(j);
        //    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;

        //    sb.Append("<th>" + cell.ToString() + "</th>");
    }

    //public class ProgressHub : Hub
    //{
    //    public void NotifyStart(string taskId)
    //    {
    //        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
    //        hubContext.Clients.All.initProgressBar(taskId);
    //    }
    //    public void NotifyProgress(string taskId, int percentage)
    //    {
    //        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
    //        hubContext.Clients.All.updateProgressBar(taskId, percentage);
    //    }
    //    public void NotifyEnd(string taskId)
    //    {
    //        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
    //        hubContext.Clients.All.clearProgressBar(taskId);
    //    }
    //}
}
