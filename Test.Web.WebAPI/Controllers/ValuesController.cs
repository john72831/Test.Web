using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Tracing;
using System.Web.Http.ValueProviders;
using Test.Web.Model;
using Test.Web.Model.Attributes;
using Test.Web.Model.ModelBinder;
using Test.Web.Model.ValueProviders;

namespace Test.Web.WebAPI.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            //Configuration.Services.GetTraceWriter().Info(Request, "ProductsController", "Get the list of products.");

            return new string[] { "value1", "value2" };
        }

        [Route("{id}", Name = "Get", Order = 1)]
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [Route("{id:nonzero}", Name = "GetWithNonZeroParameter", Order = 1)]
        [HttpGet]
        public string GetWithNonZeroParameter(int id)
        {
            return "value";
        }

        [Route("GetProduct/{id:int:nonzero}")]
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            return Ok(new Product() { Id = 1, Category = "Category1", Name = "TestName" });
        }

        [Route("GetGeoPoint")]
        [HttpGet]
        public HttpResponseMessage GetGeoPoint(GeoPoint location)
        {
            return Request.CreateResponse(HttpStatusCode.OK, location);
        }

        [Route("GetGeoPointWithModelBinder")]
        [HttpGet]
        public HttpResponseMessage GetGeoPointWithModelBinder([ModelBinder(typeof(GeoPointModelBinder))] GeoPointWithModelBinderAttribute location)
        {
            return Request.CreateResponse(HttpStatusCode.OK, location);
        }

        [Route("GetGeoPointWithModelBinderAndProvider")]
        [HttpGet]
        public HttpResponseMessage GetGeoPointWithModelBinderAndProvider([ModelBinder] GeoPointWithModelBinderAttribute location)
        {
            return Request.CreateResponse(HttpStatusCode.OK, location);
        }

        [Route("GetGeoPointWithValueProvider")]
        [HttpGet]
        public HttpResponseMessage GetGeoPointWithValueProvider([ValueProvider(typeof(CookieValueProviderFactory))] GeoPointWithModelBinderAttribute location)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, location);

            CookieHeaderValue cookie = new CookieHeaderValue("location", 47.678558 + "," + -122.130989);
            cookie.Expires = DateTimeOffset.Now.AddDays(2);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            return response;
        }

        [Route("GetGeoPointWithParameterBindingAttribute")]
        [HttpGet]
        public HttpResponseMessage GetGeoPointWithParameterBindingAttribute([IfNoneMatch] ETag etag)
        {
            return Request.CreateResponse(HttpStatusCode.OK, etag);
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]string value)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created);

            // Generate a link to the new book and set the Location header in the response.
            string uri = Url.Link("GetWithNonZeroParameter", new { id = 1 });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
