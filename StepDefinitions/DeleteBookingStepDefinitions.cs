using Newtonsoft.Json;
using SpecFlowRestSharp.APIRequests;
using SpecFlowRestSharp.Hooks;
using NUnit.Framework;
using SpecFlowRestSharp.Utility;

namespace SpecFlowRestSharp.StepDefinitions
{
    [Binding]
    public class DeleteBookingStepDefinitions:DefaultHooks
    {
        private string _token;
        private string _bookingId;

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

            Execute(request);

            ConvertToJObject();

            _token = JObj.token;
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

            Execute(request);

            ConvertToJObject();

            _bookingId = JObj.bookingid;
        }

        [When(@"The user performs a delete booking request")]
        public void WhenTheUserPerformsADeleteBookingRequest()
        {
            string url = "https://restful-booker.herokuapp.com/booking/{id}";
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Cookie", "token="+_token }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", _bookingId}
            };

            var request = new DeleteRequestBuilder()
                .WithUrl(url)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();

            Execute(request);
        }

        [Then(@"The booking should be deleted")]
        public void ThenTheBookingShouldBeDeleted()
        {
            //In case I want to assert something from the dynamic object, I need to access it via JObj or JArr depending on the object type.
            Assert.Multiple(() =>
            {
                Assert.AreEqual(201, (int)_response.StatusCode);
                //Assert.AreEqual(202, (int)_response.StatusCode);
                //Assert.AreEqual(400, (int)_response.StatusCode);
                //Assert.AreEqual(500, (int)_response.StatusCode);
            });
        }
    }
}