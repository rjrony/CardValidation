namespace Infrastructure.ApiDocumentation
{
    using System.Collections.Generic;

    using Infrastructure.ApiDocumentation.Contracts;

    /// <summary>
    /// The end point parameters.
    /// </summary>
    public class EndPoint : IEndPointParameters
    {
        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        public string ApiEndPoint { get; set; }

        /// <summary>
        /// Gets or sets the end point parameter maps.
        /// </summary>
        /// <value>
        /// The end point parameter maps.
        /// </value>
        public List<IEndPointParamMap> EndPointParameters { get; set; }
    }
}