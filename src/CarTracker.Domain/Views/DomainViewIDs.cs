namespace CarTracker.Domain.Views
{
    public static class DomainViewIDs
    {
        public static string GetAllCarsView => nameof(GetAllCarsView);

        public static string GetCarByIDView(string id) => nameof(GetCarByIDView) + $"({id})";
    }
}
