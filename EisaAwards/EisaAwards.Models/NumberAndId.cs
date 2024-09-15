namespace EisaAwards.Logic
{
    /// <summary>
    /// A custom type that stores two <see cref="int"/> values.
    /// Useful when an object having an <see cref="int"/> ID is associated with the <see cref="int"/> result of a function, but instead of copying the object itself, its unique identifier is sufficient.
    /// </summary>
    public class NumberAndId
    {
        /// <summary>
        /// Gets or Sets the result of a function related to the <see cref="NumberAndId.Id"/> member.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or Sets the ID of an object in relation to the <see cref="NumberAndId.Number"/> member.
        /// </summary>
        public int Id { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is NumberAndId other
                && this.Id == other.Id
                && this.Number == other.Number;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id + this.Number;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[[ {nameof(this.Id)} = {this.Id} \t {nameof(this.Number)} = {this.Number} ]]";
        }
    }
}
