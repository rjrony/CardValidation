using System.Linq;
using System.Reflection;
using Infrastructure.Host.Contracts;
using AntiXssEncoder =System.Web.Security.AntiXss.AntiXssEncoder;

namespace Infrastructure.Host.HtmlEncoder
{
    public static class AntiXssEncoderHelper
    {
        public static void EncodeProperties(object argumentValue)
        {
            var propertiesFlaggedForSanitization = argumentValue.GetType().GetProperties().Where(e =>
                e.PropertyType == typeof(string)
                &&
                e.GetCustomAttribute<ExcludeSanitizationAttribute>() == null).ToList();
            if (propertiesFlaggedForSanitization.Any())
            {
                foreach (var propertyInfo in propertiesFlaggedForSanitization)
                {
                    var raw = (string)propertyInfo.GetValue(argumentValue);
                    if (!string.IsNullOrEmpty(raw))
                    {
                        propertyInfo.SetValue(argumentValue,  AntiXssEncoder.HtmlEncode(raw, true));
                    }
                }
            }
        }
    }
}
