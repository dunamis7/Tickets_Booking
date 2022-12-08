using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace API1.Filters
{
    public class Version1DiscontinueResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }
        //We are retiring version 1 of our app using resource filter
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Path.Value.ToLower().Contains("v2")) // checks if the http request does not contain v2
            {
                context.Result = new BadRequestObjectResult( //if it does not contain version 2, do short circuiting and return results
                    new
                    {
                        Versioning = new[] {"This version of the API has expired. Please use the new version"}
                    }
                    );
            }
        }
    }
}
