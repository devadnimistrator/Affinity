using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MMA1.App_Code;
using MMA1Model;
using MMA1Model.Models;
using MMA_VM.Models;
using MMA1.Models;

namespace MMA1.APIControllers
{
    [Authorize]
    [RoutePrefix("api/ClockData")]
    public class ClockDataController : ApiController
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

        // GET api/ClockData/ByEmployer/{}
        [Route(@"byEmployer/{strEmployerId}/{strStartDate}/{strEndDate}")]
        public IHttpActionResult GetByEmployer([FromUri]string strEmployerId, [FromUri]string strStartDate, [FromUri]string strEndDate)
        {
            try
            {
                Guid employerId = new Guid(strEmployerId);
                DateTime startD = CommonUtils.DateTimeStringToDatetime(strStartDate);
                DateTime endD = CommonUtils.DateTimeStringToDatetime(strEndDate);
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    
                    var L = m.ClockData.GetByEmployer(employerId, startD, endD);
                    List<VMClockData> vl = new List<VMClockData>();

                    foreach (ClockData c in L)
                    {
                        vl.Add(VMClockData.GetVmClockData(c));
                    }

                    return Ok(vl);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.GetAllMessages());
            }
        }

        [Route(@"byRoster/{strRosterId}")]
        public IHttpActionResult GetByRoster([FromUri]string strRosterId)
        {
            try
            {
                Guid rosterId = new Guid(strRosterId);

                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    
                    var L = m.ClockData.GetByRoster(rosterId);
                    List<VMClockData> vl = new List<VMClockData>();

                    foreach (ClockData c in L)
                    {
                        vl.Add(VMClockData.GetVmClockData(c));
                    }

                    return Ok(vl);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.GetAllMessages());
            }
        }




    }
}