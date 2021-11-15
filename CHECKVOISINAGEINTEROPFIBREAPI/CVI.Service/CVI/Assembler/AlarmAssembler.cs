using CVI.Domain.Model;
using CVI.Service.CVI.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CVI.Service.CVI.Assembler
{
    /// <summary>
    /// The AlarmAssembler class
    /// </summary>
    public static class AlarmAssembler
    {
        /// <summary>
        /// FROM DTO to ENtity
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        public static AlarmDTO ToAlarmDTO(this Alarm alarm)
        {
            return new AlarmDTO
            {
                Address = alarm.Address,
                AlarmType = alarm.AlarmType,
                ClearTime = alarm.ClearTime,
                NotificationId = alarm.NotificationId,
                ONTNr = alarm.ONTNr,
                StartTime = alarm.StartTime,
                ID = alarm.ID
            };
        }


        /// <summary>
        /// FROM DTO to Entity
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        public static Alarm ToAlarm(this AlarmDTO alarm)
        {
            return new Alarm
            {
                Address = alarm.Address,
                AlarmType = alarm.AlarmType,
                ClearTime = alarm.ClearTime,
                NotificationId = alarm.NotificationId,
                ONTNr = alarm.ONTNr,
                StartTime = alarm.StartTime,
                ID = alarm.ID
            };
        }
    }
}
