using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagManage.API.Infrastructure;
using TagManage.API.Requests;
using TagManage.Data.Entities;
using TagManage.Domain.Command;
using TagManage.Domain.Query;

namespace TagManage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController(IMapper mapper, TagCommand tagCommand,
            TagQuery tagQuery, PercentageTagQuery percentagTagQuery) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly TagCommand _tagCommand = tagCommand;
        private readonly TagQuery _tagQuery = tagQuery;
        private readonly PercentageTagQuery _percentageTagQuery = percentagTagQuery;

        [Authorize]
        [HttpGet("GetTags")]
        public async Task<IActionResult> GetTags()
        {
            try 
            {
                return Ok(await _tagQuery.GetQuery());
            }
            catch(Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpGet("GetCountSortedTags")]
        public async Task<IActionResult> GetCountSortedTags([FromQuery]int page = 1, [FromQuery]int pagesize = 10, [FromQuery] SortDirection sortDirection = SortDirection.Descending)
        {
            try 
            {
                return Ok(await _tagQuery.GetCountSortedQuery(page, pagesize, sortDirection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetPercentageCountSortedTags")]
        public async Task<IActionResult> GetPercentageCountSortedTags([FromQuery] int page = 1, [FromQuery] int pagesize = 10, [FromQuery] SortDirection sortDirection = SortDirection.Descending)
        {
            try 
            {
                return Ok(await _percentageTagQuery.GetCountSortedQuery(page, pagesize, sortDirection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetNameSortedTags")]
        public async Task<IActionResult> GetNameSortedTags([FromQuery] int page = 1, [FromQuery] int pagesize = 10, [FromQuery] SortDirection sortDirection = SortDirection.Descending)
        {
            try
            {
                return Ok(await _tagQuery.GetNameSortedQuery(page, pagesize, sortDirection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetPercentageNameSortedTags")]
        public async Task<IActionResult> GetPercentageNameSortedTags([FromQuery] int page = 1, [FromQuery] int pagesize = 10, [FromQuery] SortDirection sortDirection = SortDirection.Descending)
        {
            try
            {
                return Ok(await _percentageTagQuery.GetNameSortedQuery(page, pagesize, sortDirection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPost("PostTag")]
        //public async Task<IActionResult> PostTag(CreateTagRequest createTagRequest)
        //{
        //    await _tagCommand.CreateTag(_mapper.Map<TagEntity>(createTagRequest));
        //    return Ok();
        //}

        [Authorize]
        [HttpPut("PutTags")]
        public async Task<IActionResult> PutTags()
        {

            try 
            {
                await _tagCommand.UpdateTagFromStackBase();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
