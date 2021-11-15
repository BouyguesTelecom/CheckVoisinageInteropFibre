using CVI.Service.CVI.DTO.Photo;
using CVI.Service.Referential.Result;
using CVI.Service.AlarmSystem.ItemData;
using System.Collections.Generic;
using System;
using CVI.Domain.Model;

namespace CVI.Service.CVI.Request
{
    /// <summary>
    /// The SaveAlarmsForPhotoRequest class
    /// </summary>
    public class SaveAlarmsForPhotoRequest
    {

        public PhotoDTO PhotoDTO { get; set; }
        /// <summary>
        /// Client referential result
        /// </summary>
        public IEnumerable<ReferentialModel> ResultReferential { get; set; }
        /// <summary>
        /// Alarms from Alarm System
        /// </summary>
        public List<AlarmItemData> AlarmsFromAlarmSystem { get; set; }
        /// <summary>
        /// DateTime now
        /// </summary>
        public DateTime DateNow { get; set; }
        /// <summary>
        /// Alarms list to add
        /// </summary>
        public List<Alarm> AlarmsToAdd { get; set; }
        /// <summary>
        /// is it the first picture of intervention ?
        /// </summary>
        public bool IsFirstPhoto { get; set; }
        /// <summary>
        /// Client state by default
        /// </summary>
        public StatusFirstCall StatusFirstCallDefault { get; set; }
        public Conclusion ConclusionNouveauKO { get; set; }
        public IEnumerable<AlarmItemData> NewAlarmsForIntervention { get; set; }
    }
}
