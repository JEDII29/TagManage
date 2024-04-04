using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagManage.Data;
using TagManage.Data.Entities;
using TagManage.Domain.ExternalApp;

namespace TagManage.Domain.Command
{
    public class TagCommand
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExternalApi _externalApiService;

        public TagCommand(AppDbContext appContext, IExternalApi externalApi)
        {
            _appDbContext = appContext;
            _externalApiService = externalApi;
        }

        public async Task CreateTag(TagEntity tagEntity)
        {
            tagEntity.Id = Guid.NewGuid();
            await _appDbContext.Tags.AddAsync(tagEntity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateTagFromStackBase()
        {
            var tagsFromStackBase = await _externalApiService.SendRequest();
            foreach (var tag in tagsFromStackBase)
            {
                tag.Id = Guid.NewGuid();
            }
            _appDbContext.Tags.RemoveRange(_appDbContext.Tags);
            await _appDbContext.Tags.AddRangeAsync(tagsFromStackBase);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
