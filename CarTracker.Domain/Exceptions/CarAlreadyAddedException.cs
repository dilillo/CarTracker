namespace CarTracker.Domain.Exceptions
{
    public class CarAlreadyAddedException : ValidationException
    {
        public CarAlreadyAddedException() : base("Car has already been added.")
        {
        }
    }
}
