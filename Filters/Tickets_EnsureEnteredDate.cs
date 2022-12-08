using API1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API1.Filters
{
    public class Tickets_EnsureEnteredDate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
           

            var ticket = context.ActionArguments["ticket"] as Ticket;

            
            
            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner) && ticket.EnteredDate.HasValue == false)//check if owner entered ticket creating date
            {
                context.ModelState.AddModelError("EnteredDate", "Enter the date you want ticket to be created");
                context.Result = new BadRequestObjectResult(context.ModelState);// return a bad request when condition is true (short circuiting)
            
            }
        }
    }
}
