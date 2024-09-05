[assembly: System.CLSCompliant(false)]
namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>Indicates that the <see cref="System.AttributeTargets.Property"/> it is placed on shall be included in a cutom/extended <see cref="object.ToString"/>'s return.</summary>
	[System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
	public sealed class StringableAttribute : System.Attribute
	{
	}
}
