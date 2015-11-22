using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using TORNEOS.Model;
using TORNEOS.Model.Commands;
using AutoMapper;

namespace TORNEOS.Test
{
    [TestClass()]
    public class WebAPITest
    {
        public TestContext TestContext { get; set; }

        public string URLServer { get; set; }

        public WebAPITest()
        {
            this.URLServer = "http://localhost:1233/";
        }

        [TestMethod()]
        public void CategoryGetTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.URLServer);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                HttpResponseMessage response = client.GetAsync("api/torneo/get").Result;
                Assert.IsTrue(response.IsSuccessStatusCode, "Error: Unable to Connect to Server");
                if (response.IsSuccessStatusCode)
                {
                    List<DTOTorneo> offers = response.Content.ReadAsAsync<List<DTOTorneo>>().Result;
                    Assert.IsTrue(offers != null, "Error: No Torneo Returned by Server");
                }
            }
        }

        [TestMethod()]
        public void CategoryDetailTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.URLServer);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                HttpResponseMessage response = client.GetAsync("api/torneo/get/1").Result;
                Assert.IsTrue(response.IsSuccessStatusCode, "Error: Unable to Connect to Server");
                if (response.IsSuccessStatusCode)
                {
                    DTOTorneo category = response.Content.ReadAsAsync<DTOTorneo>().Result;
                    Assert.IsTrue(category != null, "Error: No Torneo Returned by Server");

                }
            }


        }
    }
}
