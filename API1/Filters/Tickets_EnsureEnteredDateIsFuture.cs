using API1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace API1.Filters
{
    public class Tickets_EnsureEnteredDateIsFuture : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var ticket = context.ActionArguments["ticket"] as Ticket;

            if(ticket != null && ticket.EnteredDate < DateTime.Today)
            {
                context.ModelState.AddModelError("EnteredDate", "You can only create tickets today or in the future");
                context.Result = new BadRequestObjectResult(context.ModelState);// return a bad request when condition is true (short circuiting)

            }
        }
    }
}
