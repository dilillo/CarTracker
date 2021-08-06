using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using CarTracker.Domain.Commands;
using CarTracker.Domain.Events;
using CarTracker.Domain.Queries;
using CarTracker.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            var pendingDomainEvents = TempData["PendingDomainEvents"] as string;

            var model = await _mediator.Send(new GetAllCarsQuery
            {
                PendingDomainEvents = string.IsNullOrEmpty(pendingDomainEvents) ? null : JsonSerializer.Deserialize<DomainEvent[]>(pendingDomainEvents)
            });

            return View(model);
        }

        public async Task<ActionResult> Details(string id)
        {
            var model = await _mediator.Send(new GetCarByIDQuery { ID = id });

            return View(model);
        }

        public ActionResult AddCar()
        {
            var model = new AddCarViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCar(AddCarViewModel createCarViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createCarViewModel);
            }

            try
            {
                var cmd = new AddCarCommand
                {
                    ID = Guid.NewGuid().ToString(),
                    Make = createCarViewModel.Make,
                    Model = createCarViewModel.Model,
                    Owner = createCarViewModel.Owner
                };

                var events = await _mediator.Send(cmd);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = cmd.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(createCarViewModel);
            }
        }

        public ActionResult ChangeOil()
        {
            var model = new ChangeOilViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeOil(ChangeOilViewModel changeOilViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(changeOilViewModel);
            }

            try
            {
                var cmd = new ChangeOilCommand
                {
                    ID = changeOilViewModel.ID,
                    Charge = changeOilViewModel.Charge,
                    Mileage = changeOilViewModel.Mileage
                };

                var events = await _mediator.Send(cmd);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = cmd.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(changeOilViewModel);
            }
        }

        public ActionResult InspectBrakes()
        {
            var model = new ChangeOilViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InspectBrakesOil(InspectBrakesViewModel inspectBrakesViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inspectBrakesViewModel);
            }

            try
            {
                var cmd = new InspectBrakesCommand
                {
                    ID = inspectBrakesViewModel.ID,
                    Mileage = inspectBrakesViewModel.Mileage,
                    RemainingPad = inspectBrakesViewModel.RemainingPad
                };

                var events = await _mediator.Send(cmd);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = cmd.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(inspectBrakesViewModel);
            }
        }

        public ActionResult ReplaceTires()
        {
            var model = new ChangeOilViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReplaceTires(ReplaceTiresViewModel replaceTiresViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(replaceTiresViewModel);
            }

            try
            {
                var cmd = new ReplaceTiresCommand
                {
                    ID = replaceTiresViewModel.ID,
                    Charge = replaceTiresViewModel.Charge,
                    NumberOfTiresReplaced = replaceTiresViewModel.NumberOfTiresReplaced
                };

                var events = await _mediator.Send(cmd);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = cmd.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(replaceTiresViewModel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
