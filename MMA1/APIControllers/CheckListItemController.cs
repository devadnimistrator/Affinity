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
    [RoutePrefix("api/CheckListItem")]
    public class CheckListItemController : ApiController
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


        // GET api/CheckListItem
        [Route(@"byList/{strCheckListId}")]
        [HttpGet]
        public IHttpActionResult GetByList([FromUri]string strCheckListId)
        {
            try
            {
                Guid g = new Guid(strCheckListId);
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                    {
                        var l = m.CheckListItems.GetByList(g);
                        List<VMCheckListItem> al = new List<VMCheckListItem>(l.Count());
                        foreach (var r in l)
                        {
                            al.Add(VMCheckListItem.GetVmCheckListItem(r));
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

        // GET api/CheckListItem/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                Guid g = new Guid(id);
                //UserId = "2303eff9-b95e-4d3c-8397-bf4eaae25c4f";
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    VMCheckListItem vm = VMCheckListItem.GetVmCheckListItem(m.CheckListItems.Get(g));
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

        // POST api/CheckListItem
        public IHttpActionResult Post([FromBody]VMCheckListItem value)
        {
            //UserId = "2303eff9-b95e-4d3c-8397-bf4eaae25c4f";
            try
            {
                CheckListItem c = VMCheckListItem.GetCheckListItem(value);
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    //m.CheckListItems.Add(c);
                    //m.Complete();
                    m.CheckListItems.Insert(c);
                }
                return Created($"/api/CheckListItem/{c.Id}", VMCheckListItem.GetVmCheckListItem(c));
            }
            catch (Exception e)
            {
                return BadRequest(e.GetAllMessages());
                //throw;
            }
        }

        // PUT api/CheckListItem/5
        public IHttpActionResult Put(string id, [FromBody]VMCheckListItem value)
        {
            var g = new Guid(id);
            try
            {
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    //VMCheckListItem.UpdateCheckListItem(value, c);
                    //m.Complete();
                    m.CheckListItems.Update(g, VMCheckListItem.GetCheckListItem(value));
                    CheckListItem c = m.CheckListItems.Get(g);
                    VMCheckListItem v = VMCheckListItem.GetVmCheckListItem(c);
                    return Ok(v);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.GetAllMessages());
            }
        }

        // DELETE api/CheckListItem/5
        public IHttpActionResult Delete(string id)
        {
            var g = new Guid(id);
            try
            {
                using (UnitOfWork m = new UnitOfWork(new MMAContext(), new Guid(UserId), UserEmail))
                {
                    //CheckListItem c = m.CheckListItems.Get(g);
                    //t.Deleted = true;
                    //m.CheckListItems.Remove(c);
                    //m.Complete();
                    m.CheckListItems.Delete(g);
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