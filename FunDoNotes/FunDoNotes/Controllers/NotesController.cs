using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        
            private readonly INotesBL notesBL;
        //private readonly IMemoryCache memoryCache;
        //private readonly IDistributedCache distributedCache;
            
            public NotesController(INotesBL notesBL)//, IMemoryCache memoryCache, IDistributedCache distributedCache)
            {
                this.notesBL = notesBL;
               //this.memoryCache = memoryCache;
               // this.distributedCache = distributedCache;
               
            }
            [HttpPost("Create")]
            public IActionResult CreateNote(Notes createNotes)
            {
               
                    long userID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                    var res = notesBL.CreateNote(createNotes, userID);
                    if (res != null)
                    {
                        
                        return Ok(new { success = true, message = "Notes Created successfully", data = res });
                    }
                    else
                    {
                        
                        return BadRequest(new { success = false, message = "Failed to Create Note" });
                    }
               
            }

           
           
            [HttpGet("GetAll")]
            public IActionResult RetriveNotes()
            {
               
                    long userID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                    var res = notesBL.RetriveNotes(userID);
                    if (res != null)
                    {
                       
                        return Ok(new { success = true, message = "All Notes Displayed successfully", data = res });
                    }
                    else
                    {
                        
                        return BadRequest(new { success = false, message = "Failed to Display Notes" });
                    }
                
                
            }

        [HttpPut("Update")]
        public IActionResult UpdateNote(Notes updateNotes, long noteId)
        {

            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var resNote = notesBL.UpdateNote(updateNotes, noteId, userId);
            if (resNote != null)
            {

                return Ok(new { success = true, message = "Note Updated Successfully", data = resNote });
            }
            else
            {

                return BadRequest(new { success = false, message = "Failed to Update Note" });
            }
        }
            [HttpDelete("Delete")]
            public IActionResult DeleteNote(long noteId)
            {
               
                    long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                    var resNote = notesBL.DeleteNote(noteId, userId);
                    if (resNote.Contains("Success"))
                    {
                        
                        return Ok(new { success = true, message = resNote });
                    }
                    else
                    {
                        
                        return BadRequest(new { success = false, message = resNote });
                    }
                
            }

           
            [HttpPatch("IsArchive")]
            public IActionResult IsArchieveOrNot(long noteId)
            {
                
                    long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                    var resNote = notesBL.IsArchieveOrNot(noteId, userId);
                    if (resNote != null)
                    {
                       
                        return Ok(new { Success = true, message = "Archive Status Changed Successfully", data = resNote });
                    }
                    else
                    {
                       
                        return BadRequest(new { Success = false, message = "Failed to change Archive Status" });
                    }
                
            }

            [HttpPatch("IsPinned")]
            public IActionResult IsPinnedOrNot(long noteId)
            {
                
                    long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                    var resNote = notesBL.IsPinnedOrNot(noteId, userId);
                    if (resNote != null)
                    {
                       
                        return Ok(new { Success = true, message = "Pin Status Changed Successfully", data = resNote });
                    }
                    else
                    {
                       
                        return BadRequest(new { Success = false, message = "Failed to change Pin Status" });
                    }
                
               
            }

            [HttpPatch("IsTrash")]
            public IActionResult IsTrashOrNot(long noteId)
            {
               
                    long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                    var resNote = notesBL.IsTrashOrNot(noteId, userId);
                    if (resNote != null)
                    {
                       
                        return Ok(new { Success = true, message = "Trash Status Changed Successfully", data = resNote });
                    }
                    else
                    {
                       
                        return BadRequest(new { Success = false, message = "Failed to change Trash Status" });
                    }
               
            }

           
            [HttpPatch("UploadImage")]
            public IActionResult UploadImage(long noteId, IFormFile imagePath)
            {
                try
                {
                    long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                    var resNote = notesBL.UploadImage(noteId, userId, imagePath);
                    if (resNote != null)
                    {
                       
                        return Ok(new { Success = true, message = "Image Uploaded Successfully", data = resNote });
                    }
                    else
                    {
                       
                        return BadRequest(new { Success = false, message = "Failed to Upload Image" });
                    }
                }
                catch (Exception ex)
                {
                   
                    return NotFound(new { success = false, message = ex.Message });
                }
            }

            
           
            //[HttpGet("Redis")]
            //public async Task<IActionResult> GetAllNotesUsingRedisCache()
            //{
            //    var cacheKey = "notesList";
            //    string serializedNotesList;
            //    var notesList = new List<NotesEntity>();
            //    var redisNotesList = await distributedCache.GetAsync(cacheKey);
            //    if (redisNotesList != null)
            //    {
            //        serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
            //        notesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            //    }
            //    else
            //    {
            //       long userID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            //      notesList = (List<NotesEntity>)notesBL.RetriveNotes(userID);
            //       serializedNotesList = JsonConvert.SerializeObject(notesList);
            //        redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
            //        var options = new DistributedCacheEntryOptions()
            //            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            //        await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            //    }
            //    return Ok(notesList);
            //}

        
    }
}
