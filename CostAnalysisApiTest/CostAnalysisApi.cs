using CostAnalysis.Models;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace CostAnalysisApiTest
{
    public class CostAnalysisApi
    {
        private readonly RestClient _client;

        public CostAnalysisApi(string url)
        {
            _client = new RestClient(url);
        }

        public IRestResponse<Day> Post(Day day)
        {
            var req = new RestRequest(Method.POST);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(JsonConvert.SerializeObject(day));
            return _client.Post<Day>(req);
        }

        public IRestResponse<Day> Get(DateTime date)
        {
            var req = new RestRequest(Method.GET);
            req.AddHeader("Content-Type", "application/json");
            req.AddParameter("date", date);
            return _client.Get<Day>(req);
        }
    }
}