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
using TagManage.Domain.DataTransmitModels;

namespace TagManage.Domain.Query
{
    public class PercentageTagQuery(IMapper mapper, AppDbContext appDbContext)
    {
        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _appDbContext = appDbContext;


        public async Task<IEnumerable<PercentageTagDTO>> GetCountSortedQuery(int page, int pageSize, SortDirection sortDirection)
        {
            int totalCount = await _appDbContext.Tags.SumAsync(x => x.Count);
            IEnumerable<TagEntity> tags = new List<TagEntity>();
            if (sortDirection == SortDirection.Ascending)
            {
                tags = await _appDbContext.Tags.OrderBy(x => x.Count)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            }
            else
            {
                tags = await _appDbContext.Tags.OrderByDescending(x => x.Count)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            }
            var result = tags.Select(tag => new PercentageTagDTO
            {
                Name = tag.Name,
                PercentageCount = (tag.Count * 100.0) / totalCount
            });
            return result;
        }
        public async Task<IEnumerable<PercentageTagDTO>> GetNameSortedQuery(int page, int pageSize, SortDirection sortDirection)
        {
            int totalCount = await _appDbContext.Tags.SumAsync(x => x.Count);
            IEnumerable<TagEntity> tags = new List<TagEntity>();
            if (sortDirection == SortDirection.Ascending)
            {
                tags = await _appDbContext.Tags.OrderBy(x => x.Name)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            }
            else
            {
                tags = await _appDbContext.Tags.OrderByDescending(x => x.Name)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            }
            var result = tags.Select(tag => new PercentageTagDTO
            {
                Name = tag.Name,
                PercentageCount = (tag.Count * 100.0) / totalCount
            });
            return result;
        }
    }
}
