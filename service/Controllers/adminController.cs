using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace service.Controllers
{
    public class adminController : Controller
    {
        public IActionResult Index(DataBase dataBase,Book_services book_Services)
        {
            if(TempData.Peek("Admin_id")==null)
            {
                return RedirectToAction("Login");
            }

            DataSet uc = dataBase.user_count();     //uc = user count
            DataSet pc = dataBase.provider_count();     //pc = provider count

            DataSet Book_order = book_Services.Get_user_Booking_Details_admin();
            ViewBag.book_services = Book_order.Tables[0];

            TempData["total_user"] = uc.Tables[0].Rows.Count;
            TempData["total_provider"] = pc.Tables[0].Rows.Count;

            return View();
        }
        [HttpGet]
        public IActionResult categories()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> categories(category category,IFormFile formFile)
        {
            var imgname = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "categoryimage", formFile.FileName);
            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }


            int record = category.addcategory(category.cat_name, imgname);

            if (record > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("");
            }
            return View();
        }

        [HttpGet]
        public IActionResult services(category category,services services)
        {
            DataSet ds = category.select();
            ViewBag.DataSource = ds.Tables[0];

            DataSet pd = services.select_provider();    //pd = provider_data
            ViewBag.provider_data = pd.Tables[0];

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> services(services services, IFormFile formFile)
        {
            var imgname = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "servicesimage", formFile.FileName);
            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            int record = services.addservices(services.name,imgname,services.price,services.contactno,services.cat_name,services.provider_id);

            if (record > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("");
            }
            return View();
        }

        public IActionResult View_services(services services)
        {

            DataSet ds = services.getservices();
            ViewBag.Services = ds.Tables[0];

            return View();
        }

        public IActionResult login()
        {
            return View();
        }
        public IActionResult serviceprovider()
        {
            return View();
        }
        public IActionResult subcategories()
        {
            return View();
        }

        public IActionResult demo()
        {
            return View();
        }

        public IActionResult provider(providerModel providerModel)
        {

            DataSet pdd = providerModel.provider_details();     // provider data details
            ViewBag.provider_details = pdd.Tables[0];
            return View();
        }

        [HttpPost]
        public IActionResult login(DataBase dataBase)
        {
            DataSet ds = dataBase.LoginAdmin(dataBase.email,dataBase.password);
            ViewBag.admin_data = ds.Tables[0];

            foreach (System.Data.DataRow dr in ViewBag.Admin_data.Rows)
            {
                TempData["Admin_id"] = dr["id"].ToString();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult update_provider_status(int provider_id,providerModel providerModel)
        {
            int record = providerModel.update_provider_status(provider_id);
            return RedirectToAction("provider");
        }


        public IActionResult update_services_status(int services_id, services services)
        {
            int record = services.update_services_status(services_id);

            return RedirectToAction("View_services");
        }

       

    }
}
