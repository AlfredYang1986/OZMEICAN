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
                foreach (var rest in db.Restaurants)
                {
                    dynamic tmp = new JObject();
                    tmp.name = rest.RestaurantName;
                    tmp.dish = rest.Description;

                    reVal.Add(tmp);
                }
            }

            return Json(JsonConvert.SerializeObject(reVal), JsonRequestBehavior.AllowGet);

            //JArray reVal = new JArray();
            //for (int index = 0; index < 9; ++index)
            //{
            //    dynamic tmp = new JObject();
            //    tmp.name = String.Format(@"rest{0}", index);
            //    tmp.dish = String.Format(@"dish1, dish2, dish3");
            //    reVal.Add(tmp);
            //}

            //return Json(JsonConvert.SerializeObject(reVal), JsonRequestBehavior.AllowGet);
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
                var result = db.StateProvinces;
                foreach (var pro in result)
                {
                    dynamic provence = new JObject();
                    provence.shot = String.Format(@"{0}", pro.Name.First());
                    provence.name = pro.Name;

                    JArray suburb = new JArray();
                    foreach (var sb in pro.Suburbs)
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

            //for (int p_index = 0; p_index < 9; ++p_index)
            //{
            //    dynamic provence = new JObject();
            //    provence.shot = @"P";
            //    provence.name = String.Format(@"provence{0}", p_index);

            //    JArray cities = new JArray();
            //    for (int c_index = 0; c_index < 9; ++c_index)
            //    {
            //        dynamic city = new JObject();
            //        city.shot = @"C";
            //        city.name = String.Format("city{0}", c_index);

            //        JArray distrects = new JArray();
            //        for (int index = 0; index < 9; ++index)
            //        {
            //            dynamic dis = new JObject();
            //            dis.shot = @"D";
            //            dis.name = String.Format("distrect{0}", index);

            //            JArray buildings = new JArray();
            //            for (int b_index = 0; b_index < 9; ++b_index)
            //            {
            //                dynamic building = new JObject();
            //                building.shot = @"B";
            //                building.name = String.Format(@"building{0}", b_index);
            //                buildings.Add(building);
            //            }
            //            dis.subs = buildings;
            //            distrects.Add(dis);
            //        }
            //        city.subs = distrects;
            //        cities.Add(city);
            //    }
            //    provence.subs = cities;
            //    provences.Add(provence);
            //}

            //return Json(JsonConvert.SerializeObject(provences), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
