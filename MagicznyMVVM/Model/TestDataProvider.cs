using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicznyMVVM.Model
{
    public static class TestDataProvider
    {
        public static IList<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer{
                    Firma = "DailyCodeSolutions",
                    Name = "Grzegorz Oronowicz",
                    Number = 1
                }
            };
        }
    }
}
