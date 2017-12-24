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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MMA1.APIControllers
{
    [Authorize]
    [RoutePrefix("api/CheckList")]
    public class CheckListController : ApiController
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


        // GET api/CheckList
        [Route(@"byEmployer/{strEmployerId}/{strStartDate}/{strEndDate}")]
        [HttpGet]
        public IHttpActionResult GetByEmployer([FromUri]string strEmployerId, [FromUri]string strStartDate, [FromUri]string strEndDate)
        {
            try
            {
                Guid employerId = new Guid(strEmployerId);
                DateTime startD = CommonUtils.DateTimeStringToDatetime(strStartDate);
                DateTime endD = CommonUtils.DateTimeStringToDatetime(strEndDate);
                    using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                    {
                        var l = m.CheckLists.GetByEmployer(employerId, startD, endD);
                        List<VMCheckList> al = new List<VMCheckList>(l.Count());
                        foreach (var r in l)
                        {
                            al.Add(VMCheckList.GetVmCheckList(r));
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

        // GET api/CheckList/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                Guid g = new Guid(id);
                //UserId = "2303eff9-b95e-4d3c-8397-bf4eaae25c4f";
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    VMCheckList vm = VMCheckList.GetVmCheckList(m.CheckLists.Get(g));
                    if (vm == null)
                        return NotFound();
                    return Ok(vm);
                }
            }
            catch (Exception ex)
            {
                //todo Log Exception 
                return BadRequest(ex.GetAllMessages());
            }

        }

        // POST api/CheckList
        [HttpPost]
        public IHttpActionResult Post([FromBody]VMCheckList value)
        {
            //UserId = "2303eff9-b95e-4d3c-8397-bf4eaae25c4f";
            bool icheck;
            try
            {
                CheckList c = VMCheckList.GetCheckList(value, new Guid(UserId));
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    icheck = m.CheckLists.Insert(c);
                }
                if (icheck)
                    return Created($"/api/CheckList/{c.Id}", VMCheckList.GetVmCheckList(c));
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.GetAllMessages());
                //throw;
            }
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]VMCheckList value)
        {
            if (value != null)
            {
                CheckList c = VMCheckList.GetCheckList(value, new Guid(UserId));
                var g = c.Id;
                try
                {
                    using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                    {
                        //CheckList savedCheckList = m.CheckLists.Get(g);

                        m.CheckLists.Update(g, c);
                        
                        CheckList cc = m.CheckLists.Get(g);
                        VMCheckList v = VMCheckList.GetVmCheckList(cc);
                        return Ok(v);
                    }

                }
                catch (Exception e)
                {
                    return BadRequest(e.GetAllMessages());
                }
            }
            else
                return BadRequest();
        }

        // DELETE api/CheckList/5
        public IHttpActionResult Delete(string id)
        {
            var g = new Guid(id);
            try
            {
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    m.CheckLists.Delete(g);
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