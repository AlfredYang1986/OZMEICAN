using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PayPalComponent
{
    public class PayPalPaymentFacad
    {
        public static IDictionary<string, JObject> _dic = new Dictionary<string, JObject>();
        private static string clientid = @"AY_A0xDHhUk_60Sin0IbCEwYWNFyyh0VBnke55ebkEp4fHFfoZyqrzfuoMcE";
        private static string scret = @"EKe1FhCvKYDHfKxHTw1JGfhJrjNOKgXfSYqPCeplUmxPR3X0sH05JCv-mkuT";

        /************************************************************************/
        /* three step for one payment                                           */
        /* 1. create payment                                                    */
        /* 2. get approvel url and let user pay with paypal                     */
        /* 3. execute the payment by execute url                                */
        /************************************************************************/
        public async static Task<string> createPayment(//string clientID, string scret,
                                                       string amount, string des
                                                     , string returnUrl = @"http:\\localhost"
                                                     , string cancelUrl = @"http:\\localhost")
        {
            using (var client = PayPalRestfulClient.getPayPalClientInstance())
            {
                var access_token = await client.paypal_access_token_2(clientid, scret);
                var create_result = await client.paypal_create_payment(access_token, amount, des, returnUrl, cancelUrl);
                dynamic reVal = JsonConvert.DeserializeObject(create_result);
                //_dic.Add(getApproalTokenFromResult(reVal), reVal);
                _dic.Add(access_token, reVal);
                return getApproalUrl(reVal.links);
            }
        }

        public async static Task<string> executePayment(string token, string payerID)
        {
            using (var client = PayPalRestfulClient.getPayPalClientInstance())
            {
                string access_token = @"";
                var url = getExecuteUrlAndAccessTokenByToken(ref access_token, token);

                return await client.paypal_execute_payment(access_token, url, payerID);
            }
        }

        private static string getApproalUrl(JArray arr)
        {
            foreach (dynamic link in arr)
                if (link.rel == @"approval_url")
                    return link.href;

            return null;
        }

        private static string getExecuteUrl(JArray arr)
        {
            foreach (dynamic link in arr)
                if (link.rel == @"execute")
                    return link.href;

            return null;
        }

        private static string getExecuteUrlAndAccessTokenByToken(ref string access_token, string token)
        {
            var result = from item in _dic
                         where getApproalTokenFromResult(item.Value) == token
                         select item;

            if (result.Count() > 0)
            {
                var re = result.FirstOrDefault();
                access_token = re.Key;
                dynamic reObj = re.Value;
                return getExecuteUrl(reObj.links);
            }
            return null;
        }

        private static string getApproalTokenFromResult(dynamic result)
        {
            string url = getApproalUrl(result.links);
            return url.Split('=').Last().Trim();
        }

        public async static Task<string> listAllPayments()
        {
            using (var client = PayPalRestfulClient.getPayPalClientInstance())
            {
                var access_token = await client.paypal_access_token_2(clientid, scret);
                return await client.paypal_list_payments(access_token);
            }
        }
    }
}
