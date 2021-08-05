namespace CarTracker.Domain.Exceptions
{
    public class CarMustBeAddedFirstException : ValidationException
    {
        public CarMustBeAddedFirstException() : base("Car must be added first.")
        {
        }
    }
}
