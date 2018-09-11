using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;
using System.Web.Http.Tracing;
using System.Web.Http.ValueProviders;
using Test.Web.Model;
using Test.Web.Model.ModelBinder;
using Test.Web.Model.ValueProviders;
using Test.Web.WebAPI.Constraints;
using Test.Web.WebAPI.Formatters;

namespace Test.Web.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(typeof(ITraceWriter), new SimpleTracer());

            // Web API 路由
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("nonzero", typeof(NonZeroConstraint));
            config.MapHttpAttributeRoutes(constraintResolver);

            var bson = new BsonMediaTypeFormatter();
            bson.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.contoso"));
            config.Formatters.Add(bson);
            config.Formatters.Add(new ProductCsvFormatter());

            var provider = new SimpleModelBinderProvider(typeof(GeoPoint), new GeoPointModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
            config.Services.Add(typeof(ValueProviderFactory), new CookieValueProviderFactory());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
