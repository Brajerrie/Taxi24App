using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Taxi24App.EF;
using Taxi24App.Helper;
using Taxi24App.Models;
using Taxi24App.ModelViews;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taxi24App.Enums;

namespace Taxi24App.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class RidersController : ControllerBase
    {
        private readonly Taxi24AppDBContext _context;
        private readonly ILogger<RidersController> _logger;

        public RidersController(Taxi24AppDBContext context, ILogger<RidersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/riders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = new ApiResponse();
            try
            {
                var riders = await _context.Riders.Select(x => new RiderViewModel()
                {
                    RiderId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AlternatePhoneNumber = x.AlternatePhoneNumber,
                    PhoneNumber = x.PhoneNumber,
                    OtherNames = x.OtherNames,
                    DigitalAddress = x.DigitalAddress,
                    ReferralCode = x.ReferralCode,
                    NationalId = x.NationalId,
                    IsActive = x.IsActive,
                    EmailAddress = x.EmailAddress,
                    Location = x.Location,
                }).ToListAsync();

                result.ResponseCode = "000";
                result.Message = "Riders retrieved successfully.";
                result.Data = riders;
                _logger.LogInformation("Successfully retrieved all riders.");
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while retrieving riders: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving riders.");
            }

            return Ok(result);
        }

        // POST: api/riders/search
        [HttpPost("search")]
        public async Task<IActionResult> Search(RiderIndexViewModel viewModel)
        {
            var result = new ApiResponse();
            try
            {
                IQueryable<Rider> query = _context.Riders;

                if (viewModel.RiderId != null)
                {
                    query = query.Where(x => x.Id == viewModel.RiderId);
                }

                if (viewModel.Location != null)
                {
                    query = query.Where(x => x.Location == viewModel.Location);
                }

                var riders = await query.Select(x => new RiderViewModel()
                {
                    RiderId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AlternatePhoneNumber = x.AlternatePhoneNumber,
                    PhoneNumber = x.PhoneNumber,
                    OtherNames = x.OtherNames,
                    DigitalAddress = x.DigitalAddress,
                    ReferralCode = x.ReferralCode,
                    NationalId = x.NationalId,
                    IsActive = x.IsActive,
                    EmailAddress = x.EmailAddress,
                    Location = x.Location,
                }).ToListAsync();

                result.ResponseCode = "000";
                result.Message = "Search completed successfully.";
                result.Data = riders;
                _logger.LogInformation("Successfully completed search for riders.");
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while searching for riders: {ex.Message}";
                _logger.LogError(ex, "An error occurred while searching for riders.");
            }

            return Ok(result);
        }

        // POST: api/riders/availabledrivers
        [HttpPost("availabledrivers")]
        public async Task<IActionResult> AvailableDrivers(RiderIndexViewModel viewModel)
        {
            var result = new ApiResponse();
            try
            {
                var rider = await _context.Riders.Where(x => x.Id == viewModel.RiderId).Select(x => new RiderViewModel()
                {
                    RiderId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AlternatePhoneNumber = x.AlternatePhoneNumber,
                    PhoneNumber = x.PhoneNumber,
                    OtherNames = x.OtherNames,
                    DigitalAddress = x.DigitalAddress,
                    ReferralCode = x.ReferralCode,
                    NationalId = x.NationalId,
                    IsActive = x.IsActive,
                    EmailAddress = x.EmailAddress,
                    Location = x.Location,
                }).FirstOrDefaultAsync();

                if (rider == null)
                {
                    result.ResponseCode = "404";
                    result.Message = "Rider not found.";
                    _logger.LogWarning("Rider with ID {RiderId} not found.", viewModel.RiderId);
                    return NotFound(result);
                }

                var drivers = await _context.Drivers.Where(x => x.Location == rider.Location && x.Status == DriverStatus.Available)
                    .Select(x => new DriverViewModel()
                    {
                        DriverId = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        AlternatePhoneNumber = x.AlternatePhoneNumber,
                        PhoneNumber = x.PhoneNumber,
                        OtherNames = x.OtherNames,
                        DigitalAddress = x.DigitalAddress,
                        Driverlicence = x.Driverlicence,
                        IsActive = x.IsActive,
                        Status = x.Status,
                        EmailAddress = x.EmailAddress,
                        Location = x.Location,
                    }).ToListAsync();

                var data = new RiderLocationViewModel
                {
                    Rider = rider,
                    Drivers = drivers
                };

                result.ResponseCode = "000";
                result.Message = "Available drivers retrieved successfully.";
                result.Data = data;
                _logger.LogInformation("Successfully retrieved available drivers for rider with ID {RiderId}.", viewModel.RiderId);
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while retrieving available drivers: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving available drivers for rider with ID {RiderId}.", viewModel.RiderId);
            }

            return Ok(result);
        }

        // GET: api/riders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRider(int id)
        {
            var result = new ApiResponse();
            try
            {
                var rider = await _context.Riders.Where(x => x.Id == id).Select(x => new RiderViewModel()
                {
                    RiderId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AlternatePhoneNumber = x.AlternatePhoneNumber,
                    PhoneNumber = x.PhoneNumber,
                    OtherNames = x.OtherNames,
                    DigitalAddress = x.DigitalAddress,
                    ReferralCode = x.ReferralCode,
                    NationalId = x.NationalId,
                    IsActive = x.IsActive,
                    EmailAddress = x.EmailAddress,
                    Location = x.Location,
                }).FirstOrDefaultAsync();

                if (rider == null)
                {
                    result.ResponseCode = "404";
                    result.Message = "Rider not found.";
                    _logger.LogWarning("Rider with ID {RiderId} not found.", id);
                    return NotFound(result);
                }

                result.ResponseCode = "000";
                result.Message = "Rider retrieved successfully.";
                result.Data = rider;
                _logger.LogInformation("Successfully retrieved rider with ID {RiderId}.", id);
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while retrieving the rider: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving rider with ID {RiderId}.", id);
            }

            return Ok(result);
        }

        // POST: api/riders
        [HttpPost]
        public async Task<IActionResult> Create(CreateRiderViewModel data)
        {
            var result = new ApiResponse();
            if (ModelState.IsValid)
            {
                try
                {
                    var rider = new Rider
                    {
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        AlternatePhoneNumber = data.AlternatePhoneNumber,
                        PhoneNumber = data.PhoneNumber,
                        OtherNames = data.OtherNames,
                        DigitalAddress = data.DigitalAddress,
                        NationalId = data.NationalId,
                        IsActive = data.IsActive,
                        EmailAddress = data.EmailAddress,
                        Location = data.Location,
                        CreatedOn = DateTime.Now,
                    };

                    _context.Riders.Add(rider);
                    await _context.SaveChangesAsync();

                    result.ResponseCode = "000";
                    result.Message = "Rider created successfully.";
                    result.Data = rider;
                    _logger.LogInformation("Successfully created a new rider.");
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "005";
                    result.Message = $"An error occurred while creating the rider: {ex.Message}";
                    _logger.LogError(ex, "An error occurred while creating a new rider.");
                }
            }
            else
            {
                result.ResponseCode = "422";
                result.Message = "Invalid data provided.";
                result.Data = ModelState;
                _logger.LogWarning("Invalid data provided for creating a rider.");
                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        // PUT: api/riders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRiderViewModel data)
        {
            var result = new ApiResponse();
            if (ModelState.IsValid)
            {
                var rider = await _context.Riders.FindAsync(id);

                if (rider == null)
                {
                    result.ResponseCode = "404";
                    result.Message = "Rider not found.";
                    _logger.LogWarning("Rider with ID {RiderId} not found for update.", id);
                    return NotFound(result);
                }

                try
                {
                    rider.FirstName = data.FirstName;
                    rider.LastName = data.LastName;
                    rider.AlternatePhoneNumber = data.AlternatePhoneNumber;
                    rider.PhoneNumber = data.PhoneNumber;
                    rider.OtherNames = data.OtherNames;
                    rider.DigitalAddress = data.DigitalAddress;
                    rider.ReferralCode = data.ReferralCode;
                    rider.EmailAddress = data.EmailAddress;
                    rider.UpdatedOn = DateTime.Now;

                    await _context.SaveChangesAsync();

                    result.ResponseCode = "000";
                    result.Message = "Rider updated successfully.";
                    result.Data = rider;
                    _logger.LogInformation("Successfully updated rider with ID {RiderId}.", id);
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "005";
                    result.Message = $"An error occurred while updating the rider: {ex.Message}";
                    _logger.LogError(ex, "An error occurred while updating rider with ID {RiderId}.", id);
                }
            }
            else
            {
                result.ResponseCode = "422";
                result.Message = "Invalid data provided.";
                result.Data = ModelState;
                _logger.LogWarning("Invalid data provided for updating rider with ID {RiderId}.", id);
                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        // DELETE: api/riders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ApiResponse();
            var rider = await _context.Riders.FindAsync(id);

            if (rider == null)
            {
                result.ResponseCode = "404";
                result.Message = "Rider not found.";
                _logger.LogWarning("Rider with ID {RiderId} not found for deletion.", id);
                return NotFound(result);
            }

            try
            {
                _context.Riders.Remove(rider);
                await _context.SaveChangesAsync();

                result.ResponseCode = "000";
                result.Message = "Rider deleted successfully.";
                _logger.LogInformation("Successfully deleted rider with ID {RiderId}.", id);
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while deleting the rider: {ex.Message}";
                _logger.LogError(ex, "An error occurred while deleting rider with ID {RiderId}.", id);
            }

            return Ok(result);
        }
    }
}
