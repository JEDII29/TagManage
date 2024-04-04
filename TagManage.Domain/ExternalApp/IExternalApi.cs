using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagManage.Data.Entities;

namespace TagManage.Domain.ExternalApp
{
    public interface IExternalApi
    {
        public Task<List<TagEntity>> GetTagsRequest();

        public Task<List<TagEntity>> GetHundredTagsRequest(string url);
    }
}
