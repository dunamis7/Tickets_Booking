using System.ComponentModel.DataAnnotations;

namespace API1.Models.ModelValidations
{
    public class Tickets_EnsureDueDateForTicketOwner : ValidationAttribute //inherit this class to write custom annotations
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket =  validationContext.ObjectInstance as Ticket; // create an object of type ticket
            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner)) //if ticket is not null and ticket.owner is not null or white space (ie the ticket has an owner)
            {
                if (!ticket.DueDate.HasValue)  //ticket does not have a duedate
                {
                    return new ValidationResult("Due date is required when an owner is provided");
                } 

            }

            if (string.IsNullOrWhiteSpace(ticket.Owner) && ticket.DueDate.HasValue==true)  //ticket does not have a duedate
            {
                return new ValidationResult("Owner is required when creating a ticket");
            }

            return ValidationResult.Success;

        }
    }
}
