using API1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.Serialization;

namespace API1.Filters
{
    public class Tickets_EnteredDateBeforeDueDate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            base.OnActionExecuting(context);

            var ticket = context.ActionArguments["ticket"] as Ticket;

            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                if (ticket.EnteredDate.HasValue && ticket.DueDate.HasValue)//check if entered date and due date is entered
                {
                    if(ticket.DueDate < ticket.EnteredDate)
                    {
                        context.ModelState.AddModelError("EnteredDate", "You cant create tickets for previous days");
                        context.Result = new BadRequestObjectResult(context.ModelState);// return a bad request when condition is true (short circuiting)

                    }
                }

            }
        }
    }
}
