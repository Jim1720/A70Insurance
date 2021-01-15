using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Newtonsoft.Json;
using System.Net.Http;
using A70Insurance.Models;
using System.Net.Http.Json;

namespace A70Insurance.ClaimHelpers
{
    public  class ClaimAddHelper
    {
        private HttpClient http;
        private string url;

        public ClaimAddHelper(HttpClient Http, string url)
        {
            this.http = Http;
            this.url = url;
        }

        public async Task<(bool OK, string Message)> AddClaim(Claim claim)
        {
            string message = "";

            HttpResponseMessage m = await AddClaimApi(claim);

            var goodResult = m.IsSuccessStatusCode;

            if (goodResult == false)
            {
                message = "could not complete claim add read call -  unexpected result.";
                message += "status code:" + m.StatusCode.ToString() + " was returned. ";

                var status = m.StatusCode.ToString();
                if (status == "NotFound") { message = " not found."; }
                if (status == "ServerError") { message = "Server error."; } 

                return (false, message);
            }

             
            return (true, "Good  Claim Add Call");


        }


        private async Task<HttpResponseMessage> AddClaimApi(Claim claim)
        {

             

            var send = this.url;
            var suffix = "/api/claim/";
            var url = send + suffix;


            HttpContent item = JsonContent.Create<Claim>(claim);

            HttpRequestMessage req = new HttpRequestMessage()
            {

                RequestUri = new Uri(url),
                Method = new HttpMethod("Post"),
                Content = item
            };

            HttpResponseMessage m = null;

            try
            {
                m = await http.SendAsync(req);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Add Claim: Exception on call: " + ex.Message.ToString());

                //StateHasChanged();

                /* message = "could not complete plan read unexpected result.";
                 message += "status code:" + m.StatusCode.ToString() + " was returned. ";

                 var status = m.StatusCode.ToString();
                 if (status == "NotFound") { message = "Plans not found."; }
                 if (status == "ServerError") { message = "Server error."; } */

            }

            return m;


        }

    }
}
