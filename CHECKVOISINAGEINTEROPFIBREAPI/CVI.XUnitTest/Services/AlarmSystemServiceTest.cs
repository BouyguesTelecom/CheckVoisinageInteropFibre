using CVI.Domain.Common;
using CVI.Infrastructure;
using CVI.Service.AlarmSystem;
using CVI.Service.AlarmSystem.Contract;
using CVI.Service.AlarmSystem.Request;
using CVI.Service.Referential;
using CVI.Service.Referential.Contract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CVI.XUnitTest.Services
{
    public class AlarmSystemServiceTest
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
        private readonly IAlarmSystemService _alarmSystemService;

        /// <summary>
        /// The _unitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public AlarmSystemServiceTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            this._configuration = builder.Build();

            _dbContext = new FakeDbContext();
            _cVIContext = _dbContext.CreateDbContext(_configuration);

            _unitOfWork = new UnitOfWork(_cVIContext);
            _alarmSystemService = new AlarmSystemService(_unitOfWork);

        }

        [Fact]
        public async Task TaskGetAllAlarmsTestAsync()
        {
            var pmResult = await _alarmSystemService.GetAllAlarms(new GetAllAlarmsRequest
            {
                Asc = "true",
                Filter = new Filter()
                {
                    Address = "FI-49223-0009",
                    AlarmType = "INACT",
                },
                Size = "200",
                SortField = "startTime",
                StartIndex = "1"
            });

            Assert.Null(pmResult.Item1);
            Assert.NotNull(pmResult.Item2);
        }
    }
}
