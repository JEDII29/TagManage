using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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

        [HttpGet("GetTags")]
        public async Task<IActionResult> GetTags()
        {
            return Ok(await _tagQuery.GetQuery());
        }

        [HttpGet("GetCountSortedTags")]
        public async Task<IActionResult> GetCountSortedTags([FromQuery]int page = 1, [FromQuery]int pagesize = 10, [FromQuery] SortDirection sortDirection = SortDirection.Descending)
        {
            return Ok(await _tagQuery.GetCountSortedQuery(page, pagesize, sortDirection));
        }
        [HttpGet("GetPercentageCountSortedTags")]
        public async Task<IActionResult> GetPercentageCountSortedTags([FromQuery] int page = 1, [FromQuery] int pagesize = 10, [FromQuery] SortDirection sortDirection = SortDirection.Descending)
        {
            return Ok(await _percentageTagQuery.GetCountSortedQuery(page, pagesize, sortDirection));
        }

        [HttpPost("PostTag")]
        public async Task<IActionResult> PostTag(CreateTagRequest createTagRequest)
        {
            await _tagCommand.CreateTag(_mapper.Map<TagEntity>(createTagRequest));
            return Ok();
        }
        [HttpPut("PutTags")]
        public async Task<IActionResult> PutTags()
        {
            await _tagCommand.UpdateTagFromStackBase();
            return Ok();
        }


    }
}
