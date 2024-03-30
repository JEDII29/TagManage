using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagManage.Data;
using TagManage.Data.Entities;

namespace TagManage.Domain.Command
{
    public class TagCommand
    {
        private readonly AppDbContext _appDbContext;

        public TagCommand(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public async Task CreateTag(TagEntity tagEntity)
        {
            tagEntity.Id = Guid.NewGuid();
            await _appDbContext.Tags.AddAsync(tagEntity);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
