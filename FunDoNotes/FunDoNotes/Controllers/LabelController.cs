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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllLabelsUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var labelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                labelList = labelBL.GetAll();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }
    }
}
