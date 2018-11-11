using System;
using System.Collections.Generic;

namespace ReservationSystemApp
{
    public class Restaurant
    {
        readonly List<Customer> Customers;
        readonly List<Table> Tables;


        public Restaurant(List<Customer> customers, List<Table> tables)
        {
            Customers = customers;
            Tables = tables;
        }

        bool UnableToMakeReservations()
        {
            return Customers == null || Customers.Count == 0 || Tables == null || Tables.Count == 0;
        }

        void SortCustomersByNoOfPeopleDesceding()
        {
            Customers.Sort(delegate (Customer x, Customer y)
            {
                return -1 * x.NumberOfPeople.CompareTo(y.NumberOfPeople);
            });
        }

        void SortTablesBySeatCount()
        {
            Tables.Sort(delegate (Table x, Table y)
            {
                return x.SeatCount.CompareTo(y.SeatCount);
            });
        }

        Reservation MakeReservation(Customer customer, Table table)
        {
            return new Reservation { Customer = customer, Table = table };
        }

        public List<Reservation> MakeReservations()
        {
            if (UnableToMakeReservations()) return null;

            SortCustomersByNoOfPeopleDesceding();
            SortTablesBySeatCount();

            var reservations = new List<Reservation>();

            foreach (var customer in Customers)
            {
                if(reservations.Count == Tables.Count)
                {
                    break;
                }

                var optimalTableForCustomer = GetOptimalTableForCustomer(customer);

                if(optimalTableForCustomer == null)
                {
                    continue;
                }

                reservations.Add(MakeReservation(customer, optimalTableForCustomer));
            }

            return reservations;

        }

        Table GetOptimalTableForCustomer(Customer customer)
        {
            foreach (var table in Tables)
            {
                if (table.SeatCount >= customer.NumberOfPeople)
                {
                    return table;
                }
            }
            return null;
        }

        Customer GetCustomerWithLargestGroup()
        {
            Customer customerWithLargestGroup = null;
            var maxNoOfPeople = Int32.MinValue;

            foreach (var customer in Customers)
            {
                if(customer.NumberOfPeople <= maxNoOfPeople)
                {
                    continue;
                }

                maxNoOfPeople = customer.NumberOfPeople;
                customerWithLargestGroup = customer;
            }
            return customerWithLargestGroup;
        }
    }
}
