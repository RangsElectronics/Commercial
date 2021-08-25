using AutoMapper;
using Commercial.Helpers;
using Commercial.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Commercial.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        private dbCommercialEntities db = new dbCommercialEntities();
        private readonly IMapper _mapper;
        public MasterController()
        {
            _mapper = AutoMapperProfiles.Mapper;
        }
        public ActionResult Index()
        {
            var masterList = db.tblMasters
                .Include("tblSupplier")
                .Include("tblBank").ToList();
            var viewList = new List<MasterViewModel>();
            viewList = _mapper.Map<List<MasterViewModel>>(masterList);
            return View(viewList);
        }
        public ActionResult Create()
        {
            MasterViewModel model = new MasterViewModel();
            model.BankList = db.tblBanks.ToList();
            model.SupplierList = db.tblSuppliers.ToList();
            model.DetailModel.ProductList = db.tblProducts.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterViewModel modelToSave)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tblMaster entity = new tblMaster();
                    entity = _mapper.Map<tblMaster>(modelToSave);
                    db.tblMasters.Add(entity);
                    db.SaveChanges();

                    foreach (var item in modelToSave.DetailList)
                    {
                        tblDetail detailEntity = new tblDetail();
                        detailEntity = _mapper.Map<tblDetail>(item);
                        detailEntity.MasterId = entity.Id;
                        db.tblDetails.Add(detailEntity);
                    }
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException.ToString();
            }
            modelToSave.BankList = db.tblBanks.ToList();
            modelToSave.SupplierList = db.tblSuppliers.ToList();
            modelToSave.DetailModel.ProductList = db.tblProducts.ToList();
            return View(modelToSave);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMaster tblmaster = db.tblMasters.Find(id);
            if (tblmaster == null)
            {
                return HttpNotFound();
            }
            MasterViewModel model = new MasterViewModel();
            model = _mapper.Map<MasterViewModel>(tblmaster);
            model.BankList = db.tblBanks.ToList();
            model.SupplierList = db.tblSuppliers.ToList();
            model.DetailModel.ProductList = db.tblProducts.ToList();

            List<tblDetail> detailList = db.tblDetails.Include("tblProduct").Where(x => x.MasterId == id).ToList();
            model.DetailList = _mapper.Map<List<DetailViewModel>>(detailList);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                tblMaster tblmaster = _mapper.Map<tblMaster>(model);
                db.Entry(tblmaster).State = EntityState.Modified;
                db.SaveChanges();

                if(model.DetailList.Count > 0)
                {
                    db.tblDetails.RemoveRange(db.tblDetails.Where(x => x.MasterId == tblmaster.Id).ToList());
                    foreach (var item in model.DetailList)
                    {
                        tblDetail detailToSave = _mapper.Map<tblDetail>(item);
                        db.tblDetails.Add(detailToSave);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                tblMaster tblBank = db.tblMasters.Find(id);
                if(tblBank != null)
                {
                    List<tblDetail> details = db.tblDetails.Where(x => x.MasterId == id).ToList();
                    foreach (var item in details)
                    {
                        db.tblDetails.Remove(item);
                    }
                }
                db.tblMasters.Remove(tblBank);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            
            return RedirectToAction("Index");
        }

        public ActionResult ViewReport(int id)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            try
            {
                List<vw_MasterDetail> dataList = db.vw_MasterDetail.Where(x => x.Id == id).ToList();
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RptMasterDetail.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsMasterDetail", dataList));
                ViewBag.ReportViewer = reportViewer;
            }
            catch (Exception ex)
            {

                throw;
            }


            return View();
        }

        public PartialViewResult AddProductDetail(DetailViewModel model)
        {
            var master = new MasterViewModel();
            string prodName = db.tblProducts.FirstOrDefault(x => x.Id == model.ProductId).Name;
            model.ProductName = prodName;
            master.DetailList.Add(model);

            return PartialView("_Detail", master);
        }

        [HttpPost]
        public JsonResult GetProductInfo(int productId)
        {
            string msg = string.Empty;
            try
            {
                var obj = db.tblProducts.FirstOrDefault(x => x.Id == productId);
                if (obj != null)
                {
                    return Json(new
                    {
                        ProdName = obj.Name,
                        Description = obj.Description
                    });
                }
                else
                {
                    return Json(new { Result = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = false });
            }
        }
    }
}