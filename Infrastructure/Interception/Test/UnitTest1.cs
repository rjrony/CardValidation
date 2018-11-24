// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception.Test
{
    using Infrastructure.Interception.Contract;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The unit test 1.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        ///     The test method 1.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var container = new UnityContainer();
            IDependencyResolver DependencyResolver = new DependencyResolver(container);

            var objectDependentOnFirstObject = DependencyResolver.Resolve<ObjectDependentOnFirstObject, FirstObject>(new FirstObject(5));
            Assert.IsTrue(objectDependentOnFirstObject.TestObject.X == 5);
        }

        /// <summary>
        ///     The test resolve with object params.
        /// </summary>
        [TestMethod]
        public void TestResolveWithObjectParams()
        {
            var container = new UnityContainer();
            IDependencyResolver DependencyResolver = new DependencyResolver(container);

            //        var customer = DependencyResolver
            //            .Resolve<Customer>(new Order { Id = 5 }, new Address());

            //        var customer2 = DependencyResolver
            //.Resolve<Customer>(new Order { Id = 5 });

            //Assert.IsTrue(customer.order.Id == 5);

            //var x = container.Resolve<Customer>(new DependencyOverride<Order>
            //       (new Order { Id = 5 }), new DependencyOverride<Address>(new Address()));

            //var y = container.Resolve<Customer>(new DependencyOverride<Order>
            //        (new Order { Id = 5 }));

            //      var cus = UnityContainerExtensions.Resolve(container, typeof(Customer),
            //          new DependencyOverride<Order>(new Order { Id = 5 }),
            //          new DependencyOverride<Address>(new Address {Order = new Order()})
            //          );

            //      var cus2 = UnityContainerExtensions.Resolve(container, typeof(Customer),
            //new DependencyOverride<Order>(new Order { Id = 5 }));
        }
    }

    /// <summary>
    ///     The first object.
    /// </summary>
    public class FirstObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FirstObject" /> class.
        /// </summary>
        public FirstObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstObject"/> class.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        public FirstObject(int x)
        {
            this.X = x;
        }

        /// <summary>
        ///     Gets the x.
        /// </summary>
        public int X { get; private set; }
    }

    /// <summary>
    ///     The object dependent on first object.
    /// </summary>
    public class ObjectDependentOnFirstObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDependentOnFirstObject"/> class.
        /// </summary>
        /// <param name="testObject">
        /// The test object.
        /// </param>
        public ObjectDependentOnFirstObject(FirstObject testObject)
        {
            this.TestObject = testObject;
        }

        /// <summary>
        ///     Gets or sets the other object.
        /// </summary>
        public FirstObject OtherObject { get; set; }

        /// <summary>
        ///     Gets or sets the test object.
        /// </summary>
        public FirstObject TestObject { get; set; }
    }

    /// <summary>
    ///     The customer.
    /// </summary>
    public class Customer
    {
        public Address address;

        public Order order;

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        public Customer(Order order)
        {
            this.order = order;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        public Customer(Order order, Address address)
        {
            this.order = order;
            this.address = address;
            var test = address.Order.Id;
        }
    }

    /// <summary>
    ///     The order.
    /// </summary>
    public class Order
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    ///     The address.
    /// </summary>
    public class Address
    {
        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        public Order Order { get; set; }
    }
}