using CarTracker.Domain.Commands;
using CarTracker.Domain.Events;
using CarTracker.Domain.Queries;
using CarTracker.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
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

        public ActionResult Create()
        {
            var model = new CreateCarViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCarViewModel createCarViewModel)
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
