// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicQueryRequest.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Infrastructure.Localization.Contracts;

    /// <summary>
    ///     Grid Data Source Request
    /// </summary>
    public abstract class DynamicQueryRequest
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets Filter info
        /// </summary>
        /// <summary>
        ///     Gets or sets Page info
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///     Gets or sets PageSize info
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets the sort.
        /// </summary>
        public IList<SortDescriptor> Sort { get; set; }

        /// <summary>
        ///     Gets or sets Skip info
        /// </summary>
        private int? Skip { get; set; }

        /// <summary>
        ///     Gets or sets Take info
        /// </summary>
        private int Take { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The prepare filter.
        /// </summary>
        /// <returns>
        ///     The <see cref="LambdaExpression" />.
        /// </returns>
        protected virtual LambdaExpression PrepareFilter()
        {
            return null;
        }

        private IQueryable SetFilter<T>(IQueryable queryable) where T : class
        {
            var filter = this.PrepareFilter();
            return filter != null ? queryable.Where<T>(filter) : queryable;
        }

        /// <summary>
        ///     The prepare sort.
        /// </summary>
        /// <returns>
        ///     The <see cref="LambdaExpression" />.
        /// </returns>
        protected virtual LambdaExpression PrepareSort()
        {
            return null;
        }

        private IQueryable SetSorting<T>(IQueryable queryable) where T : class
        {
            var sort = this.PrepareSort();
            return sort != null ? queryable.Where<T>(sort) : queryable;
        }

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="queryable">
        /// The queryable.
        /// </param>
        /// <param name="languageSupport">
        /// The language support.
        /// </param>
        /// <typeparam name="T">
        /// entity type
        /// </typeparam>
        /// <returns>
        /// The <see cref="DynamicQueryData"/>.
        /// </returns>
        public DynamicQueryData<T> GetData<T>(IQueryable<T> queryable, ILanguageSupport languageSupport) where T : class
        {
            var total = 0;
            /*
            if (this.Sort == null)
            {
                this.Sort = new List<SortDescriptor> { new SortDescriptor { Dir = "asc", Field = defaultSortField } };
            }
            var test=queryable.Skip((1) * 2).Take(2);
           
            */
            queryable = (IQueryable<T>)this.SetFilter<T>(queryable);
            total = Queryable.Count(queryable);
            if (this.Skip == null)
            {
                this.Skip = (this.Page - 1) * this.PageSize;
            }

            queryable = this.SortData(queryable, this.Sort, languageSupport);
            queryable = this.PageData(queryable, this.PageSize, this.Skip);

            return new DynamicQueryData<T> { Data = queryable, Total = total };
        }

        /// <summary>
        /// Method to get sort
        /// </summary>
        /// <typeparam name="T">
        /// set entity
        /// </typeparam>
        /// <param name="queryable">
        /// set parameter
        /// </param>
        /// <param name="sort">
        /// set sort
        /// </param>
        /// <param name="languageSupport">
        /// The current culture provider.
        /// </param>
        /// <returns>
        /// returns sort
        /// </returns>
        private IQueryable<T> SortData<T>(IQueryable<T> queryable, IEnumerable<SortDescriptor> sort, ILanguageSupport languageSupport)
        {
            if (sort != null)
            {
                var sortDescriptors = sort as SortDescriptor[] ?? sort.ToArray();
                if (sort != null && sortDescriptors.Any())
                {
                    var ordering = string.Join(",", sortDescriptors.Select(s => s.ToExpression()));
                    return queryable.OrderBy(ordering, languageSupport);
                }
            }

            return queryable;
        }

        /// <summary>
        /// Method get Page info
        /// </summary>
        /// <typeparam name="T">
        /// set entity
        /// </typeparam>
        /// <param name="queryable">
        /// set parameter
        /// </param>
        /// <param name="take">
        /// set take
        /// </param>
        /// <param name="skip">
        /// set skip
        /// </param>
        /// <returns>
        /// returns page
        /// </returns>
        private IQueryable<T> PageData<T>(IQueryable<T> queryable, int take, int? skip)
        {
            //return queryable.Skip<T>((int)skip).Take<T>(take);
            return Queryable.Take(Queryable.Skip(queryable, (int)skip), take);
        }

        #endregion
    }
}