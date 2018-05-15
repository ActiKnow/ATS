using ATS.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ATS.Web.Controllers
{
    public class CommonTaskController : Controller
    {
        // GET: CommonTask
        public ActionResult AppConstant()
        {
            string json = null;
            try
            {
                var constants = typeof(Constants)
                    .GetFields()
                    .ToDictionary(x => x.Name, x => x.GetValue(null));
                json = new JavaScriptSerializer().Serialize(constants);

            }
            catch (Exception ex) { }
            return JavaScript("var AppConstant = " + json + ";");

        }
    }
}