namespace CardValidation.Api.Dtos.Validation
{
    public static class ConstantValues
    {
        /// <summary>
        ///     The business name regular expression.
        /// </summary>
        public const string UrlRegularExpression = "^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$";
        public const string ImageUrlRegularExpression = "(http(s?):)([/|.|\\w|\\s|-])*\\.(?:jpg|gif|png)";
        /// <summary>
        ///     The Guid Regular Expresssion
        /// </summary>
        public const string GuidRegularExpresssion = @"\b[A-F0-9a-f]{8}(?:-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}\b";
    }
}
