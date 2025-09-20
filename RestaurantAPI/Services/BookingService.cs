using Microsoft.Extensions.Options;
using RestaurantAPI.Constants;
using RestaurantAPI.Data.Repositories.IRepositories;
using RestaurantAPI.Models;
using RestaurantAPI.Models.DTOs.Booking;
using RestaurantAPI.Models.DTOs.Customer;
using RestaurantAPI.Services.IServices;
using BookingStatus = RestaurantAPI.Models.BookingStatus;

namespace RestaurantAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly ITableRepo _tableRepo;
        private readonly ResturantConfig _resturantConfiguration;

        public BookingService(IBookingRepo bookingRepo,
            ICustomerRepo customerRepo,
            ITableRepo tableRepo,
            IOptions<ResturantConfig> resturantConfiguration)
        {
            _bookingRepo = bookingRepo;
            _customerRepo = customerRepo;
            _tableRepo = tableRepo;
            _resturantConfiguration = resturantConfiguration.Value;
        }


        public async Task<List<AvailableTimeSlotsDTO>> GetAvailableTimeSlotsAsync(DateTime date, int numberOfGuests) //slots first, then check availability
        {
            var availableSlots = new List<AvailableTimeSlotsDTO>();

            //1. Generate all possible slots for the day

            var openingTime = date.Date.Add(_resturantConfiguration.OpeningTime);
            var closingTime = date.Date.Add(_resturantConfiguration.ClosingTime);

            for(var slotStartTime = openingTime; slotStartTime <= closingTime; slotStartTime.Add(_resturantConfiguration.BookingSlotInterval))
            {
                availableSlots.Add(new AvailableTimeSlotsDTO
                {
                    DisplayTime = slotStartTime.ToString("HH:mm"),
                });
            }

            //2. Check availability for each time slot
            foreach(var slot in availableSlots)
            {
                var availableTables = await _tableRepo.GetAvailableTablesByCapacityAsync
                    (date, slot.StartTime, _resturantConfiguration.DefaultBookingDuration, numberOfGuests
                    );
                slot.IsAvailable = availableTables != null;
            }
            return availableSlots;
        }

        public async Task<BookingDTO> GetBookingByCustomerPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingDTO> GetBookingByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookingDTO>> GetBookingsByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingMessageDTO> CreateBookingAsync(BookingCreateDTO bookingCreateDTO)
        {
            var bookingStart = TimeSpan.Parse(bookingCreateDTO.DisplayTime);
            var bookingEnd = bookingStart.Add(_resturantConfiguration.DefaultBookingDuration);

            //check available tables again incase someone booked it before booking confirmation
            var availableTables = await _tableRepo.GetAvailableTablesByCapacityAsync(
                bookingCreateDTO.BookingDate,
                bookingStart,
                _resturantConfiguration.DefaultBookingDuration,
                bookingCreateDTO.NumberOfGuests
                );

            var table = availableTables
                .OrderBy(t => t.Capacity)
                .FirstOrDefault();

            //find or create customer
            var customerDTO = bookingCreateDTO.CustomerCreate;
            var customer = await _customerRepo.GetCustomerByPhoneNumberAsync(bookingCreateDTO.CustomerCreate.Phone);
            if (customer == null)
            {
                customer = new Customer
                {
                    FirstName = customerDTO.FirstName,
                    LastName = customerDTO.LastName,
                    Email = customerDTO.Email,
                    Phone = customerDTO.Phone
                };
                await _customerRepo.AddCustomerAsync(customer);
            }

            //create booking
            var booking = new Booking
            {
                FK_TableId = table.Id,
                FK_CustomerId = customer.Id,
                BookingDate = bookingCreateDTO.BookingDate,
                StartTime = bookingStart,
                Duration = _resturantConfiguration.DefaultBookingDuration,
                status = BookingStatus.Confirmed,
                NumberOfGuests = bookingCreateDTO.NumberOfGuests,
            };
            await _bookingRepo.CreateBookingAsync(booking);


            //booking confirmed
            return new BookingMessageDTO
            {
                BookingId = booking.Id,
                BookingDate = booking.BookingDate,
                StartTime= bookingStart,
                TableName = table.TableNumber,
                TableCapacity = table.Capacity,
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                CustomerPhone = customer.Phone,
                CustomerEmail = customer.Email,
                ConfirmationMessage = $"Your booking is confirmed for {booking.StartTime:HH:mm} on {booking.BookingDate:yyyy-MM-dd} , you will soon get an confirmation email."
            };
        }

        public async Task<BookingMessageDTO> DeleteBookingAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<BookingMessageDTO> UpdateBookingAsync(int id, BookingUpdateDTO bookingUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
