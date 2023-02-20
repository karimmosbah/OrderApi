using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Models;

namespace OrderApi.Services
{
    
        public class OrderServices : IOrderServices
        {
            private readonly ApplicationDbContext _context;
            public OrderServices(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Order> Add(Order order)
            {
                await _context.AddAsync(order);
                _context.SaveChanges();
                return order;
            }

            public Order Delete(Order order)
            {
                _context.Remove(order);
                _context.SaveChanges();
                return order;
            }

            public async Task<List<Order>> GetAll()
            {
                return await _context.orders.ToListAsync();
            }

            public async Task<Order> GetById(int id)
            {
                return await _context.orders.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            }

            public Order Update(Order order)
            {
                _context.Update(order);
                _context.SaveChanges();
                return order;
            }
        }
    }


