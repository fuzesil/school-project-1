namespace EisaAwards.ConsoleApp
{
    using System;

    /// <summary>
    /// Helper class that includes methods to parse the user's input.
    /// </summary>
    public class ConsoleUserInput
    {
        /// <summary>
        /// Gets the <see cref="int"/> that was successfully parsed the last time when <see cref="IsNumberFromUser(string, bool)"/> was called.
        /// </summary>
        public int Parsed { get; private set; }

        /// <summary>
        /// Gets the <see cref="string"/> that could not be parsed the last time when <see cref="IsNumberFromUser(string, bool)"/> was called.
        /// </summary>
        public string ParseSubject { get; private set; }

        /// <summary>
        /// Gets the <see cref="string"/> that was input the last time when <see cref="IsTextFromUser(string, bool)"/> was called.
        /// </summary>
        public string UserInput { get; private set; }

        /// <summary>
        /// Returns <c>true</c> if the <see cref="string"/> input from the user was .
        /// <para>Set <paramref name="isMessageCustom"/> to <c>true</c> if you have a custom message instead of a short decription of the expected input.</para>
        /// </summary>
        /// <remarks>Formatting the custom message to the user (line breaks, tabs) is possible,
        /// if <paramref name="message"/> contains them.</remarks>
        /// <param name="message">The description of the expected user input.</param>
        /// <param name="isMessageCustom">Whether or not a custom message should be displayed to the user.</param>
        /// <returns>User's input as <see cref="string"/>.</returns>
        public bool IsTextFromUser(string message = "", bool isMessageCustom = false)
        {
            do
            {
                Console.Write(isMessageCustom ? message : "\nInput text for [" + message + "] then press Enter - or an empty pair of parentheses to abort, i.e. \"()\" without quotes.\t: ");
                this.UserInput = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(this.UserInput));
            if (this.UserInput == "()")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("ABORTED!\tPress a key to continue.");
                Console.ResetColor();
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns <c>true</c> if the input from the user was sucessfully parsed to <see cref="int"/>.
        /// <para>Set <paramref name="isMessageCustom"/> to <c>true</c> if you have a custom message instead of a short decription of the expected input.</para>
        /// </summary>
        /// <remarks>Formatting the custom message to the user (line breaks, tabs) is possible,
        /// if <paramref name="message"/> contains them.</remarks>
        /// <param name="message">Description of the input.</param>
        /// <param name="isMessageCustom">Whether or not a custom message should be displayed to the user.</param>
        /// <returns>Whether or not the user's input could be parsed to an <see cref="int"/> value.</returns>
        public bool IsNumberFromUser(string message, bool isMessageCustom = false)
        {
            bool success;
            int result;
            do
            {
                do
                {
                    Console.Write(isMessageCustom ? message : "\nInput text for [" + message + "] then press Enter - or an empty pair of parentheses to abort, i.e. \"()\" without quotes.\t: ");
                    this.ParseSubject = Console.ReadLine();
                }
                while (string.IsNullOrWhiteSpace(this.ParseSubject));
                if (this.ParseSubject == "()")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("ABORTED!\tPress a key to continue.");
                    Console.ResetColor();
                    return false;
                }

                success = int.TryParse(this.ParseSubject, out result);
            }
            while (!success);
            this.Parsed = result;
            return true;
        }
    }
}
