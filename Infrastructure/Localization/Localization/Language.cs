// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Language.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Serialization;

    using Infrastructure.Localization.Contracts;

    /// <summary>
    ///     The language.
    /// </summary>
    public class Language : Enumeration<Guid>, ILanguage
    {
        [NonSerialized]
        private CultureInfo cultureInfo;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Language" /> class.
        /// </summary>
        public Language()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        public Language(Guid guid)
            : base(guid)
        {
        }

        /// <summary>
        ///     Gets or sets the culture info.
        /// </summary>
        [XmlIgnore]
        public CultureInfo CultureInfo
        {
            get
            {
                //when deserialized to have a culture
                if (this.cultureInfo == null)
                {
                    var foundLanguage = GetAll<Language, LanguageCodes>().FirstOrDefault(l => l.Value == this.Value);
                    if (foundLanguage != null)
                    {
                        this.cultureInfo = foundLanguage.cultureInfo;
                    }
                }

                return this.cultureInfo;
            }

            set
            {
                this.cultureInfo = value;
            }
        }
    }
}