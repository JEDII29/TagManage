using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagManage.Data;
using TagManage.Data.Entities;

namespace TagManage.Domain.Query
{
    public class TagQuery
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public TagQuery(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<TagEntity>> GetAllTags()
        {
            var tags = await _appDbContext.Tags.ToArrayAsync();
            return tags;
        }


    }
}
