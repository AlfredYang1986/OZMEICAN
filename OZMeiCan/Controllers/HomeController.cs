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
        public JsonResult getRTContent(String cor, int subID)
        {
            JArray reVal = new JArray();
            using (var db = new IGoDeliverEntities())
            {
                var result = from r in db.Restaurant
                             from s in db.Suburb
                             where s.Id == subID && s.Geolocation.Contains(r.Geolocation)
                             select r;

                foreach (var rest in result)
                {
                    dynamic tmp = new JObject();
                    tmp.name = rest.RestaurantName;
                    tmp.dish = rest.Description;

                    reVal.Add(tmp);
                }
            }

            return Json(JsonConvert.SerializeObject(reVal), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getFastPayDishContent()
        {
            JArray reVal = new JArray();
            using (var db = new IGoDeliverEntities())
            {
                var result = from iter in db.Dish
                             where iter.isRecommended.HasValue && iter.isRecommended.Value == true
                             select iter;

                foreach (var index in result)
                {
                    dynamic tmp = new JObject();
                    tmp.name = index.Name;
                    tmp.price = index.Price;
                    tmp.ID = index.Id;
                    tmp.restName = index.Restaurant.RestaurantName;

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
                    tmp.ID = index.Id;
                    tmp.restName = index.Restaurant.RestaurantName;

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
                        sbu.subID = sb.Id;

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

        public JsonResult saveOrder(string orderData, double total) 
        {
            dynamic arr = JsonConvert.DeserializeObject(orderData) as JArray;

            using (var db = new IGoDeliverEntities())
            {
                var order = new DeliveryOrder();
                order.PriceTotal = total;
                db.DeliveryOrder.Add(order);
                //db.SaveChanges();

                foreach (var iter in arr)
                {
                    var orderRow = new DeliverOrderRow();
                    orderRow.OrderId = order.OrderId;
                    orderRow.DishId = iter.dishID;
                    orderRow.SubTotal = iter.subTotal;
                    orderRow.DeliveryCost = 0.0;
                    orderRow.Distance = 0.0;
                    //orderRow.Count = iter.count;

                    db.DeliverOrderRow.Add(orderRow);
                }

                order.OrderDateTime = System.DateTime.Now;
                order.Status = @"A";

                db.SaveChanges();
            }
            
            return Json(JsonConvert.SerializeObject("success"), JsonRequestBehavior.AllowGet);
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
