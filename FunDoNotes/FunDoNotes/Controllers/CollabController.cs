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
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [HttpPost("Add")]
        public IActionResult AddCollab(Collaborators collaborator, long noteID)
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = collabBL.AddCollaborator(collaborator, noteID, userID);
            if (result != null)
                return Ok(new { success = true, message = "Collaborator added successfully", data = result });
            else
                return BadRequest(new { success = false, message = " Unsuccessful" });
        }
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long collabID, long noteID)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = collabBL.RemoveCollaborator(collabID, noteID);
            if (result != null)
                return Ok(new { success = true, message = "Collaborator Removed successfully", data = result });
            else
                return BadRequest(new { success = false, message = " Unsuccessful" });
        }
        [HttpGet("GetAll")]
        public IActionResult GetCollabs(long noteID)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = collabBL.GetAll(noteID);
            if (result != null)
                return Ok(new { success = true, message = " successful", data = result });
            else
                return BadRequest(new { success = false, message = " Unsuccessful" });
        }

    }
}
