using System;
using System.ComponentModel.DataAnnotations;

namespace API1.Models.ModelValidations
{
    public class Tickets_EnsureDueDateIsFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

           var ticket = validationContext.ObjectInstance as Ticket;

            if(ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                if(ticket.DueDate.HasValue && ticket.DueDate < DateTime.Now)
                {
                    return new ValidationResult("Dates are to be in the future");
                }
            }
            return ValidationResult.Success;
        }
    }
}
