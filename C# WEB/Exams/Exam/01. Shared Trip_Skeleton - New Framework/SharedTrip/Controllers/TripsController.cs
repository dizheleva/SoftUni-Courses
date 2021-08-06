using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext data;

        public TripsController(ApplicationDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public HttpResponse All()
        {
            var trips = this.data
                .Trips
                .Select(t => new TripViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    SeatsAvailable = t.Seats - t.UserTrips.Count()
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripFormModel model)
        {
            var parsedDate = DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var date);

            if (model.DepartureTime == null || !parsedDate
                || model.StartPoint == null
                || model.EndPoint == null
                || model.ImagePath.Any() && !Uri.IsWellFormedUriString(model.ImagePath, UriKind.Absolute)
                || model.Seats is < 2 or > 6
                || String.IsNullOrEmpty(model.Description) || model.Description.Length > 80)
            {
                return Redirect("/Trips/Add");
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = date,
                ImagePath = model.ImagePath,
                Seats = model.Seats,
                Description = model.Description
            };

            data.Trips.Add(trip);

            data.UsersTrips.Add(new UserTrip { TripId = trip.Id, UserId = this.User.Id });

            data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = this.data.Trips.First(i => i.Id == tripId);

            var tripDetails = new TripDetailsViewModel
            {
                Id = tripId,
                EndPoint = trip.EndPoint,
                StartPoint = trip.StartPoint,
                DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                SeatsAvailable = trip.Seats - trip.UserTrips.Count(),
                ImagePath = trip.ImagePath,
                Description = trip.Description
            };

            return View(tripDetails);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            if (this.data.UsersTrips.Any(ut => ut.UserId == this.User.Id && ut.TripId == tripId))
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            this.data.UsersTrips.Add(new UserTrip { UserId = this.User.Id, TripId = tripId });

            data.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}