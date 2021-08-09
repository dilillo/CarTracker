using CarTracker.Domain.Commands;
using CarTracker.Domain.Events;
using CarTracker.Domain.Queries;
using CarTracker.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

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
            var model = new AddCarCommand();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCar(AddCarCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                command.ID = Guid.NewGuid().ToString();

                var events = await _mediator.Send(command);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Clear();
                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = command.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(command);
            }
        }

        public ActionResult ChangeOil(string id)
        {
            var model = new ChangeOilCommand { ID = id };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeOil(ChangeOilCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                var events = await _mediator.Send(command);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Clear();
                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = command.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(command);
            }
        }

        public ActionResult NewOwner(string id)
        {
            var model = new NewOwnerCommand { ID = id };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOwner(NewOwnerCommand newOwnerCommand)
        {
            if (!ModelState.IsValid)
            {
                return View(newOwnerCommand);
            }

            try
            {
                var events = await _mediator.Send(newOwnerCommand);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Clear();
                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = newOwnerCommand.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(newOwnerCommand);
            }
        }

        public ActionResult InspectBrakes(string id)
        {
            var model = new InspectBrakesCommand { ID = id };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InspectBrakes(InspectBrakesCommand commmand)
        {
            if (!ModelState.IsValid)
            {
                return View(commmand);
            }

            try
            {
                var events = await _mediator.Send(commmand);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Clear();
                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = commmand.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(commmand);
            }
        }

        public ActionResult ReplaceTires(string id)
        {
            var model = new ReplaceTiresCommand { ID = id };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReplaceTires(ReplaceTiresCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                var events = await _mediator.Send(command);

                var serializedPendingDomainEvents = JsonSerializer.Serialize(events);

                TempData.Clear();
                TempData.Add("PendingDomainEvents", serializedPendingDomainEvents);

                return RedirectToAction("Details", "Home", new { id = command.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(command);
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
