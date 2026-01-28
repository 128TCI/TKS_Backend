using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings
{
    public class AllowancePerClassificationSetUpDTO
    {
        public int ID { get; set; }

        public string? RefNo { get; set; }
        public string? AllowanceCode { get; set; }
        public string? WorkShiftCode { get; set; }

        public decimal? MinHrsRegDay { get; set; }
        public decimal? MinAmtRegDay { get; set; }
        public decimal? MaxHrsRegDay { get; set; }
        public decimal? MaxAmtRegDay { get; set; }

        public decimal? MinHrsRestDay { get; set; }
        public decimal? MinAmtRestDay { get; set; }
        public decimal? MaxHrsRestDay { get; set; }
        public decimal? MaxAmtRestDay { get; set; }

        public decimal? MinHrsHoliday { get; set; }
        public decimal? MinAmtHoliday { get; set; }
        public decimal? MaxHrsHoliday { get; set; }
        public decimal? MaxAmtHoliday { get; set; }

        public decimal? MinHrsHolidayRestDay { get; set; }
        public decimal? MinAmountHolidayRestDay { get; set; }
        public decimal? MaxHrsHolidayRestDay { get; set; }
        public decimal? MaxAmountHolidayRestDay { get; set; }

        public string? ClassificationCode { get; set; }
    }
}
