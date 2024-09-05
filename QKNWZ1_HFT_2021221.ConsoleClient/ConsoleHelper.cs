using System;
using System.Collections.Generic;
using System.Linq;

namespace QKNWZ1_HFT_2021221.ConsoleClient
{
	/// <summary>
	/// Contains method(s) to provide a nice(r) output to the <see cref="Console"/> window.
	/// </summary>
	public static class ConsoleHelper
	{
		/// <summary>
		/// Prints the contents of <paramref name="input"/> to the console window.
		/// </summary>
		/// <typeparam name="T">The generic type of the items to print.</typeparam>
		/// <param name="input">The collection of items to print.</param>
		/// <param name="title">The title to print before and after the <paramref name="input"/>.</param>
		/// <param name="isCountWanted">Whether to print the number of items in <paramref name="input"/>.</param>
		public static void PrintToConsole<T>(this IEnumerable<T> input, string title = "", bool isCountWanted = true)
		{
			if (input is null)
			{
				throw new ArgumentNullException(nameof(input));
			}
			T[] inputArray = input.ToArray();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(Environment.NewLine + "\t BEGIN: " + title + (isCountWanted ? $"[ {inputArray.Length} items total]" : ""));
			Console.ResetColor();
			foreach (T item in inputArray)
			{
				Console.WriteLine(item.ToString());
			}
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine(Environment.NewLine + "\t END: " + title + (isCountWanted ? $"[ {inputArray.Length} items total]" : ""));
			Console.ResetColor();
			_ = Console.ReadKey();
		}
	}
}
