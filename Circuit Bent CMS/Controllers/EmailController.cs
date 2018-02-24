using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;

namespace CircuitBentCMS.Controllers
{
    public class EmailController : Controller
    {
        [HttpPost]
        public ActionResult SendMail()
        {
            // make sure that some time have elapsed between page hit and form submit
            DateTime pageLoaded = Convert.ToDateTime(Request.Form["timestamp"]);
            TimeSpan compareDates = DateTime.Now - pageLoaded;
            
            int seconds = compareDates.Seconds;

            string messageStatus = "You have to fill in all fields";
            string email = Request.Form["email"];
            string subject = Request.Form["subject"];
            string message = Request.Form["message"];

            // check if it is a spam bot
            if (String.IsNullOrEmpty(Request.Form["cheater"]) && seconds > 10)
            {
                // only send the message if all the form fields are entered
                if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(subject) && !String.IsNullOrEmpty(message))
                {
                    // send the mail and return an eventual error message
                    messageStatus = MessageService.SendMail(email, subject, message);
                }
                
                // if messageStatus is empty, the request went without errors
                if (String.IsNullOrEmpty(messageStatus))
                {
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false, errorMessage = messageStatus });        
        }
    }
}
