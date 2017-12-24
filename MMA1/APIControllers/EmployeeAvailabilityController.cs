using MMA_VM.Models;
using MMA1.Models;
using MMA1Model;
using MMA1Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MMA_Common;

namespace MMA1.APIControllers
{
    [Authorize]
    [RoutePrefix("api/EmployeeAvailability")]
    public class EmployeeAvailabilityController : ApiController
    {
        private string _userId = null;
        private string _userEmail = null;
        public string UserId
        {
            get
            {
                if (_userId == null) _userId = User.Identity.GetUserId();
                return _userId;
            }
            set { _userId = value; }
        }

        public string UserEmail
        {
            get
            {
                if (_userEmail == null)
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        ApplicationUser u = db.Users.Find(UserId);
                        _userEmail = u.Email;
                    }
                }
                return _userEmail;
            }
            set { _userEmail = value; }
        }
        // GET: api/EmployeeAvailability

        [Route(@"byEmployee/{employeeEmail}/{strStartDate}")]
        [HttpGet]
        public IHttpActionResult GetByEmployee(string employeeEmail, string strStartDate)
        {
            try
            {
                DateTime startD = CommonUtils.DateTimeStringToDatetime(strStartDate);

                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    var l = m.EmployeeAvailabilityRecs.GetByEmployee(employeeEmail, startD);
                    List<VMEmployeeAvailability> al = new List<VMEmployeeAvailability>(l.Count());
                    foreach (var t in l)
                    {
                        al.Add(VMEmployeeAvailability.GetVmEAvailability(t));
                    }
                    return Ok(al);
                }

            }
            catch (Exception e)
            {
                //ToDo Log exception
                return BadRequest(e.GetAllMessages());
            }

        }

        // GET: api/EmployeeAvailability/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                Guid g = new Guid(id);
                //UserId = "2303eff9-b95e-4d3c-8397-bf4eaae25c4f";
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    VMEmployeeAvailability vm = VMEmployeeAvailability.GetVmEAvailability(m.EmployeeAvailabilityRecs.Get(g));
                    return Ok(vm);
                }
            }
            catch (Exception ex)
            {
                //todo Log Exception 
                return BadRequest(ex.GetAllMessages());
            }
        }


    }
}
