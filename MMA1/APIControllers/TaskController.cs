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
using MMA1.App_Code;
using MMA1.Models;
using MMA1Model;
using MMA1Model.Models;
using MMA_VM.Models;

namespace MMA1.APIControllers
{
    [Authorize]
    [RoutePrefix("api/Task")]
    public class TaskController : ApiController
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


        // GET api/Task
        [Route(@"byEmployer/{strEmployerId}/{strStartDate}/{strEndDate}")]
        [HttpGet]
        public IHttpActionResult GetByEmployer([FromUri]string strEmployerId, [FromUri]string strStartDate, [FromUri]string strEndDate)
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
                        var l = m.Tasks.GetByEmployer(employerId, startD, endD);
                        List<VMTask> al = new List<VMTask>(l.Count());
                        foreach (var r in l)
                        {
                            al.Add(VMTask.GetVmTask(r));
                        }
                        return Ok(al);
                    }
                }
            }
            catch (Exception e)
            {
                //ToDo Log exception
                return BadRequest(e.GetAllMessages());
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
                    VMTask vm = VMTask.GetVmTask(m.Tasks.Get(g));
                    if (vm == null)
                        return NotFound();
                    else return Ok(vm);
                }
            }
            catch (Exception ex)
            {
                //todo Log Exception 
                return BadRequest(ex.GetAllMessages());
            }

        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]VMTask value)
        {
            //UserId = "2303eff9-b95e-4d3c-8397-bf4eaae25c4f";
            try
            {
                Task t = VMTask.GetTask(value, UserId);
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    t.UpdatedBy = new Guid(UserId);
                    m.Tasks.Add(t);
                    m.Complete();
                }
                return Created($"/api/Task/{t.Id}", t);
            }
            catch (Exception e)
            {
                return BadRequest(e.GetAllMessages());
                //throw;
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(string id, [FromBody]VMTask value)
        {
            var g = new Guid(id);
            try
            {
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    Task t = m.Tasks.Get(g);
                    VMTask.UpdateTask(value, t);
                    t.UpdatedBy = new Guid(UserId);
                    m.Complete();
                    VMTask v = VMTask.GetVmTask(t);
                    return Ok(v);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.GetAllMessages());
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
                    Task t = m.Tasks.Get(g);
                    t.Deleted = true;
                    //m.Tasks.Remove(t);
                    m.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.GetAllMessages());
            }
        }
    }
}