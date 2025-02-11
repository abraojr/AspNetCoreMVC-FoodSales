﻿using Microsoft.EntityFrameworkCore;
using SalesFood.Context;
using SalesFood.Models;

namespace SalesFood.Areas.Admin.Services;

public class SalesReportService
{
    private readonly AppDbContext _context;

    public SalesReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = from obj in _context.Orders
                     select obj;

        if (minDate.HasValue)
        {
            result = result.Where(x => x.OrderDate >= minDate.Value);
        }

        if (maxDate.HasValue)
        {
            result = result.Where(x => x.OrderDate <= maxDate.Value);
        }

        return await result
                        .Include(o => o.OrderItems)
                        .ThenInclude(f => f.Food)
                        .OrderByDescending(x => x.OrderDate)
                        .ToListAsync();
    }
}