using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OZMeiCan.Entity;
using PayPalComponent;

namespace OZMeiCan.Controllers
{
    public class HomeController : Controller
    {
        /************************************************************************/
        /* cor is sub name                                                      */
        /*    now only list all the rest                                        */
        /************************************************************************/
        public JsonResult getRTContent(String cor)
        {
            JArray reVal = new JArray();
            using (var db = new IGoDeliverEntities())
            {
                foreach (var rest in db.Restaurant)
                {
                    dynamic tmp = new JObject();
                    tmp.name = rest.RestaurantName;
                    tmp.dish = rest.Description;

                    reVal.Add(tmp);
                }
            }

            return Json(JsonConvert.SerializeObject(reVal), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getDishContent(String name, String cor)
        {
            JArray reVal = new JArray();
            using (var db = new IGoDeliverEntities())
            {
                var result = from it in db.Dish
                             where it.Restaurant.RestaurantName == name
                             select it;

                foreach (var index in result)
                {
                    dynamic tmp = new JObject();
                    tmp.name = index.Name;
                    tmp.price = index.Price;
                    reVal.Add(tmp);
                }
            }

            return Json(JsonConvert.SerializeObject(reVal), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getJsonData()
        {
            JArray provences = new JArray();
            using (var db = new IGoDeliverEntities())
            {
                var result = db.StateProvince;
                foreach (var pro in result)
                {
                    dynamic provence = new JObject();
                    provence.shot = String.Format(@"{0}", pro.Name.First());
                    provence.name = pro.Name;

                    JArray suburb = new JArray();
                    foreach (var sb in pro.Suburb)
                    {
                        dynamic sbu = new JObject();
                        sbu.shot = String.Format(@"{0}", sb.Name.First());
                        sbu.name = sb.Name;

                        suburb.Add(sbu);
                    }
                    provence.subs = suburb;
                    provences.Add(provence);
                }
            }

            return Json(JsonConvert.SerializeObject(provences), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        /************************************************************************/
        /* paypal                                                               */
        /************************************************************************/

        // Get: /PayPalPayment

        public async Task<ActionResult> PayPalPayment(double price)
        {
            var approvalUrl = await PayPalPaymentFacad.createPayment(price.ToString("F"), @"Alfred Test"
                                        , @"http://localhost:2444/Home/ConfirmPayment"
                                        , @"http://localhost:2444/Home/CancelPayment");
            return Redirect(approvalUrl);
        }

        // Get: /ConfirmPayment

        public async Task<string> ConfirmPayment(string token, string PayerID)
        {
            return await PayPalPaymentFacad.executePayment(token, PayerID);
        }

        // Get: /CancelPayment

        public ActionResult CancelPayment()
        {
            return null;
        }

        // Get: /ListAllPayments

        public async Task<string> ListAllPayments()
        {
            return await PayPalPaymentFacad.listAllPayments();
        }
    }
}
