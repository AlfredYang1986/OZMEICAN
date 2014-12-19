using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PayPalComponent
{
    class PayPalRestfulClient : IDisposable
    {
        public void Dispose() { }

        public static PayPalRestfulClient getPayPalClientInstance() { return _instance; }
        private static PayPalRestfulClient _instance = new PayPalRestfulClient();
        private PayPalRestfulClient() {}

        //private JArray _testArray = new JArray();
        //private string access_token;

        /************************************************************************/
        /* 1.the first step is to get paypal access token directly              */
        /************************************************************************/
        public async Task<string> paypal_access_token_2(string clientID, string scret)
        {

            // 'HTTP Basic Auth Post' <http://stackoverflow.com/questions/21066622/how-to-send-a-http-basic-auth-post>
            string oAuthCredentials = Convert.ToBase64String(Encoding.Default.GetBytes(clientID + ":" + scret));
            string uriString = "https://api.sandbox.paypal.com/v1/oauth2/token";

            HttpClient client = new HttpClient();

            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Post, uriString);
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Basic", oAuthCredentials);
            h_request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            h_request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

            h_request.Content = new StringContent("grant_type=client_credentials", UTF8Encoding.UTF8, "application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage response = await client.SendAsync(h_request);

                //if call failed ErrorResponse created...simple class with response properties
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    // TODO: error handling
                }

                dynamic t = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                System.Console.WriteLine(t);
                //access_token = t.access_token;
                return t.access_token;
            }
            catch (Exception)
            {
                throw new HttpRequestException("Request to PayPal Service failed.");
            }
        }

        /************************************************************************/
        /* 1.1 get authorization code                                           */
        /************************************************************************/
        public void paypal_get_grand_code(string clientid, string redirctUrl)
        {
            string uriString = string.Format("https://www.sandbox.paypal.com/webapps/auth/protocol/openidconnect/v1/authorize?client_id={0}&response_type=code&redirect_uri={1}", clientid, redirctUrl);

            var request = WebRequest.Create(uriString);
            request.GetResponse();
        }
        
        /************************************************************************/
        /* 2. create a payment                                                  */
        /************************************************************************/
        public async Task<string> paypal_create_payment(string access_token, string amount
                                        , string des, string returnUrl = @"http://localhost"
                                        , string cancelUrl = @"http://localhost"
                                        , string currency = "AUD")
        {
            dynamic amount_par = new JObject();
            amount_par.total = amount;
            amount_par.currency = currency;

            dynamic one_trans = new JObject();
            one_trans.description = des;
            one_trans.amount = amount_par;

            var trans_array = new JArray();
            trans_array.Add(one_trans);

            dynamic rediect = new JObject();
            rediect.return_url = returnUrl;
            rediect.cancel_url = cancelUrl;

            dynamic payer = new JObject();
            payer.payment_method = @"paypal";

            dynamic cp = new JObject();
            cp.intent = @"sale";
            cp.redirect_urls = rediect;
            cp.payer = payer;
            cp.transactions = trans_array;

            System.Console.WriteLine(JsonConvert.SerializeObject(cp));

            HttpClient client = new HttpClient();
            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Post, @"https://api.sandbox.paypal.com/v1/payments/payment");
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            h_request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            h_request.Content = new StringContent(JsonConvert.SerializeObject(cp), UTF8Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.SendAsync(h_request);

                //if call failed ErrorResponse created...simple class with response properties
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    // TODO: error handling
                }

                //dynamic t = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                //System.Console.WriteLine(t.links);
                //string confirmUrl = null;
                //foreach (dynamic link in t.links)
                //{
                //    if (link.rel == @"approval_url")
                //    {
                //        confirmUrl = link.href;
                //    }
                //}
                //_testArray = t.links;
                //return confirmUrl;
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw new HttpRequestException("Request to PayPal Service failed.");
            }
        }

        /************************************************************************/
        /* 3. execute a payment                                                 */
        /************************************************************************/
        public async Task<string> paypal_execute_payment(string access_token, string executeUrl, string payerid)
        {
            /**
             * 3.1 get payment execute url
             */
            //string executeUrl = null;
            //foreach (dynamic link in _testArray)
            //{
            //    if (link.rel == @"approval_url")
            //    {
            //        executeUrl = link.href;
            //    }
            //}

            HttpClient client = new HttpClient();

            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Post, executeUrl);
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            h_request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            dynamic cp = new JObject();
            cp.payer_id = payerid;
            h_request.Content = new StringContent(JsonConvert.SerializeObject(cp), UTF8Encoding.UTF8, "application/json");

            var strTest = @"";
            try
            {
                HttpResponseMessage response = await client.SendAsync(h_request);

                //if call failed ErrorResponse created...simple class with response properties
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    // TODO: error handling
                }

                strTest = await response.Content.ReadAsStringAsync();

                dynamic t = JsonConvert.DeserializeObject(strTest);
                System.Console.WriteLine(t);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                //throw new HttpRequestException("Request to PayPal Service failed.");
                return strTest;
            }

            //return @"success";
            return @"success";
        }

        /************************************************************************/
        /* 4. list all payments                                                 */
        /************************************************************************/
        public async Task<string> paypal_list_payments(string access_token)
        {
            HttpClient client = new HttpClient();
            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Get, @"https://api.sandbox.paypal.com/v1/payments/payment");
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            h_request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.SendAsync(h_request);

                //if call failed ErrorResponse created...simple class with response properties
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    // TODO: error handling
                }

                var strTest = await response.Content.ReadAsStringAsync();

                dynamic t = JsonConvert.DeserializeObject(strTest);
                System.Console.WriteLine(t);
                return strTest;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                throw new HttpRequestException("Request to PayPal Service failed.");
            }
        }
    }
}
