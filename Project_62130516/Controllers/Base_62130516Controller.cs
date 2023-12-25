using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_62130516.Controllers
{
    public class Base_62130516Controller : Controller
    {
        protected string _CurrentUserId
        {
            get { return Session["UserId"] as string; }
            set { Session["UserId"] = value; }
        }
        protected string _Role
        {
            get { return Session["Role"] as string; }
            set { Session["Role"] = value; }
        }
    }
}