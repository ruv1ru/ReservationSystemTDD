using System;
using Xunit;
using ReservationSystemApp;
using System.Collections.Generic;

namespace ReservationSystemTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetReservationForOne_ShouldReturnSingleReservation()
        {
            var customers = new List<Customer> { new Customer { Name = "Jack", NumberOfPeople = 1 } };

            var tables = new List<Table> { new Table { Number = 1, SeatCount = 1 } };

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Single(reservations);

            Assert.Collection(reservations, reservation => Assert.Equal("Jack", reservation.Customer.Name));
            Assert.Collection(reservations, reservation => Assert.Equal(1, reservation.Customer.NumberOfPeople));
            Assert.Collection(reservations, reservation => Assert.Equal(1, reservation.Table.SeatCount));


        }

        [Fact]
        public void GetNullReservationForZeroCustomers_ShouldReturnNoReservations()
        {
            var customers = new List<Customer>();

            var tables = new List<Table> { new Table { Number = 1, SeatCount = 1 } };

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Null(reservations);



        }

        [Fact]
        public void GetNullReservationForZeroTables_ShouldReturnNoReservations()
        {

            var customers = new List<Customer> { new Customer { Name = "Jack", NumberOfPeople = 1 } };

            var tables = new List<Table>();

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Null(reservations);



        }




        [Fact]
        public void GetNullReservationForNullCustomers_ShouldReturnNoReservations()
        {
            List<Customer> customers = null;

            var tables = new List<Table> { new Table { Number = 1, SeatCount = 1 } };

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Null(reservations);



        }

        [Fact]
        public void GetNullReservationForNullTables_ShouldReturnNoReservations()
        {

            var customers = new List<Customer> { new Customer { Name = "Jack", NumberOfPeople = 1 } };

            List<Table> tables = null;

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Null(reservations);



        }

        [Fact]
        public void GetReservationForMultipleCustomers_ShouldReturnReservationsForLargestGroup()
        {

            var customers = new List<Customer> 
            {
                new Customer { Name = "Jack", NumberOfPeople = 1 },
                new Customer { Name = "Jane", NumberOfPeople = 2 }
            };

            var tables = new List<Table> { new Table { Number = 1, SeatCount = 2 } };

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Collection(reservations, reservation => Assert.Equal("Jane", reservation.Customer.Name));
            Assert.Collection(reservations, reservation => Assert.Equal(2, reservation.Customer.NumberOfPeople));


        }

        [Fact]
        public void GetReservationForMultipleTables_ShouldReturnReservationsWithOptimalSeatCount()
        {

            var customers = new List<Customer>
            {
                new Customer { Name = "Jack", NumberOfPeople = 1 }
            };

            var tables = new List<Table>
            {
                new Table { Number = 1, SeatCount = 2 },
                new Table { Number = 2, SeatCount = 1 }
            };

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Equal(1, reservations[0].Table.SeatCount);

        }

        [Fact]
        public void GetReservationForMultipleCustomers_ShouldReturnReservationsForLargestGroups()
        {

            var customers = new List<Customer>
            {
                new Customer { Name = "Jack", NumberOfPeople = 2 },
                new Customer { Name = "Jane", NumberOfPeople = 4 },
                new Customer { Name = "Tina", NumberOfPeople = 3 },

            };

            var tables = new List<Table>
            {
                new Table { Number = 1, SeatCount = 3 },
                new Table { Number = 2, SeatCount = 4 }
            };

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Collection(reservations, reservation => Assert.Equal(4, reservation.Customer.NumberOfPeople),
                              reservation => Assert.Equal(3, reservation.Customer.NumberOfPeople));



        }

        [Fact]
        public void GetReservationForCustomersWithAboveLimitGroup_ShouldNotReturnReservationsForAboveLimitGroup()
        {

            var customers = new List<Customer>
            {
                new Customer { Name = "Jack", NumberOfPeople = 7 },
                new Customer { Name = "Jane", NumberOfPeople = 2 },
                new Customer { Name = "Jina", NumberOfPeople = 1 }

            };

            var tables = new List<Table>
            {
                new Table { Number = 1, SeatCount = 6 }
            };

            var restaurant = new Restaurant(customers, tables);

            var reservations = restaurant.MakeReservations();

            Assert.Collection(reservations, reservation => Assert.NotEqual(7, reservation.Customer.NumberOfPeople));

        }


    }
}
