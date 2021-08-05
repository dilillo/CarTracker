namespace CarTracker.Domain.Exceptions
{
    public class OwnershipChangeException : ValidationException
    {
        public OwnershipChangeException() : base("New owner cannot be the same as old owner.")
        {
        }
    }
}
