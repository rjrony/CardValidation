using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Security.AntiXss;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Host.HtmlEncoder
{
    public class AntiXssContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = base.CreateProperties(type, memberSerialization);
            foreach (var prop in props.Where(p => p.PropertyType == typeof(string)))
            {
                PropertyInfo pi = type.GetProperty(prop.UnderlyingName);
                if (pi != null)
                {
                    prop.ValueProvider = new AntiXssEncodingValueProvider(pi);
                }
            }
            return props;
        }

        /// <summary>
        /// The Value provider for AntiXSS Encoding
        /// </summary>
        protected class AntiXssEncodingValueProvider : IValueProvider
        {
            private readonly PropertyInfo _targetProperty;

            public AntiXssEncodingValueProvider(PropertyInfo targetProperty)
            {
                this._targetProperty = targetProperty;
            }

            // SetValue gets called by Json.Net during deserialization.
            // The value parameter has the original value read from the JSON;
            // target is the object on which to set the value.
            public void SetValue(object target, object value)
            {
                string valueString = (string)value;
                var encodedValue = AntiXssEncoder.HtmlEncode(valueString, true);
                _targetProperty.SetValue(target, encodedValue);
            }

            // GetValue is called by Json.Net during serialization.
            // The target parameter has the object from which to read the string;
            // the return value is the string that gets written to the JSON
            public object GetValue(object target)
            {
                string value = (string)_targetProperty.GetValue(target);
                return AntiXssEncoder.HtmlEncode(value, true);
            }
        }
    }
}
