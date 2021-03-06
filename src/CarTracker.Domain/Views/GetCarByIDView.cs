using System;
using System.ComponentModel.DataAnnotations;

namespace CarTracker.Domain.Views
{
    public class GetCarByIDView : DomainView
    {
        public GetCarByIDView()
        {
        }

        public GetCarByIDView(string id)
        {
            ID = DomainViewIDs.GetCarByIDView(id);
        }

        public GetCarByIDViewCar Car { get; set; }
    }

    public class GetCarByIDViewCar
    {
        public string ID { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Owner { get; set; }

        public int Mileage { get; set; }

        [Display(Name = "Total Charges")]
        public decimal TotalCharges { get; set; }

        [Display(Name = "Last Oil Change")]
        public DateTimeOffset? LastOilChange { get; set; }

        [Display(Name = "Remaining Pad")]
        public double RemainingPad { get; set; }
    }
}
