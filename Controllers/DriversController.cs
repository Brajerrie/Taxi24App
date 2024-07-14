using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Taxi24App.EF;
using Taxi24App.Enums;
using Taxi24App.Models;
using Taxi24App.ModelViews;
using System;
using System.Linq;
using System.Threading.Tasks;
using Taxi24App.Helper;

namespace Taxi24App.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly Taxi24AppDBContext _context;
        private readonly ILogger<DriversController> _logger;

        public DriversController(Taxi24AppDBContext context, ILogger<DriversController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/drivers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = new ApiResponse();
            try
            {
                var drivers = await _context.Drivers.Select(x => new DriverViewModel()
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

                result.ResponseCode = "000";
                result.Message = "Drivers retrieved successfully.";
                result.Data = drivers;
                _logger.LogInformation("Successfully retrieved all drivers.");
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while retrieving drivers: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving drivers.");
            }

            return Ok(result);
        }

        // POST: api/drivers/search
        [HttpPost("search")]
        public async Task<IActionResult> Search(DriverIndexViewModel viewModel)
        {
            var result = new ApiResponse();
            try
            {
                IQueryable<Driver> query = _context.Drivers;

                if (viewModel.DriverId != null)
                {
                    query = query.Where(x => x.Id == viewModel.DriverId);
                }

                if (viewModel.Status != null)
                {
                    var status = (DriverStatus?)viewModel.Status;
                    query = query.Where(x => x.Status == status);
                }

                if (viewModel.Location != null)
                {
                    query = query.Where(x => x.Location == viewModel.Location);
                }

                var drivers = await query.Select(x => new DriverViewModel()
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

                result.ResponseCode = "000";
                result.Message = "Search completed successfully.";
                result.Data = drivers;
                _logger.LogInformation("Successfully completed search for drivers.");
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while searching for drivers: {ex.Message}";
                _logger.LogError(ex, "An error occurred while searching for drivers.");
            }

            return Ok(result);
        }

        // GET: api/drivers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver(int id)
        {
            var result = new ApiResponse();
            try
            {
                var driver = await _context.Drivers.Where(x => x.Id == id).Select(x => new DriverViewModel()
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
                }).FirstOrDefaultAsync();

                if (driver == null)
                {
                    result.ResponseCode = "404";
                    result.Message = "Driver not found.";
                    _logger.LogWarning("Driver with ID {DriverId} not found.", id);
                    return NotFound(result);
                }

                result.ResponseCode = "000";
                result.Message = "Driver retrieved successfully.";
                result.Data = driver;
                _logger.LogInformation("Successfully retrieved driver with ID {DriverId}.", id);
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while retrieving the driver: {ex.Message}";
                _logger.LogError(ex, "An error occurred while retrieving driver with ID {DriverId}.", id);
            }

            return Ok(result);
        }

        // POST: api/drivers
        [HttpPost]
        public async Task<IActionResult> Create(CreateDriverViewModel data)
        {
            var result = new ApiResponse();
            if (ModelState.IsValid)
            {
                if (await _context.Drivers.AnyAsync(d => d.EmailAddress == data.EmailAddress))
                {
                    ModelState.AddModelError("EmailAddress", "Email address is already in use.");
                }

                if (await _context.Drivers.AnyAsync(d => d.PhoneNumber == data.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number is already in use.");
                }

                if (!ModelState.IsValid)
                {
                    result.ResponseCode = "422";
                    result.Message = "Invalid data provided.";
                    result.Data = ModelState;
                    _logger.LogWarning("Attempt to create driver with duplicate email or phone number.");
                    return UnprocessableEntity(result);
                }

                try
                {
                    var driver = new Driver
                    {
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        AlternatePhoneNumber = data.AlternatePhoneNumber,
                        PhoneNumber = data.PhoneNumber,
                        OtherNames = data.OtherNames,
                        DigitalAddress = data.DigitalAddress,
                        Driverlicence = data.Driverlicence,
                        IsActive = data.IsActive,
                        Status = DriverStatus.Unavailable,
                        EmailAddress = data.EmailAddress,
                        Location = data.Location,
                        CreatedOn = DateTime.Now,
                    };

                    _context.Drivers.Add(driver);
                    await _context.SaveChangesAsync();

                    result.ResponseCode = "000";
                    result.Message = "Driver created successfully.";
                    result.Data = data;
                    _logger.LogInformation("Successfully created a new driver.");
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "005";
                    result.Message = $"An error occurred while creating the driver: {ex.Message}";
                    _logger.LogError(ex, "An error occurred while creating a new driver.");
                }
            }
            else
            {
                result.ResponseCode = "422";
                result.Message = "Invalid data provided.";
                result.Data = ModelState;
                _logger.LogWarning("Invalid data provided for creating a driver.");
                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        // PUT: api/drivers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDriverViewModel data)
        {
            var result = new ApiResponse();
            if (ModelState.IsValid)
            {
                var driver = await _context.Drivers.FindAsync(id);

                if (driver == null)
                {
                    result.ResponseCode = "404";
                    result.Message = "Driver not found.";
                    _logger.LogWarning("Driver with ID {DriverId} not found for update.", id);
                    return NotFound(result);
                }

                if (await _context.Drivers.AnyAsync(d => d.EmailAddress == data.EmailAddress && d.Id != id))
                {
                    ModelState.AddModelError("EmailAddress", "Email address is already in use.");
                }

                if (await _context.Drivers.AnyAsync(d => d.PhoneNumber == data.PhoneNumber && d.Id != id))
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number is already in use.");
                }

                if (!ModelState.IsValid)
                {
                    result.ResponseCode = "422";
                    result.Message = "Invalid data provided.";
                    result.Data = ModelState;
                    _logger.LogWarning("Attempt to create driver with duplicate email or phone number.");
                    return UnprocessableEntity(result);
                }

                try
                {
                    driver.FirstName = data.FirstName;
                    driver.LastName = data.LastName;
                    driver.AlternatePhoneNumber = data.AlternatePhoneNumber;
                    driver.PhoneNumber = data.PhoneNumber;
                    driver.OtherNames = data.OtherNames;
                    driver.DigitalAddress = data.DigitalAddress;
                    driver.Driverlicence = data.Driverlicence;
                    driver.EmailAddress = data.EmailAddress;
                    driver.UpdatedOn = DateTime.Now;

                    await _context.SaveChangesAsync();

                    result.ResponseCode = "000";
                    result.Message = "Driver updated successfully.";
                    result.Data = data;
                    _logger.LogInformation("Successfully updated driver with ID {DriverId}.", id);
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "005";
                    result.Message = $"An error occurred while updating the driver: {ex.Message}";
                    _logger.LogError(ex, "An error occurred while updating driver with ID {DriverId}.", id);
                }
            }
            else
            {
                result.ResponseCode = "422";
                result.Message = "Invalid data provided.";
                result.Data = ModelState;
                _logger.LogWarning("Invalid data provided for updating driver with ID {DriverId}.", id);
                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        // DELETE: api/drivers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ApiResponse();
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                result.ResponseCode = "404";
                result.Message = "Driver not found.";
                _logger.LogWarning("Driver with ID {DriverId} not found for deletion.", id);
                return NotFound(result);
            }

            try
            {
                _context.Drivers.Remove(driver);
                await _context.SaveChangesAsync();

                result.ResponseCode = "000";
                result.Message = "Driver deleted successfully.";
                _logger.LogInformation("Successfully deleted driver with ID {DriverId}.", id);
            }
            catch (Exception ex)
            {
                result.ResponseCode = "005";
                result.Message = $"An error occurred while deleting the driver: {ex.Message}";
                _logger.LogError(ex, "An error occurred while deleting driver with ID {DriverId}.", id);
            }

            return Ok(result);
        }
    }
}
