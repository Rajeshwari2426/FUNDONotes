using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FunDoNotes.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [HttpPost("Add")]
        public IActionResult AddLabel(Label label)
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = labelBL.AddLabel(label, userID);
            if (result != null)
                return Ok(new { success = true, message = "Label added successfully", data = result });
            else
                return BadRequest(new { success = false, message = " Unsuccessful" });
        }
        [HttpDelete("Remove")]
        public IActionResult DeleteLabel(long labelId, long noteId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = labelBL.RemoveLabel(labelId, noteId, userId);
            if (result != null)
                return Ok(new { success = true, message = "label Removed successfully", data = result });
            else
                return BadRequest(new { success = false, message = " Unsuccessful" });
        }
        [HttpPatch("Edit")]
        public IActionResult EditLabel(string newName, long labelId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = labelBL.EditLabel(newName, labelId, userId);
            if (result != null)
                return Ok(new { success = true, message = " successful", data = result });
            else
                return BadRequest(new { success = false, message = " Unsuccessful" });
        }
        [HttpGet("GetLabels")]
        public IActionResult GetLabels()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = labelBL.GetAll();
            if (result != null)
                return Ok(new { success = true, message = " successful", data = result });
            else
                return BadRequest(new { success = false, message = " Unsuccessful" });
        }
    }
}
