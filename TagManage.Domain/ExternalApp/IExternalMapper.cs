using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagManage.Data.Entities;

namespace TagManage.Domain.ExternalApp
{
    public interface IExternalMapper
    {
        Task<List<TagEntity>> MapToTagEntity();
    }
}
