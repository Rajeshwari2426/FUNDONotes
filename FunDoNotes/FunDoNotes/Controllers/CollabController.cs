using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
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
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache; 
            this.distributedCache = distributedCache;
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
        [HttpGet("GetAllCollabs")]
        public IActionResult GetCollabs(long noteID)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = collabBL.GetAll(noteID);
            if (result != null)
                return Ok(new { success = true, message = " successful", data = result });
            else
                return BadRequest(new { success = false, message = " Unsuccessful" });
        }       
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllCollabsUsingRedisCache()
        {
            var cacheKey = "collabList";
            string serializedCollabList;
            var collabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                collabList = collabBL.GetAllNotes();
                serializedCollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }

    }
}
