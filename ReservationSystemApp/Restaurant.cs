using System;
using System.Collections.Generic;

namespace ReservationSystemApp
{
    public class Restaurant
    {
        List<Customer> Customers;
        List<Table> Tables;


        public Restaurant(List<Customer> customers, List<Table> tables)
        {
            Customers = customers;
            Tables = tables;
        }


        public List<Reservation> Reserve()
        {
            if(Customers == null || Customers.Count == 0 || Tables == null || Tables.Count == 0)
            {
                return null;
            }

            if(Customers.Count == 1 && Tables.Count == 1)
            {

                if(Tables[0].SeatCount >= Customers[0].NumberOfPeople)
                {
                    return new List<Reservation> { new Reservation { Customer = Customers[0], Table = Tables[0] } };
                }

                return null;

            }


            if(Tables.Count == 1)
            {
                var customerWithMaxNoOfPeople = GetCustomerWithLargestGroup();

                if(Tables[0].SeatCount >= customerWithMaxNoOfPeople.NumberOfPeople)
                {
                    return new List<Reservation> { new Reservation { Customer = customerWithMaxNoOfPeople, Table = Tables[0] } };
                }

            }
            else if (Customers.Count == 1)
            {
                return new List<Reservation> { new Reservation { Customer = Customers[0], Table = GetSmallestTableForCustomer(Customers[0]) } };
            }

            var reservations = new List<Reservation>();

            Customers.Sort(delegate (Customer x, Customer y)
            {
                return -1 * x.NumberOfPeople.CompareTo(y.NumberOfPeople);
            });

            foreach (var customer in Customers)
            {
                if(reservations.Count == Tables.Count)
                {
                    break;
                }

                var bestTableForCustomer = GetSmallestTableForCustomer(customer);
                if(bestTableForCustomer == null)
                {
                    continue;
                }

                reservations.Add(new Reservation { Customer = customer, Table = bestTableForCustomer });
            }

            return reservations;

        }

        Table GetSmallestTableForCustomer(Customer customer)
        {
            Tables.Sort(delegate (Table x, Table y)
            {
                return x.SeatCount.CompareTo(y.SeatCount);
            });

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
                if (customer.NumberOfPeople > maxNoOfPeople)
                {
                    maxNoOfPeople = customer.NumberOfPeople;
                    customerWithLargestGroup = customer;
                }
            }
            return customerWithLargestGroup;
        }
    }
}
