using System.Web.Http.ModelBinding;
using Test.Web.Model.ModelBinder;

namespace Test.Web.Model
{
    [ModelBinder(typeof(GeoPointModelBinder))]
    public class GeoPointWithModelBinderAttribute
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static bool TryParse(string s, out GeoPointWithModelBinderAttribute result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }

            double latitude, longitude;
            if (double.TryParse(parts[0], out latitude) &&
                double.TryParse(parts[1], out longitude))
            {
                result = new GeoPointWithModelBinderAttribute() { Longitude = longitude, Latitude = latitude };
                return true;
            }
            return false;
        }
    }
}
