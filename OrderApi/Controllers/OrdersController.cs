using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Dtos;
using OrderApi.Models;
using OrderApi.Services;
using System.Linq;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderServices _orderServices;
        private readonly IWebHostEnvironment HostingEnvironment;
        private readonly ApplicationDbContext _context;

        public OrdersController(IOrderServices orderServices, IMapper mapper, IWebHostEnvironment HostingEnvironment, ApplicationDbContext context)
        {
            _orderServices = orderServices;
            _mapper = mapper;
            this.HostingEnvironment = HostingEnvironment;
            _context = context;
        }

        //Pagination
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.orders.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).ToListAsync();
            var Response = await _context.orders.ToListAsync();
            return Ok(Response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var order = await _orderServices.GetById(id);
            if (order == null)
                return NotFound();
            var dto = _mapper.Map<OrderListDto>(order);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] OrderDto dto)
        {
            
            if (dto.Image != null)
            {
                string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");

                string filePath = Path.Combine(uploadFolder, dto.Image.FileName);

                await dto.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            }
            var order = _mapper.Map<Order>(dto);
            order.Image = dto.Image.FileName;
           await _orderServices.Add(order);
            return Ok(order);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int id, [FromForm] EditOrderDto dto)
        {

            var order = await _orderServices.GetById(id);
            if (order == null)
                return NotFound($"No order was found with ID: {id}");

            var FileName = "";
            if (dto.Image != null)
            {
                string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");

                string filePath = Path.Combine(uploadFolder, dto.Image.FileName);

                await dto.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
                FileName = dto.Image.FileName;
            }
            if(FileName == "")
            {
                order.Image = order.Image;
            }
            else
            {
                order.Image = FileName;
            }
            order.Name = dto.Name;
            order.Price = dto.Price;
            order.Quantity = dto.Quantity;
             _orderServices.Update(order);
            return Ok(order);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var order = await _orderServices.GetById(id);
            if (order == null)
                return NotFound($"No order was found with ID: {id}");
            _orderServices.Delete(order);
            return Ok(order);
        }
    }
}
