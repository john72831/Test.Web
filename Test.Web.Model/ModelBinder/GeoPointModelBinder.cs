using System;
using System.Collections.Concurrent;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace Test.Web.Model.ModelBinder
{
    public class GeoPointModelBinder : IModelBinder
    {
        // List of known locations.
        private static ConcurrentDictionary<string, GeoPointWithModelBinderAttribute> _locations
            = new ConcurrentDictionary<string, GeoPointWithModelBinderAttribute>(StringComparer.OrdinalIgnoreCase);

        static GeoPointModelBinder()
        {
            _locations["redmond"] = new GeoPointWithModelBinderAttribute() { Latitude = 47.67856, Longitude = -122.131 };
            _locations["paris"] = new GeoPointWithModelBinderAttribute() { Latitude = 48.856930, Longitude = 2.3412 };
            _locations["tokyo"] = new GeoPointWithModelBinderAttribute() { Latitude = 35.683208, Longitude = 139.80894 };
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(GeoPointWithModelBinderAttribute))
            {
                return false;
            }

            ValueProviderResult val = bindingContext.ValueProvider.GetValue(
                bindingContext.ModelName);
            if (val == null)
            {
                return false;
            }

            string key = val.RawValue as string;
            if (key == null)
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.ModelName, "Wrong value type");
                return false;
            }

            GeoPointWithModelBinderAttribute result;
            if (_locations.TryGetValue(key, out result) || GeoPointWithModelBinderAttribute.TryParse(key, out result))
            {
                bindingContext.Model = result;
                return true;
            }

            bindingContext.ModelState.AddModelError(
                bindingContext.ModelName, "Cannot convert value to GeoPoint");
            return false;
        }
    }
}
