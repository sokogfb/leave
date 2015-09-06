﻿using LeaveApplication.Context;
using LeaveApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveApplication.Controllers
{
    public class StatsController : Controller
    {
        private LeaveAppDbContext db = new LeaveAppDbContext();

        // GET: Stats
        public string Index()
        {
            int total = db.Leaves.Count();

            string result = String.Format("Total of {0} records since September 2012. ", total);

            var data = db.Database.SqlQuery<ReportResult>("SpStats");

            Report report = new Report();
            report.ReportResults = data.ToArray();

            result += "TOP SCORERS OF " + DateTime.Now.Year + ": ";
            string prevLTypName = "";

            foreach (var item in report.ReportResults)
            {
                if (!prevLTypName.Equals(item.LTypName))
                {
                    prevLTypName = item.LTypName;                    
                }
                else
                {
                    continue;
                }

                result += string.Format("{0}: {1} ({2}) {3} ", item.LTypName, item.EmployeeName, item.NoOfDays, "*****");
            }

            return result;
        }
    }
}