using CVI.Domain.Common;
using CVI.Infrastructure;
using CVI.Service.Referential;
using CVI.Service.Referential.Contract;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace CVI.XUnitTest.Referential
{
    public class ReferentialServiceTest
    {
        #region PROPS

        /// <summary>
        /// Get configuration.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Ges or sets _fakeDbContext
        /// </summary>
        private readonly FakeDbContext _dbContext;

        private readonly CviContext _cVIContext;

        /// <summary>
        /// The gcrService
        /// </summary>
        private readonly IReferentialService _referentialService;

        /// <summary>
        /// The _unitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public ReferentialServiceTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            this._configuration = builder.Build();

            _dbContext = new FakeDbContext();
            _cVIContext = _dbContext.CreateDbContext(_configuration);

            _unitOfWork = new UnitOfWork(_cVIContext);
            _referentialService = new ReferentialService(_unitOfWork);

        }

        [Fact]
        public async Task GetByPmTest()
        {
            var pmResult = await _referentialService.GetByPm(new Service.Referential.Request.GetByPmRequest
            {
                PmName = "FI-49223-0009"
            });

            Assert.NotNull(pmResult);
        }
    }
}
