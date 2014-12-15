using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OZMeiCan.Entity;

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
            for (int index = 0; index < 9; ++index)
            {
                dynamic tmp = new JObject();
                tmp.name = String.Format(@"dish{0}", index);
                reVal.Add(tmp);
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
    }
}
