using CVI.Service.Referential.Request;
using CVI.Service.Referential.Result;
using System.Collections.Generic;


using System.Threading.Tasks;

namespace CVI.Service.Referential.Contract
{
    public interface IReferentialService
    {
        public Task<IEnumerable<ReferentialModel>> GetByPm(GetByPmRequest request);
    }
}
