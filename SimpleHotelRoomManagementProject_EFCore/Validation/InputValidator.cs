using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Validation
{
    // Simple console input validator utilities.
    /// Keeps prompting until valid input is provided.
    public static class InputValidator
    {
        // Reads a positive integer from console.
        /// Keeps prompting until the user enters a valid positive integer (> 0)

        public static int GetPositiveInt()
        {
            while (true)
            {
                string? s = Console.ReadLine();
                if (int.TryParse(s, out int val) && val > 0)
                    return val;

                Console.Write("Invalid input. Please enter a positive integer: ");
            }
        }

        /// Reads a positive decimal number (currency, rate) from console.
        /// Keeps prompting until valid.

        public static decimal GetPositiveDecimal()
        {
            while (true)
            {
                string? s = Console.ReadLine();
                if (decimal.TryParse(s, out decimal val) && val > 0)
                    return val;

                Console.Write("Invalid input. Please enter a positive decimal (for example 100.00): ");
            }
        }




    }
}
