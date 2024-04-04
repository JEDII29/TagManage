using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagManage.API.Infrastructure;
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

        public async Task<IEnumerable<TagEntity>> GetQuery()
        {
            var tags = await _appDbContext.Tags.ToArrayAsync();
            return tags;
        }
        public async Task<IEnumerable<TagEntity>> GetCountSortedQuery(int page, int pageSize, SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Ascending)
            {
                return await _appDbContext.Tags.OrderBy(x => x.Count)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            }
            else
            {
                return await _appDbContext.Tags.OrderByDescending(x => x.Count)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            }
        }
        public async Task<IEnumerable<TagEntity>> GetNameSortedQuery(int page, int pageSize, SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Ascending)
            {
                return await _appDbContext.Tags.OrderBy(x => x.Name)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            }
            else
            {
                return await _appDbContext.Tags.OrderByDescending(x => x.Name)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            }
        }


    }
}
