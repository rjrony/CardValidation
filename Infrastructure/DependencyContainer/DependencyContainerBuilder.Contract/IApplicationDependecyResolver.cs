using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyContainerBuilder.Contract
{
    using Infrastructure.Interception.Contract;

    /// <summary>
    /// The ApplicationDependecyResolver interface.
    /// </summary>
    public interface IApplicationDependecyResolver : IDependencyResolver
    {
        /// <summary>
        /// The begin scope.
        /// </summary>
        /// <returns>
        /// The <see cref="ScopedApplicationDependecyResolver"/>.
        /// </returns>
        IScopedApplicationDependecyResolver BeginScope();
    }
}
