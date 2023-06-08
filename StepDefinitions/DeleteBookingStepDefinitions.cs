using SpecFlowRestSharp.APIRequests;
using SpecFlowRestSharp.Hooks;
using NUnit.Framework;
using SpecFlowRestSharp.Utility;
using SpecFlowRestSharp.Models;

namespace SpecFlowRestSharp.StepDefinitions
{
    [Binding]
    public class DeleteBookingStepDefinitions
    {
        private string token;
        private string bookingId;
        private dynamic json;
        DefaultHooks hook;

        public DeleteBookingStepDefinitions(DefaultHooks hook)
        {
            this.hook = hook;
            //Configuration is stored inside of the json dynamic object
            json = JsonProcessor.ReadJson("../../../StepDefinitions/DeleteBooking.json");
        }

        [Given(@"The user is authenticated")]
        public void GivenTheUserIsAuthenticated()
        {
            //string url = "http://restapi.adequateshop.com/api/Traveler";

            //var request = new GetRequestBuilder()
            //    .WithUrl(url)
            //    .Build();

            //hook.Execute(request);

            //var obj = hook.ConvertToObject<TravelerinformationResponse>();

            //var a = obj.TotalPages;

            string url = json.url;
            string endpoint = json.authorization;
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var body = new
            {
                username = json.username,
                password = json.password
            };

            //Create a request using the request builder method
            var request = new PostRequestBuilder()
                .WithUrl(url)
                .WithEndpoint(endpoint)
                .WithHeaders(headers)
                .WithBody(body)
                .Build();

            hook.Execute(request);

            var obj = hook.ConvertToObject<TokenResponse>();

            token = obj.token;
        }

        [When(@"The user creates a booking")]
        public void WhenTheUserCreatesABooking()
        {
            string url = json.url;
            string endpoint = json.createBooking;
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" }
            };

            var body = @"{""firstname"":""MadeUp"",""lastname"":""AlsoMadeUp"",""totalprice"":1508,""depositpaid"":true,""bookingdates"":{""checkin"":""2023-05-25"",""checkout"":""2023-05-25""},""additionalneeds"":""Dorucak""}";

            var request = new PostRequestBuilder()
                .WithUrl(url)
                .WithEndpoint(endpoint)
                .WithHeaders(headers)
                .WithBody(body)
                .Build();

            hook.Execute(request);

            var jObj = hook.ConvertToDynamicObject();

            bookingId = jObj.bookingid;
        }

        [When(@"The user performs a delete booking request")]
        public void WhenTheUserPerformsADeleteBookingRequest()
        {
            string url = json.url;
            string endpoint = json.deleteBooking;
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
                .WithEndpoint(endpoint)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();

            hook.Execute(request);
        }

        [Then(@"The booking should be deleted")]
        public void ThenTheBookingShouldBeDeleted()
        {
            //In case I want to assert something from the dynamic object, I need to access it via JObj or JArr depending on the object type.
            Assert.Multiple(() =>
            {
                //Yes, I know it this assertion does not make sense for a delete request, but, since the API is "bugged", I need to set it to 201 so I can check out the results when the test passes.
                Assert.AreEqual(201, (int)hook._response.StatusCode);
            });
        }











        //Ignore this, I used it for testing parallel exeucution of tests.

        [Then(@"The booking should be deleted2")]
        public void ThenTheBookingShouldBeDeleted2()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(201, (int)hook._response.StatusCode);
            });
        }

        [Then(@"The booking should be deleted3")]
        public void ThenTheBookingShouldBeDeleted3()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(201, (int)hook._response.StatusCode);
            });
        }

        [Then(@"The booking should be deleted4")]
        public void ThenTheBookingShouldBeDeleted4()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(201, (int)hook._response.StatusCode);
            });
        }

        [Then(@"The booking should be deleted5")]
        public void ThenTheBookingShouldBeDeleted5()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(201, (int)hook._response.StatusCode);
            });
        }

        [Then(@"The booking should be deleted6")]
        public void ThenTheBookingShouldBeDeleted6()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(201, (int)hook._response.StatusCode);
            });
        }
    }
}