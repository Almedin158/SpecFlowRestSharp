using Newtonsoft.Json;
using RestSharp;
using SpecFlowRestSharp.APIRequests;
using SpecFlowRestSharp.Hooks;
using NUnit.Framework;
using SpecFlowRestSharp.Utility;

namespace SpecFlowRestSharp.StepDefinitions
{
    [Binding]
    public class DeleteBookingStepDefinitions:DefaultHooks
    {
        RestResponse response;
        string token;
        string bookingId;

        [Given(@"The user is authenticated")]
        public void GivenTheUserIsAuthenticated()
        {
            string url = "https://restful-booker.herokuapp.com/auth";
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var body = new
            {
                username = "admin",
                password = "password123"
            };
            var request = new PostRequestBuilder()
                .WithUrl(url)
            .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(body))
                .Build();
            response = client.GetClient().Execute(request);

            var jsonObj = DynamicConverter.ConvertToJObject(response.Content);

            token = jsonObj.token;
        }

        [When(@"The user creates a booking")]
        public void WhenTheUserCreatesABooking()
        {
            string url = "https://restful-booker.herokuapp.com/booking/";
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" }
            };
            var body = new 
            {
                firstname = "MadeUp",
                lastname = "AlsoMadeUp",
                totalprice = 1508,
                depositpaid = true,
                bookingdates = new 
                {
                    checkin = DateTime.Now.ToString("yyyy'-'MM'-'dd"),
                    checkout = DateTime.Now.ToString("yyyy'-'MM'-'dd")
                },
                additionalneeds = "Dorucak"
            };
            var request = new PostRequestBuilder()
                .WithUrl(url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(body))
                .Build();
            response = client.GetClient().Execute(request);

            var jsonObj = DynamicConverter.ConvertToJObject(response.Content);

            bookingId = jsonObj.bookingid;
        }

        [When(@"The user performs a delete booking request")]
        public void WhenTheUserPerformsADeleteBookingRequest()
        {
            string url = "https://restful-booker.herokuapp.com/booking/{id}";
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Cookie", "token="+token }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", bookingId}
            };

            var request = new DeleteRequestBuilder()
                .WithUrl(url)
                .WithHeaders(headers)
            .WithUrlSegments(urlSegments)
                .Build();
            response = client.GetClient()
                .Execute(request);
        }

        [Then(@"The booking should be deleted")]
        public void ThenTheBookingShouldBeDeleted()
        {
            Assert.AreEqual(201, (int)response.StatusCode);
        }
    }
}