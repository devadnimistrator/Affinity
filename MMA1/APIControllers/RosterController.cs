using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MMA1.Models;
using MMA1Model;
using MMA1Model.Models;
using MMA_Common;
using MMA_VM.Models;
using CommonUtils = MMA1.App_Code.CommonUtils;
using ExceptionExtention = MMA1.App_Code.ExceptionExtention;

namespace MMA1.APIControllers
{
   [Authorize]
   [RoutePrefix("api/Roster")]
    public class RosterController : ApiController
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


        // GET api/Roster
        [Route(@"byEmployer/{strEmployerId}/{strStartDate}/{strEndDate}")]
        [HttpGet]
        public IHttpActionResult GetByEmployer([FromUri]string strEmployerId, [FromUri]string strStartDate, [FromUri]string strEndDate )
        {
            try
            {
                Guid employerId = new Guid(strEmployerId);
                DateTime startD = CommonUtils.DateTimeStringToDatetime(strStartDate);
                DateTime endD = CommonUtils.DateTimeStringToDatetime(strEndDate);
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                    {
                        var l = m.Rosters.GetByEmployer(employerId, startD, endD);
                        List<VMRoster> al = new List<VMRoster>(l.Count());
                        foreach (var r in l)
                        {
                            al.Add(VMRoster.GetVmRoster(r));
                        }
                        return Ok(al);
                    }                   
                }
            }
            catch (Exception e)
            {
                //ToDo Log exception
                return BadRequest(ExceptionExtention.GetAllMessages(e));
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                Guid g = new Guid(id);
                //UserId = "2303eff9-b95e-4d3c-8397-bf4eaae25c4f";
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    VMRoster vm = VMRoster.GetVmRoster(m.Rosters.Get(g));
                    if (vm ==null) 
                        return NotFound();
                    else return Ok(vm);
                }
            }
            catch (Exception ex)
            {
                //todo Log Exception 
                return BadRequest(ExceptionExtention.GetAllMessages(ex));
            }
            
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]VMRoster value)
        {
            //UserId = "2303eff9-b95e-4d3c-8397-bf4eaae25c4f";
            try
            {
                Roster r = VMRoster.GetRoster(value, UserId);
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    m.Rosters.Add(r);
                    m.Complete();
                }
                return Created($"/api/Roster/{r.Id}", r);
            }
            catch (Exception e)
            {
                return BadRequest(ExceptionExtention.GetAllMessages(e));
                //throw;
            }           
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(string id, [FromBody]VMRoster value)
        {
            var g = new Guid(id);
            try
            {
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    Roster ro = m.Rosters.Get(g);
                    ro.Acknowledged = AcknowledgementType.Unknown;
                    ro.AcknowledgeComment = string.Empty;
                    VMRoster.UpdateRoster(value, ro);
                    m.Complete();
                    VMRoster v = VMRoster.GetVmRoster(ro);
                    return Ok(v);
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(ExceptionExtention.GetAllMessages(e));
            }
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(string id)
        {
            var g = new Guid(id);
            try
            {
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    Roster rr = m.Rosters.Get(g);
                    rr.Deleted = true;
                    //m.Rosters.Remove(rr);
                    m.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ExceptionExtention.GetAllMessages(e));
            }
        }
        // GET api/Roster
        [Route(@"Acknowledged/{strEmployerId}/{strStartDate}/{strEndDate}")]
        [HttpGet]
        public IHttpActionResult GetAcknowledged([FromUri]string strEmployerId, [FromUri]string strStartDate, [FromUri]string strEndDate)
        {
            try
            {
                Guid employerId = new Guid(strEmployerId);
                DateTime startD = CommonUtils.DateTimeStringToDatetime(strStartDate);
                DateTime endD = CommonUtils.DateTimeStringToDatetime(strEndDate);
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                    {
                        var l = m.Rosters.GetAckdByEmployer(employerId, startD, endD);
                        List<VMRoster> al = new List<VMRoster>(l.Count());
                        foreach (var r in l)
                        {
                            al.Add(VMRoster.GetVmRoster(r));
                        }
                        return Ok(al);
                    }
                }
            }
            catch (Exception e)
            {
                //ToDo Log exception
                return BadRequest(ExceptionExtention.GetAllMessages(e));
            }
        }
    }
}