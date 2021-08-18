using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RatingAdjustment.Services;
using BreadmakerReport.Models;

namespace BreadmakerReport
{
    class Program
    {
        static string dbfile = @".\data\breadmakers.db";
        static RatingAdjustmentService ratingAdjustmentService = new RatingAdjustmentService();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bread World");
            var BreadmakerDb = new BreadMakerSqliteContext(dbfile);

            var BMList = BreadmakerDb.Breadmakers

               // .Include(i => i.Reviews)
                .AsEnumerable()
                .Select(bmd => new {
                    Reviews = bmd.Reviews.Count,Average = Math.Round(bmd.Reviews.Average(s => s.stars), 2),
                    Adjust = Math.Round(ratingAdjustmentService.Adjust(bmd.Reviews.Average(s => s.stars), bmd.Reviews.Count()), 2),
                    bmd.title
                })

             //   var BMList = BMListT.Select 
                .OrderByDescending(bmd => bmd.Adjust)
                .ToList();

            Console.WriteLine("[#]  Reviews Average  Adjust    Description");
            for (var j = 0; j < 3; j++)
            {
                var i = BMList[j];
              
               Console.WriteLine("\n[{0}] {1} {2} {3} {4}", j + 1, i.Reviews, i.Average, i.Adjust, i.title); //add output
            } 
        }
    }
}
