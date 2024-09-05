using System.Linq;

namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// Contains custom extensions on types in <see cref="Models"/>.
	/// </summary>
	public static class XtensionClass
	{
		/// <summary>
		/// Custom alternative to the <see cref="object.ToString"/> method which only considers properties marked with <see cref="StringableAttribute"/> to be returned as a <see cref="string"/>.
		/// </summary>
		/// <typeparam name="T">A generic type having the marked properties.</typeparam>
		/// <param name="stringableTypeObject">The object of the generic type storing the values of the marked properties.</param>
		/// <returns>A <see cref="string"/> representing <typeparamref name="T"/> type's object.</returns>
		public static string StringableToString<T>(this T stringableTypeObject)
		{
			var tType = typeof(T); // stringableTypeObject.GetType()
			var sb = new System.Text.StringBuilder($"{tType.Name} ( ");
			var stringableProperties = tType.GetProperties()
				.Where(propertyInfo => propertyInfo.GetCustomAttributes(typeof(StringableAttribute), false).Length > 0);

			// if (!stringableProperties.Any()) return $"No {nameof(StringableAttribute)} properties in {tType.Name}!";

			foreach (var stringableProp in stringableProperties)
			{
				sb.Append($"{stringableProp.Name} = {stringableProp.GetValue(stringableTypeObject)}, ");
			}

			return sb.Append(')').ToString();
		}
	}
}
