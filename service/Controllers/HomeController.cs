using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace service.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /* Register user */

        public IActionResult Register(userModel usermodel)
        {
            int record = usermodel.user_register(usermodel.name, usermodel.email, usermodel.password);

            if (record > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Login(userModel userModel)
        {
            DataSet ds = userModel.Login(userModel.email, userModel.password);
            ViewBag.user_data = ds.Tables[0];

            foreach (System.Data.DataRow dr in ViewBag.user_data.Rows)
            {
                TempData["user_id"] = dr["id"].ToString();
                TempData["Name"] = dr["user_name"].ToString();
                TempData["Email"] = dr["email"].ToString();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult logout()
        {
            TempData.Remove("user_id");
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> provider_registration(providerModel providerModel,IFormFile  formFile)
        {
            var imgname = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profileimage", formFile.FileName);
            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }


            int record = providerModel.provider_register(providerModel.name, providerModel.email, providerModel.password,imgname);

            if (record > 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult provider_login(provider_model provider_Model)
        {
            DataSet ds = provider_Model.Login(provider_Model.email, provider_Model.password);
            ViewBag.user_data = ds.Tables[0];

            foreach(System.Data.DataRow dr in ViewBag.user_data.Rows)
            {
                if (dr["p_status"].ToString() == "0")
                {
                    ViewBag.status = "0";
                    ViewBag.status1 = "Your Account is not Activeted";
                    return RedirectToAction("Index");
                }
                else 
                {
                    TempData["provider_id"] = dr["id"].ToString();
                    return RedirectToAction("provider_dashboard");
                }
             
            }
            return RedirectToAction("Index");
        }

        public IActionResult logout_provider()
        {
            TempData.Remove("provider_id");
            return RedirectToAction("Index");
        }

  

        public IActionResult provider_dashboard(providerModel providerModel)
        {
            string provider_id = TempData.Peek("provider_id").ToString();

            DataSet bookig_details = providerModel.get_booking_details(provider_id);
            int count_booking = bookig_details.Tables[0].Rows.Count;
            ViewBag.Order_count = count_booking;

            DataSet services_details= providerModel.get_services_details(provider_id);
            int count_services = services_details.Tables[0].Rows.Count;
            ViewBag.Services_count = count_services;

            string id = TempData.Peek("provider_id").ToString();

            DataSet ds = providerModel.Getproviderinfo(id);
            ViewBag.ProviderInfo = ds.Tables[0];

            foreach (System.Data.DataRow dr in ViewBag.ProviderInfo.Rows)
            {
            
                TempData["name"] = dr["provider_name"].ToString();
                TempData["imagename"] = dr["provider_image"].ToString();
            }

            return View();
        }

        public IActionResult Index(category category)
        {

            if (TempData.Peek("provider_id") != null)
            {
                return RedirectToAction("provider_dashboard");
            }

            DataSet ds = category.select();
            ViewBag.DataSource = ds.Tables[0];

            return View();
        }

     
        public IActionResult categories(category category)
        {
            DataSet ds = category.select();
            ViewBag.DataSource = ds.Tables[0];
            return View();
        }

        public IActionResult search(services services, string id)
        {
            DataSet ds = services.select(id);
            ViewBag.DataSource = ds.Tables[0];
            ViewBag.Totalservices = ViewBag.DataSource.Rows.Count;
            return View();
        }

        [HttpPost]
        public IActionResult search(services services)
        {
            DataSet ds = services.select(services.src_keyword);
            ViewBag.DataSource = ds.Tables[0];
            ViewBag.Totalservices = ViewBag.DataSource.Rows.Count;
            return View();
        }

        public IActionResult services_provider(providerModel providerModel)
        {
            string provider_id = TempData.Peek("provider_id").ToString();

            DataSet ds = providerModel.getservices(provider_id,1);
            ViewBag.ActiveService = ds.Tables[0];

            return View();
        }

        public IActionResult inactive_services(providerModel providerModel)
        {

            string provider_id = TempData.Peek("provider_id").ToString();

            DataSet ds = providerModel.getservices(provider_id,0);
            ViewBag.InActiveService = ds.Tables[0];

            return View();
        }

        [HttpGet]
        public IActionResult Add_services(services services,category category)
        {

            DataSet ds = category.select();
            ViewBag.DataSource = ds.Tables[0];

            DataSet pd = services.select_provider();    //pd = provider_data
            ViewBag.provider_data = pd.Tables[0];


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add_services(services services, IFormFile formFile)
        {
            var imgname = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "servicesimage", formFile.FileName);
            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            int id = services.provider_id;

            int record = services.addservices(services.name, imgname, services.price, services.contactno, services.cat_name, id);

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
        public IActionResult book_servives(services services,int services_id)
        {
            DataSet ts = services.GetTime();
            ViewBag.Time_sloat = ts.Tables[0];
          
            DataSet ds = services.select_services_details(services_id);
            ViewBag.Services_Details = ds.Tables[0];

            foreach (System.Data.DataRow dr in ViewBag.Services_Details.Rows)
            {
                ViewBag.price = dr["price"].ToString();
                ViewBag.name = dr["name"].ToString();
                TempData["services_id"] = dr["id"].ToString();
                TempData["provider_services_id"] = dr["provider_id"].ToString();
            }

            return View();
        }

        [HttpPost]
        public IActionResult book_servives(Book_services book_Services)
        {
            string user_id = TempData.Peek("user_id").ToString();
            string services_id = TempData.Peek("services_id").ToString();
            string provider_id = TempData.Peek("provider_services_id").ToString();

            int record = book_Services.Book_user_services(book_Services.date,services_id,book_Services.phone_no,user_id,book_Services.location,provider_id);

            if (record > 0)
            {
                return RedirectToAction("Index");
            }
           
            return View();
        }

        public IActionResult Booking_list(Book_services book_Services)
        { 
            string provider_id = TempData.Peek("provider_id").ToString();
            DataSet Book_order = book_Services.Get_user_Booking_Details(provider_id);
            ViewBag.book_services = Book_order.Tables[0];

            return View();
        }

        public IActionResult update_user_services_status(Book_services book_Services,int services_id)
        {
            int record = book_Services.Update_status(services_id);
            return RedirectToAction("Booking_list");
        }

        public IActionResult update_complte_status(Book_services book_Services,int services_id)
        {
            int record = book_Services.Update_status_complete(services_id);
            return RedirectToAction("Booking_list");
        }

        public IActionResult update_services_status_cancel(Book_services book_Services, int services_id)
        {
            int record = book_Services.Update_status_cancel(services_id);
            return RedirectToAction("Booking_list");
        }

        public IActionResult customers()
        {
            return View();
        }
        public IActionResult favourites()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult servicedetails(int services_id,services services)
        {
            DataSet ds = services.select_details(services_id);
            ViewBag.DataSource = ds.Tables[0];
            return View();
        }
        public IActionResult booking()
        {
            return View();
        }
        public IActionResult profile()
        {
            return View();
        }
        public IActionResult wallet()
        {
            return View();
        }
        public IActionResult reviews()
        {
            return View();
        }

        public IActionResult payment()
        {
            return View();
        }
        public IActionResult add_service()
        {
            return View();
        }
        public IActionResult about()
        {
            return View();
        }

        public IActionResult contact()
        {
            return View();
        }

        public IActionResult demo()
        {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
