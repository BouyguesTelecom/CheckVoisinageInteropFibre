using CVI.Domain.Model;
using CVI.Domain.Model.Alarms;
using CVI.Domain.Model.Intervention;
using CVI.Domain.Model.Referential;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CVI.Infrastructure
{
    public static class InitializeDatabase
    {
        public static void DataBenchAlarmsNotifications(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlarmNotification>().HasData(
                new AlarmNotification
                {
                    ID = 1,
                    NotificationId = "AMS:379039455",
                    PonPath = "OLT3404-LT1-PON4",
                    AlarmType = "INACT",
                    Address = "OLT3404:0-0-1-4",
                    ONTNr = "0",
                    StartDateTime = DateTime.Parse("2021-03-08 11:44:17.0000000"),
                    StartTime = "2021-03-08 11:44:17.0000000",
                    CreationDate = DateTime.Parse("2021-03-08 13:58:36.5912869"),
                    ClearDateTime = DateTime.Parse("2021-03-08 11:44:47.0000000"),
                    ClearTime = "2021-03-08 11:44:47.0000000",
                },
                new AlarmNotification
                {
                    ID = 2,
                    NotificationId = "AMS:379040690",
                    PonPath = "OLT3404-LT1-PON4",
                    AlarmType = "INACT",
                    Address = "OLT3404:0-0-1-4",
                    ONTNr = "1",
                    StartDateTime = DateTime.Parse("2021-03-08 11:44:17.0000000"),
                    StartTime = "2021-03-08 11:44:17.0000000",
                    CreationDate = DateTime.Parse("2021-03-08 13:58:36.5912869"),
                    ClearDateTime = DateTime.Parse("2021-03-08 11:44:47.0000000"),
                    ClearTime = "2021-03-08 11:44:47.0000000"
                },
                new AlarmNotification
                {
                    ID = 3,
                    NotificationId = "AMS:379040690",
                    PonPath = "OLT3404-LT1-PON4",
                    AlarmType = "INACT",
                    Address = "OLT3404:0-0-1-4",
                    ONTNr = "2",
                    StartDateTime = DateTime.Parse("2021-03-08 11:44:17.0000000"),
                    StartTime = "2021-03-08 11:44:17.0000000",
                    CreationDate = DateTime.Parse("2021-03-08 13:58:36.5912869"),
                    ClearDateTime = DateTime.Parse("2021-03-08 11:44:47.0000000"),
                    ClearTime = "2021-03-08 11:44:47.0000000"
                },
                new AlarmNotification
                {
                    ID = 4,
                    NotificationId = "AMS:379040690",
                    PonPath = "OLT3404-LT1-PON4",
                    AlarmType = "INACT",
                    Address = "OLT3404:0-0-1-4",
                    ONTNr = "3",
                    StartDateTime = DateTime.Parse("2021-03-08 11:44:17.0000000"),
                    StartTime = "2021-03-08 11:44:17.0000000",
                    CreationDate = DateTime.Parse("2021-03-08 13:58:36.5912869")
                },
                new AlarmNotification
                {
                    ID = 5,
                    NotificationId = "AMS:379040690",
                    PonPath = "OLT3404-LT1-PON4",
                    AlarmType = "INACT",
                    Address = "OLT3404:0-0-1-4",
                    ONTNr = "4",
                    StartDateTime = DateTime.Parse("2021-03-08 11:44:17.0000000"),
                    StartTime = "2021-03-08 11:44:17.0000000",
                    CreationDate = DateTime.Parse("2021-03-08 13:58:36.5912869")
                }
            );
        }
    }
}
