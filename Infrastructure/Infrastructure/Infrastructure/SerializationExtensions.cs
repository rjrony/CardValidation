// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationExtensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization.Formatters;

    using Newtonsoft.Json;

    /// <summary>
    ///     The serialization extensions.
    /// </summary>
    public static class SerializationExtensions
    {
        #region Static Fields

        private static readonly JsonSerializerSettings JsonSerializationSettings = CreateJsonSerializationSettings();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The deep clone using reference handling.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// The type to be cloned.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeepCloneUsingReferenceHandling<T>(this T obj)
        {
            if (object.Equals(obj, null))
            {
                return default(T);
            }

            var settings = CreateJsonSerializationSettings();
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            return obj.DeepClone(settings);
        }

        /// <summary>
        /// The deep clone.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// The type to be cloned
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeepClone<T>(this T obj)
        {
            var settings = CreateJsonSerializationSettings();
            return obj.DeepClone(settings);
        }

        /// <summary>
        /// The deep clone.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <typeparam name="T">
        /// The type to be cloned
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeepClone<T>(this T obj, JsonSerializerSettings settings)
        {
            if (object.Equals(obj, null))
            {
                return default(T);
            }

            T deserialized;

            var serializationSettings = settings ?? JsonSerializationSettings;

            var json = JsonConvert.SerializeObject(obj, serializationSettings);

            if (typeof(T).IsAbstract && !(obj is IEnumerable))
            {
                deserialized = (T)JsonConvert.DeserializeObject(json, obj.GetType());
            }
            else
            {
                deserialized = JsonConvert.DeserializeObject<T>(json, JsonSerializationSettings);
            }

            if (object.Equals(deserialized, null))
            {
                throw new Exception(string.Format("DeepClone failed for the type {0}", obj.GetType()));
            }

            return deserialized;
        }

        /// <summary>
        /// The deserialize from json.
        /// </summary>
        /// <param name="jsonString">
        /// The json string.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object DeserializeFromJson(this string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString, JsonSerializationSettings);
        }

        /// <summary>
        /// The deserialize from json.
        /// </summary>
        /// <param name="jsonString">
        /// The json string.
        /// </param>
        /// <param name="serializationSettings">
        /// The serialization Settings.
        /// </param>
        /// <typeparam name="T">
        /// Type you want to deserialize to
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeserializeFromJson<T>(this string jsonString, SerializationSettings serializationSettings = null)
        {
            return string.IsNullOrEmpty(jsonString)
                       ? default(T)
                       : JsonConvert.DeserializeObject<T>(jsonString, ChangeDefaultSettings(serializationSettings));
        }

        /// <summary>
        /// The deserialize from json.
        /// </summary>
        /// <param name="jsonString">
        /// The json string.
        /// </param>
        /// <typeparam name="T">
        /// Type of the object to deserialize
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeserializeFromJson<T>(this string jsonString)
        {
            return jsonString.DeserializeFromJson<T>(null);
        }

        /// <summary>
        /// The deserialize from json.
        /// </summary>
        /// <param name="jsonString">
        /// The json string.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object DeserializeFromJson(this string jsonString, Type targetType)
        {
            return string.IsNullOrEmpty(jsonString)
                       ? null
                       : JsonConvert.DeserializeObject(jsonString, targetType, JsonSerializationSettings);
        }

        /// <summary>
        /// The deserialize json using reference handling.
        /// </summary>
        /// <param name="jsonString">
        /// The json string.
        /// </param>
        /// <typeparam name="T">
        /// Type of the object to deserialize
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeserializeJsonUsingReferenceHandling<T>(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return default(T);
            }

            var settings = CreateJsonSerializationSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            return JsonConvert.DeserializeObject<T>(jsonString, settings);
        }

        /// <summary>
        /// The serialize to json.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <param name="serializationSettings">
        /// The serialization Settings.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SerializeToJson(this object instance, SerializationSettings serializationSettings)
        {
            if (instance == null)
            {
                return string.Empty;
            }

            JsonSerializerSettings settings = ChangeDefaultSettings(serializationSettings);
            return JsonConvert.SerializeObject(instance, settings);
        }

        /// <summary>
        /// The serialize to json.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SerializeToJson(this object instance)
        {
            return instance.SerializeToJson(null);
        }

        /// <summary>
        /// The serialize to json including object names.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SerializeToJsonIncludingObjectNames(this object instance)
        {
            if (instance == null)
            {
                return string.Empty;
            }

            var settings = CreateJsonSerializationSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;

            return JsonConvert.SerializeObject(instance, settings);
        }

        /// <summary>
        /// The serialize to json using reference handling.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SerializeToJsonUsingReferenceHandling(this object instance)
        {
            if (instance == null)
            {
                return string.Empty;
            }

            var settings = CreateJsonSerializationSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            return JsonConvert.SerializeObject(instance, settings);
        }

        #endregion

        #region Methods

        private static JsonSerializerSettings ChangeDefaultSettings(SerializationSettings serializationSettings)
        {
            if (serializationSettings == null)
            {
                return JsonSerializationSettings;
            }

            var defaultSettings = CreateJsonSerializationSettings();
            defaultSettings.TypeNameHandling = serializationSettings.TypeNameHandling;
            defaultSettings.TypeNameAssemblyFormat = serializationSettings.FormatterAssemblyStyle;
            defaultSettings.PreserveReferencesHandling = serializationSettings.PreserveReferencesHandling;
            defaultSettings.ReferenceLoopHandling = serializationSettings.ReferenceLoopHandling;
            return defaultSettings;
        }

        private static JsonSerializerSettings CreateJsonSerializationSettings()
        {
            var jsonSerializationSettings = new JsonSerializerSettings();
            jsonSerializationSettings.TypeNameHandling = TypeNameHandling.Auto;
            jsonSerializationSettings.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;
            var contractResolver = new PrivateSetterContractResolver();
            jsonSerializationSettings.ContractResolver = contractResolver;
            return jsonSerializationSettings;
        }

        #endregion
    }
}