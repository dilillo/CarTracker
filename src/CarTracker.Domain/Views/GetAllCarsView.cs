using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTracker.Domain.Views
{
    public class GetAllCarsView : DomainView
    {
        public GetAllCarsView()
        {
            ID = DomainViewIDs.GetAllCarsView;
        }

        [Display(Name = "Total Charges")]
        public decimal TotalCharges { get; set; }

        public List<GetAllCarsViewCar> Cars { get; set; } = new List<GetAllCarsViewCar>();
    }

    public class GetAllCarsViewCar
    {
        public string ID { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Owner { get; set; }

        [Display(Name = "Last Serviced")]
        public DateTimeOffset? LastServiced { get; set; }
    }
}
