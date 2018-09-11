using System;
using System.ComponentModel;
using System.Globalization;

namespace Test.Web.Model.Converters
{
    public class GeoPointConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                GeoPoint point;

                if (GeoPoint.TryParse((string)value, out point))
                {
                    return point;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}