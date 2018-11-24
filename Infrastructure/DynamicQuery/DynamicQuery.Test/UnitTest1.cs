// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The test dynamic query request.
    /// </summary>
    public class TestDynamicQueryRequest : DynamicQueryRequest
    {
        /// <summary>
        ///     Gets or sets the request status.
        /// </summary>
        public FilterEnumeration<short> RequestStatus { get; set; }

        /// <summary>
        ///     Gets or sets the request status id.
        /// </summary>
        public short? RequestStatusId { get; set; }

        /// <summary>
        ///     The prepare filter.
        /// </summary>
        /// <returns>
        ///     The <see cref="LambdaExpression" />.
        /// </returns>
        protected override LambdaExpression PrepareFilter()
        {
            Expression<Func<MyClass, bool>> expression = x => x.Id == this.RequestStatusId;
            return expression;
        }

        /// <summary>
        ///     The prepare sort.
        /// </summary>
        /// <returns>
        ///     The <see cref="LambdaExpression" />.
        /// </returns>
        protected override LambdaExpression PrepareSort()
        {
            var sortList = new List<DynamicOrdering>();

            Expression<Func<MyClass, bool>> expression = x => x.Id == this.RequestStatusId;
            var dynamicOrdering = new DynamicOrdering() { Ascending = true, Selector = expression };

            sortList.Add(dynamicOrdering);

            return expression;
        }
    }

    /// <summary>
    ///     The unit test 1.
    /// </summary>
    [Ignore]
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        ///     The test method 1.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            TestDynamicQueryRequest dynamicQueryRequest = new TestDynamicQueryRequest();
            dynamicQueryRequest.Page = 1;
            dynamicQueryRequest.PageSize = 3;
            dynamicQueryRequest.RequestStatusId = 1;
            dynamicQueryRequest.RequestStatus = new FilterEnumeration<short>();
            dynamicQueryRequest.RequestStatus.Value = 1;
            dynamicQueryRequest.RequestStatus.Operator = (QueryOperatorCodes)1;

            List<MyClass> myClasses = new List<MyClass>
                                          {
                                              new MyClass { Name = "Abir", Id = 1, Amount = 1.2 },
                                              new MyClass { Name = "Zebra", Id = 2, Amount = .5 },
                                              new MyClass { Name = "Karim", Id = 2, Amount = 5.5 }
                                          };

            var queryable = myClasses.AsQueryable();

            var requestMoneyModel = new MyClass();
            var fieldId = requestMoneyModel.GetPropertyName(x => x.Id);
            dynamicQueryRequest.Sort = new List<SortDescriptor>();
            dynamicQueryRequest.Sort.Add(new SortDescriptor { Field = new MyClass().GetPropertyName(x => x.Id), Dir = "desc" });
            dynamicQueryRequest.Sort.Add(new SortDescriptor { Field = new MyClass().GetPropertyName(x => x.Name), Dir = "desc" });
            var data = dynamicQueryRequest.GetData(queryable, null);
        }
    }

    /// <summary>
    ///     The my class.
    /// </summary>
    public class MyClass
    {
        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}