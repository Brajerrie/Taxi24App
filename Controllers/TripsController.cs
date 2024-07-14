using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Taxi24App.EF;
using Taxi24App.Enums;
using Taxi24App.Helper;
using Taxi24App.Models;
using Taxi24App.ModelViews;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Taxi24App.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly Taxi24AppDBContext _context;
        private readonly ILogger<TripsController> _logger;

        public TripsController(Taxi24AppDBContext context, ILogger<TripsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/trips
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = new ApiResponse();
            try
            {
                var trips = await _context.Trips.Select(x => new TripViewModel()
                {
                    TripId = x.Id,
                    DriverName = x.Driver.FirstName,
                    DriverPhonenumber = x.Driver.PhoneNumber,
                    RiderName = x.Rider.FirstName,
                    RiderPhoneNumber = x.Rider.PhoneNumber,
                    DocumentNumber = x.DocumentNumber,
                    StartTIme = x.StartTIme,
                    Origin = x.Origin,
                    Destination = x.Destination,
                    TotalAmount = x.TotalAmount,
                }).ToListAsync();

                result.ResponseCode = "000";
                result.Message = "Trips retrieved successfully.";
                result.Data = trips;
                _logger.LogInformation("Successfully retrieved all trips.");
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while retrieving trips: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving trips.");
            }

            return Ok(result);
        }

        // POST: api/trips/search
        [HttpPost("search")]
        public async Task<IActionResult> Search(CreateTripViewModel viewModel)
        {
            var result = new ApiResponse();
            try
            {
                IQueryable<Trip> query = _context.Trips;

                if (viewModel.DriverId != null)
                {
                    query = query.Where(x => x.DriverId == viewModel.DriverId);
                }

                if (viewModel.Status != null)
                {
                    var status = (TripStatus?)viewModel.Status;
                    query = query.Where(x => x.Status == status);
                }

                if (viewModel.RiderId != null)
                {
                    query = query.Where(x => x.RiderId == viewModel.RiderId);
                }

                var trips = await query.Select(x => new TripViewModel()
                {
                    TripId = x.Id,
                    DriverName = x.Driver.FirstName,
                    DriverPhonenumber = x.Driver.PhoneNumber,
                    RiderName = x.Rider.FirstName,
                    RiderPhoneNumber = x.Rider.PhoneNumber,
                    DocumentNumber = x.DocumentNumber,
                    StartTIme = x.StartTIme,
                    Origin = x.Origin,
                    Destination = x.Destination,
                    TotalAmount = x.TotalAmount,
                }).ToListAsync();

                result.ResponseCode = "000";
                result.Message = "Search completed successfully.";
                result.Data = trips;
                _logger.LogInformation("Successfully completed search for trips.");
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while searching for trips: {ex.Message}";
                _logger.LogError(ex, "An error occurred while searching for trips.");
            }

            return Ok(result);
        }

        // GET: api/trips/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip(int id)
        {
            var result = new ApiResponse();
            try
            {
                var trip = await _context.Trips.Where(x => x.Id == id).Select(x => new TripViewModel()
                {
                    TripId = x.Id,
                    DriverName = x.Driver.FirstName,
                    DriverPhonenumber = x.Driver.PhoneNumber,
                    RiderName = x.Rider.FirstName,
                    RiderPhoneNumber = x.Rider.PhoneNumber,
                    DocumentNumber = x.DocumentNumber,
                    StartTIme = x.StartTIme,
                    Origin = x.Origin,
                    Destination = x.Destination,
                    TotalAmount = x.TotalAmount,
                }).FirstOrDefaultAsync();

                if (trip == null)
                {
                    result.ResponseCode = "404";
                    result.Message = "Trip not found.";
                    _logger.LogWarning("Trip with ID {TripId} not found.", id);
                    return NotFound(result);
                }

                result.ResponseCode = "000";
                result.Message = "Trip retrieved successfully.";
                result.Data = trip;
                _logger.LogInformation("Successfully retrieved trip with ID {TripId}.", id);
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while retrieving the trip: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving trip with ID {TripId}.", id);
            }

            return Ok(result);
        }

        // POST: api/trips
        [HttpPost]
        public async Task<IActionResult> Create(CreateTripViewModel data)
        {
            var result = new ApiResponse();
            if (ModelState.IsValid)
            {
                try
                {
                    var trip = new Trip
                    {
                        DocumentNumber = Util.RandomString(8),
                        Refereence = data.Refereence,
                        RiderId = data.RiderId,
                        DriverId = data.DriverId,
                        StartTIme = DateTime.Now,
                        Origin = data.Origin,
                        Destination = data.Destination,
                        TotalAmount = data.TotalAmount,
                        IsActive = true,
                        Status = data.Status,
                        CreatedOn = DateTime.Now,
                    };

                    _context.Trips.Add(trip);
                    await _context.SaveChangesAsync();

                    result.ResponseCode = "000";
                    result.Message = "Trip created successfully.";
                    result.Data = trip;
                    _logger.LogInformation("Successfully created a new trip.");
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "005";
                    result.Message = $"An error occurred while creating the trip: {ex.Message}";
                    _logger.LogError(ex, "An error occurred while creating a new trip.");
                }
            }
            else
            {
                result.ResponseCode = "422";
                result.Message = "Invalid data provided.";
                result.Data = ModelState;
                _logger.LogWarning("Invalid data provided for creating a trip.");
                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        // PUT: api/trips/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTripViewModel data)
        {
            var result = new ApiResponse();
            if (ModelState.IsValid)
            {
                var trip = await _context.Trips.FindAsync(id);

                if (trip == null)
                {
                    result.ResponseCode = "404";
                    result.Message = "Trip not found.";
                    _logger.LogWarning("Trip with ID {TripId} not found for update.", id);
                    return NotFound(result);
                }

                try
                {
                    trip.Status = TripStatus.Completed;
                    await _context.SaveChangesAsync();

                    result.ResponseCode = "000";
                    result.Message = "Trip updated successfully.";
                    result.Data = trip;
                    _logger.LogInformation("Successfully updated trip with ID {TripId}.", id);
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "005";
                    result.Message = $"An error occurred while updating the trip: {ex.Message}";
                    _logger.LogError(ex, "An error occurred while updating trip with ID {TripId}.", id);
                }
            }
            else
            {
                result.ResponseCode = "422";
                result.Message = "Invalid data provided.";
                result.Data = ModelState;
                _logger.LogWarning("Invalid data provided for updating trip with ID {TripId}.", id);
                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        // DELETE: api/trips/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ApiResponse();
            var trip = await _context.Trips.FindAsync(id);

            if (trip == null)
            {
                result.ResponseCode = "404";
                result.Message = "Trip not found.";
                _logger.LogWarning("Trip with ID {TripId} not found for deletion.", id);
                return NotFound(result);
            }

            try
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();

                result.ResponseCode = "000";
                result.Message = "Trip deleted successfully.";
                _logger.LogInformation("Successfully deleted trip with ID {TripId}.", id);
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while deleting the trip: {ex.Message}";
                _logger.LogError(ex, "An error occurred while deleting trip with ID {TripId}.", id);
            }

            return Ok(result);
        }
    }
}
