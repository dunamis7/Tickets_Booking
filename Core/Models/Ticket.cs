using Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Ticket
    {
        public int? TicketID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Ticket_EnsureOwnerValidation]
        public string Owner { get; set; }

        [Ticket_EnsureDueDatePresent]
        [Ticket_EnsureFutureDueDateOnCreation]
        [Ticket_EnsureDueDateAfterReportDate]
        public DateTime? DueDate { get; set; } //every nuallable field has the hasvalue method to verify if it has a value or not

        [Ticket_EnsureFutureReportDateOnCreation]
        [Ticket_EnsureReportDatePresent]
        public DateTime? ReportDate { get; set; }

        public Project Project { get; set; } //one ticket is associated with one project

        //Capture your validations with plain c sharp classes for the sake of changes in technology
        
        //summary
        //when creating a ticket, if due date is entered, it has to be in the future
        
        public bool ValidateFutureDueDate()
        {
           // if (TicketID.HasValue) return true;
            if(!DueDate.HasValue) return true;

            return !(DueDate.Value > DateTime.Now);
        }


        //summary
        //when creating a ticket, if report date is entered, it has to be in the future

        public bool ValidateFutureReportDate()
        {
           // if (TicketID.HasValue) return true;
            if (!ReportDate.HasValue) return true;
            if (ReportDate.Value < DateTime.Now) return true;

            return !(ReportDate.Value > DateTime.Now);
        }

        //summary
        //When owner is assigned, the report date has to be present
        public bool ValidateReportDatePresence()
        {
            if(DueDate.HasValue && !ReportDate.HasValue) return true;
            if (!ReportDate.HasValue) return true;
            if (!string.IsNullOrWhiteSpace(Owner) && !ReportDate.HasValue) return true;
            if (!string.IsNullOrWhiteSpace(Owner) && DueDate.HasValue && !ReportDate.HasValue) return true;
           
            return !ReportDate.HasValue;
        }

        //summary
        //When owner is assigned, the due date has to be present
        public bool ValidateDueDatePresence()
        {
            
            if (ReportDate.HasValue && !DueDate.HasValue) return true;
            if (!DueDate.HasValue) return true;
            if (!string.IsNullOrWhiteSpace(Owner) && !DueDate.HasValue) return true;
            if (!string.IsNullOrWhiteSpace(Owner) && ReportDate.HasValue && !DueDate.HasValue) return true;



            return !DueDate.HasValue;
        }


        //summary
        //When due date and report date are present, due date shout be at a later date than report date
        public bool ValidateDueDateAfterReportDate()
        {
            if(!ReportDate.HasValue) return true;
            if (!DueDate.HasValue) return true;
            if (!DueDate.HasValue && !ReportDate.HasValue) return true; //if both due date and report date doesnot have value, return true

            return !(DueDate.Value.Date >= ReportDate.Value.Date); //.Date gets only the date part and ignores the seconds part
        }

        //summary
        //When due date or report date is absent but owner is present
        public bool ValidateOwnerPresence()
        {
            if(string.IsNullOrWhiteSpace(Owner)) return true;
            if (string.IsNullOrEmpty(Owner) && DueDate.HasValue) return true; // if owner is not entered, but due date is entered
            if (string.IsNullOrEmpty(Owner) && ReportDate.HasValue) return true; // if owner is not entered, but report date is entered

            return string.IsNullOrWhiteSpace(Owner);
        }
    }
}
