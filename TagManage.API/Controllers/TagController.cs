using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagManage.API.Requests;
using TagManage.Data.Entities;
using TagManage.Domain.Command;
using TagManage.Domain.Query;

namespace TagManage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TagCommand _createTagCommand;
        private readonly TagQuery _tagsQuery;

        public TagController(IMapper mapper, TagCommand createTagCommand,
            TagQuery getTagsQuery) 
        { 
            _mapper = mapper;
            _createTagCommand = createTagCommand;
            _tagsQuery = getTagsQuery;
        }

        [HttpGet("GetTags")]
        public async Task<IActionResult> GetTags()
        {
            return Ok(await _tagsQuery.GetAllTags());
        }

        [HttpPost("PostTag")]
        public async Task<IActionResult> PostTag(CreateTagRequest createTagRequest)
        {
            _createTagCommand.CreateTag(_mapper.Map<TagEntity>(createTagRequest));
            return Ok();
        }


    }
}
