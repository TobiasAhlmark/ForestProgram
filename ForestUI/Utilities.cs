using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ForestProgram.UI;
class Utilities
{
    ///---------------------------------------- MIN EGNA
    public static bool IsValidEmail1(string email)
    {
        try
        {
            // Use MailAddress to check basic email structure
            var mailAddress = new MailAddress(email);
        }
        catch (FormatException)
        {
            return false;
        }

        // Apply stricter Regex pattern to further validate
        string emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    public static DateTime GetAdultBirthDate(string prompt)
    {
        DateTime date;
        while (true)
        {
            Console.WriteLine(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out date))
            {
                // Kontrollera om födelsedatumet är i framtiden
                if (date > DateTime.Now)
                {
                    Console.WriteLine("Birth date cannot be in the future.");
                    continue;
                }

                // Beräkna ålder
                var age = DateTime.Now.Year - date.Year;
                // Justera om födelsedagen inte har inträffat än i år
                if (date.Date > DateTime.Now.AddYears(-age))
                    age--;

                // Kontrollera om åldern är mer än 140 år
                if (age > 140)
                {
                    Console.WriteLine("Age cannot be more than 140 years.");
                    continue;
                }

                // Kontrollera om personen är under 18 år
                if (age < 18)
                {
                    Console.WriteLine("You must be at least 18 years old.");
                    continue;
                }

                // Returnera giltigt födelsedatum om alla villkor är uppfyllda
                return date;
            }

            // Felmeddelande vid ogiltigt datumformat
            Console.WriteLine("Invalid date format, please use yyyy-mm-dd format.");
        }
    }

    public static DateTime GetValidBirthDate(string prompt)
    {
        DateTime date;
        while (true)
        {
            Console.WriteLine(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out date))
            {
                // Check if birth date is in the future
                if (date > DateTime.Now)
                {
                    Console.WriteLine("Birth date cannot be in the future.");
                    continue;
                }

                // Calculate age
                var age = DateTime.Now.Year - date.Year;
                // Adjust for birthdays that haven't occurred this year
                if (date.Date > DateTime.Now.AddYears(-age))
                    age--;

                // Check if age is reasonable
                if (age > 140)
                {
                    Console.WriteLine("Age cannot be more than 140 years.");
                    continue;
                }

                return date;
            }
            Console.WriteLine("Invalid date format, please use yyyy-mm-dd format.");
        }
    }
    ///----------------------------------- GUSTAVS
    public static string GetEmail(string prompt = "")
    {
        do
        {
            Console.Write(prompt);
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email) && IsValidEmail(email))
            {
                return email;
            }
            Console.WriteLine("Email is not correct");
        } while (true);
    }

    /// <summary>
    /// Gets telephone number of 10 digits
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="errormsg"></param>
    /// <returns></returns>
    public static string GetPhoneNumber(string prompt = "", string errormsg = "")
    {
        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && input.Length == 10 && input.All(char.IsDigit))
            {
                return input;
            }
            Console.WriteLine(errormsg);
        } while (true);
    }

    /// <summary>
    /// Get string that may not be empty
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="errormsg"></param>
    /// <returns></returns>
    public static string GetString(string prompt = "", string errormsg = "")
    {
        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Console.WriteLine(errormsg);
        } while (true);
    }
    /// <summary>
    /// Get string without numbers, string may not be empty
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="errormsg"></param>
    /// <param name="noNumbers"></param>
    /// <returns></returns>
    /// 
    string getastring = Utilities.GetString("Enter a string: ", "Invalid input, please try again.", true);
    public static string GetString(string prompt, string errormsg, bool noNumbers)
    {
        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && (!noNumbers || !input.Any(char.IsDigit)))
            {
                return input;
            }
            Console.Clear();
            Console.WriteLine(errormsg);
        } while (true);
    }

    //https://www.reddit.com/r/csharp/comments/sbvlgp/is_using_systemnetmailmailaddress_enough_to/
    private static bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(
            email,
            @"^\w+([-+.']\w+)*@(\[*\w+)([-.]\w+)*\.\w+([-.]\w+\])*$"
        );
    }

    public static string FormatCurrency(decimal sum)
    {
        return String.Format(new CultureInfo("sv-SE"), "{0:C2}", sum);
    }

    public static void WriteLineColor(string input, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(input);
        Console.ResetColor();
    }

    public static void PressKeyToProceed()
    {
        Console.WriteLine("Enter key to procceed...");
        Console.ReadKey();
    }

    //Konverterbar till decimal?
    public static decimal GetValidDecimalInput(string prompt, string errorMessage)
    {
        decimal result;
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out result))
            {
                return result;
            }

            Console.WriteLine(errorMessage);
        }
    }

    public static DateTime GetValidDate(string prompt)
    {
        DateTime date;
        while (true)
        {
            Console.WriteLine(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out date))
            {
                return date;
            }
            Console.WriteLine("Invalid date format, please use yyyy-mm-dd format.");
        }
    }
    //Konverterbar till int?
    public static int GetValidIntInput(string prompt, string errorMessage)
    {
        int result;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out result))
            {
                return result;
            }

            Console.WriteLine(errorMessage);
        }
    }
    public static int GetValidIntInput(string prompt, string errorMessage, int minvalue, int maxvalue)
    {
        int result;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out result) && result >= minvalue && result <= maxvalue)
            {
                return result;
            }

            Console.WriteLine(errorMessage);
        }
    }

    public static int GetValidIntInput(string prompt, string errorMessage, int howManyNumbers)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (input.Length == howManyNumbers && int.TryParse(input, out int result))
            {
                return result;
            }

            Console.WriteLine(errorMessage);
        }
    }

    // Om vi ska kolla efter en specifik sträng input
    /// <summary>
    /// Check if string is input1 or input2, return the input
    /// </summary>
    /// <param name="message"></param>
    /// <param name="input1"></param>
    /// <param name="input2"></param>
    /// <returns></returns>
    public static string GetSpecificValidString(string message, string input1, string input2)
    {
        string input;
        do
        {
            Console.WriteLine(message);
            input = Console.ReadLine() ?? string.Empty;
        } while (input != input1 && input != input2);

        return input;
    }
}
