using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Employment
{
    public class OnlineApprovalSetUp
    {

        public int ID { get; set; }
        public string OnlineAppCode { get; set; }
        public string? OnlineAppDesc { get; set; }
        public string? OnlineAppMngr { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public string? EditedBy { get; set; }
        public string? DeviceName { get; set; }

    }
}
