using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using src.Data;
using src.Models;
using src.Services;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Members")]
    //[Authorize]
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public MembersController(ApplicationDbContext context,
            IDotnetdesk dotnetdesk,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _dotnetdesk = dotnetdesk;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        //GET: api/Dashboard/GetMembers
        [HttpGet("{organizationId}")]
        public IActionResult GetMembers([FromRoute]Guid organizationId)
        {
            var members = _context.Members.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = members });
        }

        //POST: api/Dashboard/PostExtend
        [HttpPost("PostExtend")]
        public async Task<IActionResult> PostExtend(int id)
        {
            var extend = _context.Members.Where(x => x.Id == id).FirstOrDefault();
            //if (extend.Status == "Active")
            //{
            //    extend.ExpireDate = extend.ExpireDate.AddMonths(1);

            //}
            //else
            //{
            //    extend.ExpireDate = DateTime.Now.AddMonths(1);
            //}
            _context.Members.Update(extend);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Successfully Saved!" });
        }
        //POST: api/Dashboard/PostMembers
        [HttpPost]
        public async Task<IActionResult> PostMembers([FromBody] JObject model)
        {
            int id = 0;
            var info = await _userManager.GetUserAsync(User);
            id = Convert.ToInt32(model["Id"].ToString());
            var daily = _context.DailyCollection.OrderByDescending(x => x.Date).FirstOrDefault();
            var monthly = _context.MonthlyCollection.OrderByDescending(x => x.Date).FirstOrDefault();

            if (id == 0)
            {
            Members members = new Members
            {
                FullName = model["FullName"].ToString(),
                Address = model["Address"].ToString(),
                Contact = model["Contact"].ToString(),
                Entitlement = model["Entitlement"].ToString(),
                Coverage = model["Coverage"].ToString(),
                PersonnelId = model["PersonnelId"].ToString(),
                Remarks = model["Remarks"].ToString()
            };
                _context.Members.Add(members);
            }

            //Members oldPersonnels = _context.Members.Where(x => x.PersonnelId == model["PersonnelId"].ToString()).FirstOrDefault();
            //if (oldPersonnels != null)
            //{
            //    return Json(new { success = false, message = "Personnel Id is already taken!" });
            //}

            //else
            //{
            //    members.FullName = model["FullName"].ToString();
            //    members.Address = model["Address"].ToString();
            //    members.Contact = model["Contact"].ToString();
            //    members.Entitlement = model["Entitlement"].ToString();
            //    members.Coverage = model["Coverage"].ToString();
            //    members.PersonnelId = model["PersonnelId"].ToString();
            //    members.Remarks = model["Remarks"].ToString();
            //    members.Id = id;
            //    _context.Members.Update(members);
            //}

            Members currentPersonnel = _context.Members.Where(x => x.Id == id).FirstOrDefault();
            if (currentPersonnel != null)
            {
            if (currentPersonnel.FullName != model["FullName"].ToString() || currentPersonnel.Contact != model["Contact"].ToString() || currentPersonnel.Entitlement != model["Entitlement"].ToString() || currentPersonnel.Coverage != model["Coverage"].ToString() || currentPersonnel.Address != model["Address"].ToString() || currentPersonnel.Remarks != model["Remarks"].ToString() || currentPersonnel.PersonnelId != model["PersonnelId"].ToString())
            {
                var one = "";
                var two = "";
                var three = "";
                var four = "";
                var five = "";
                var six = "";
                var seven = "";
                EditedDatas editedDatas = new EditedDatas
                {
                    DateEdited = DateTime.Now,
                    Origin = "Personnel",
                    EditedBy = info.FullName,
                    ControlNumber = currentPersonnel.Id
                };
                if (currentPersonnel.PersonnelId != model["PersonnelId"].ToString())
                {
                    one = "Personnel Id = " + currentPersonnel.PersonnelId + " - " + model["PersonnelId"].ToString() + "; ";
                }
                if (currentPersonnel.FullName != model["FullName"].ToString())
                {
                    two = " Full Name = " + currentPersonnel.FullName + " - " + model["FullName"].ToString() + "; ";
                }
                if (currentPersonnel.Address != model["Address"].ToString())
                {
                    three = " Address = " + currentPersonnel.Address + " - " + model["Address"].ToString() + "; ";
                }
                if (currentPersonnel.Contact != model["Contact"].ToString())
                {
                    four = " Contact = " + currentPersonnel.Contact + " - " + model["Contact"].ToString() + "; ";
                }
                if (currentPersonnel.Entitlement != model["Entitlement"].ToString())
                {
                    five = " Entitlement = " + currentPersonnel.Entitlement + " - " + model["Entitlement"].ToString() + "; ";
                }
                if (currentPersonnel.Coverage != model["Coverage"].ToString())
                {
                    six = " Coverage = " + currentPersonnel.Coverage + " - " + model["Coverage"].ToString() + "; ";
                }
                if (currentPersonnel.Remarks != model["Remarks"].ToString())
                {
                    seven = " Remarks = " + currentPersonnel.Remarks + " - " + model["Remarks"].ToString() + "; ";
                }
                var datas = one + two + three + four + five + six + seven;
                editedDatas.EditedData = datas;
                _context.EditedDatas.Add(editedDatas);
            }
                //Members currentMembers = _context.Members.Where(x => x.Id == id).FirstOrDefault();
                currentPersonnel.FullName = model["FullName"].ToString();
                currentPersonnel.Address = model["Address"].ToString();
                currentPersonnel.Contact = model["Contact"].ToString();
                currentPersonnel.Entitlement = model["Entitlement"].ToString();
                currentPersonnel.Coverage = model["Coverage"].ToString();
                currentPersonnel.PersonnelId = model["PersonnelId"].ToString();
                currentPersonnel.Remarks = model["Remarks"].ToString();
                //members.Id = Convert.ToInt32(model["Id"].ToString());
                _context.Members.Update(currentPersonnel);
            }

            //var Daily = new DailyCollection
            //{
            //    Origin = "Membership of " + currentPersonnel.FullName,
            //    Date = DateTime.Now,
            //    Amount = currentPersonnel.AmountPaid,
            //    Remarks = currentPersonnel.Remarks
            //};
            //if (daily == null)
            //{
            //    Daily.Total = Daily.Amount;
            //    _context.DailyCollection.Add(Daily);
            //}
            //else if (Daily.Date.Day == daily.Date.Day)
            //{
            //    Daily.Total = daily.Total + Daily.Amount;
            //}
            //else
            //{
            //    Daily.Total = Daily.Amount;
            //}
            //_context.DailyCollection.Add(Daily);

            //var Monthly = new MonthlyCollection
            //{
            //    Origin = "Membership of " + currentPersonnel.FullName,
            //    Date = DateTime.Now,
            //    Amount = currentPersonnel.AmountPaid,
            //    Remarks = currentPersonnel.Remarks
            //};
            //if (monthly == null)
            //{
            //    Monthly.Total = Monthly.Amount;
            //    _context.MonthlyCollection.Add(Monthly);
            //}
            //else if (Monthly.Date.Month == monthly.Date.Month)
            //{
            //    Monthly.Total = monthly.Total + Monthly.Amount;
            //}
            //else
            //{
            //    Monthly.Total = Monthly.Amount;
            //}
            //_context.MonthlyCollection.Add(Monthly);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Successfully Saved!" });
        }

       
        //DELETE: api/Security/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembers([FromRoute] int id)
        {
            Members members = _context.Members.Where(mem => mem.Id == id).FirstOrDefault();
            _context.Remove(members);

            var info = await _userManager.GetUserAsync(User);
            DeletedDatas deleted = new DeletedDatas
            {
                DateDeleted = DateTime.Now,
                //PlateNumber = repair.PlateNumber,
                Origin = "Personnel",
                FullName = members.FullName,
                DeletedBy = info.FullName,
                Remarks = members.Remarks
            };

            _context.DeletedDatas.Add(deleted);

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete success." });
        }

    }
}