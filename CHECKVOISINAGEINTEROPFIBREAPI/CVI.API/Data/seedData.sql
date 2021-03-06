SET IDENTITY_INSERT [dbo].[CL_CLIENT_STATUS] ON 

INSERT [dbo].[CL_CLIENT_STATUS] ([ID], [Name], [IsDeleted], [IsCommentMandatory], [IsDefault], [CreationDate]) VALUES (1, N'KO - Churner', 0, 0, 0, CAST(N'2021-02-11T10:32:27.1545670' AS DateTime2))
INSERT [dbo].[CL_CLIENT_STATUS] ([ID], [Name], [IsDeleted], [IsCommentMandatory], [IsDefault], [CreationDate]) VALUES (2, N'KO - Déménagement', 0, 0, 0, CAST(N'2021-02-11T10:32:27.1545670' AS DateTime2))
INSERT [dbo].[CL_CLIENT_STATUS] ([ID], [Name], [IsDeleted], [IsCommentMandatory], [IsDefault], [CreationDate]) VALUES (3, N'KO - Autre', 1, 0, 0, CAST(N'2021-02-11T10:32:27.1545670' AS DateTime2))
INSERT [dbo].[CL_CLIENT_STATUS] ([ID], [Name], [IsDeleted], [IsCommentMandatory], [IsDefault], [CreationDate]) VALUES (4, N'KO - Refus Tech', 0, 1, 0, CAST(N'2021-02-11T10:32:27.1545670' AS DateTime2))
INSERT [dbo].[CL_CLIENT_STATUS] ([ID], [Name], [IsDeleted], [IsCommentMandatory], [IsDefault], [CreationDate]) VALUES (5, N'Non Identifié', 0, 0, 1, CAST(N'2021-02-11T10:32:27.1545670' AS DateTime2))
INSERT [dbo].[CL_CLIENT_STATUS] ([ID], [Name], [IsDeleted], [IsCommentMandatory], [IsDefault], [CreationDate]) VALUES (6, N'Indéterminé', 0, 1, 0, CAST(N'2021-02-11T10:32:27.1545670' AS DateTime2))
INSERT [dbo].[CL_CLIENT_STATUS] ([ID], [Name], [IsDeleted], [IsCommentMandatory], [IsDefault], [CreationDate]) VALUES (7, N'Demande réparation', 0, 1, 0, CAST(N'2021-02-11T10:32:27.1545670' AS DateTime2))
SET IDENTITY_INSERT [dbo].[CL_CLIENT_STATUS] OFF
SET IDENTITY_INSERT [dbo].[CL_CONCLUSIONS] ON 

INSERT [dbo].[CL_CONCLUSIONS] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (1, N'Nouveau voisin KO', 0, CAST(N'2021-10-26T10:54:41.9252435' AS DateTime2))
INSERT [dbo].[CL_CONCLUSIONS] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (2, N'Problème non identifié', 0, CAST(N'2021-10-26T10:54:41.9252872' AS DateTime2))
INSERT [dbo].[CL_CONCLUSIONS] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (3, N'Panne résolue', 0, CAST(N'2021-10-26T10:54:41.9252909' AS DateTime2))
INSERT [dbo].[CL_CONCLUSIONS] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (4, N'Panne résolue', 0, CAST(N'2021-10-26T10:54:41.9252913' AS DateTime2))
INSERT [dbo].[CL_CONCLUSIONS] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (5, N'Réparation complexe PM ou PB', 0, CAST(N'2021-10-26T10:54:41.9252916' AS DateTime2))
INSERT [dbo].[CL_CONCLUSIONS] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (6, N'Infos OI erronées ou manquantes', 0, CAST(N'2021-10-26T10:54:41.9252919' AS DateTime2))
INSERT [dbo].[CL_CONCLUSIONS] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (7, N'Autre', 0, CAST(N'2021-10-26T10:54:41.9252922' AS DateTime2))
SET IDENTITY_INSERT [dbo].[CL_CONCLUSIONS] OFF
SET IDENTITY_INSERT [dbo].[CL_LOCALISATIONS] ON 

INSERT [dbo].[CL_LOCALISATIONS] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (1, N'PM', 0, CAST(N'2021-10-26T11:32:00.4383241' AS DateTime2))
SET IDENTITY_INSERT [dbo].[CL_LOCALISATIONS] OFF
SET IDENTITY_INSERT [dbo].[CL_SUB_CAUSES] ON 

INSERT [dbo].[CL_SUB_CAUSES] ([ID], [Name], [IsDeleted], [CreationDate]) VALUES (1, N'Brassage nouvelle jarretière', 0, CAST(N'2021-10-26T11:31:51.5773864' AS DateTime2))
SET IDENTITY_INSERT [dbo].[CL_SUB_CAUSES] OFF
SET IDENTITY_INSERT [dbo].[INT_INTERVENTION_PMS] ON 

INSERT [dbo].[INT_INTERVENTION_PMS] ([ID], [PMName], [CreationDate]) VALUES (1, N'FI-49223-0009', CAST(N'2021-10-26T10:54:41.9184831' AS DateTime2))
SET IDENTITY_INSERT [dbo].[INT_INTERVENTION_PMS] OFF
SET IDENTITY_INSERT [dbo].[INT_INTERVENTION_TYPES] ON 

INSERT [dbo].[INT_INTERVENTION_TYPES] ([ID], [InterventionTypeName], [IsDeleted], [Duration], [CreationDate]) VALUES (1, N'SAV', 0, CAST(N'02:00:00' AS Time), CAST(N'2021-10-26T10:54:41.9227067' AS DateTime2))
INSERT [dbo].[INT_INTERVENTION_TYPES] ([ID], [InterventionTypeName], [IsDeleted], [Duration], [CreationDate]) VALUES (2, N'RACCO', 0, CAST(N'04:00:00' AS Time), CAST(N'2021-10-26T10:54:41.9237970' AS DateTime2))
SET IDENTITY_INSERT [dbo].[INT_INTERVENTION_TYPES] OFF
SET IDENTITY_INSERT [dbo].[REF_OPERATORS] ON 

INSERT [dbo].[REF_OPERATORS] ([ID], [OperatorName], [CreationDate]) VALUES (1, N'BOUYGUES TELECOM', CAST(N'2021-10-26T10:54:41.9238952' AS DateTime2))
INSERT [dbo].[REF_OPERATORS] ([ID], [OperatorName], [CreationDate]) VALUES (2, N'ORANGE', CAST(N'2021-10-26T10:54:41.9239451' AS DateTime2))
INSERT [dbo].[REF_OPERATORS] ([ID], [OperatorName], [CreationDate]) VALUES (3, N'SFR', CAST(N'2021-10-26T10:54:41.9239488' AS DateTime2))
INSERT [dbo].[REF_OPERATORS] ([ID], [OperatorName], [CreationDate]) VALUES (4, N'FREE', CAST(N'2021-10-26T10:54:41.9239515' AS DateTime2))
SET IDENTITY_INSERT [dbo].[REF_OPERATORS] OFF
SET IDENTITY_INSERT [dbo].[REF_USERS] ON 

INSERT [dbo].[REF_USERS] ([ID], [FirstName], [LastName], [Login], [Profil], [CreationDate]) VALUES (1, N'FIT DEV', N'BOUYGUES TELECOM', N'fitdev', N'Administrateur', CAST(N'2021-10-26T11:27:15.1556814' AS DateTime2))
SET IDENTITY_INSERT [dbo].[REF_USERS] OFF
SET IDENTITY_INSERT [dbo].[INT_INTERVENTIONS] ON 

INSERT [dbo].[INT_INTERVENTIONS] ([ID], [InterventionName], [BeginDate], [InterventionPto], [UnloadedCode], [InterventionTypeID], [OperatorID], [UserID], [MutualisationPointID], [CreationDate]) VALUES (1, N'FI-49223-0009', CAST(N'2021-10-26T07:27:12.1000000' AS DateTime2), N'AAAAAAAA', NULL, 2, 2, 1, 1, CAST(N'2021-10-26T11:27:15.3388118' AS DateTime2))
SET IDENTITY_INSERT [dbo].[INT_INTERVENTIONS] OFF
SET IDENTITY_INSERT [dbo].[CL_CLIENT_RESULTS] ON 

INSERT [dbo].[CL_CLIENT_RESULTS] ([ID], [SrvID], [ONTPath], [AdditionalInfos], [FirstCallComent], [IsDown], [StatusFirstCallId], [LocalizationId], [ConclusionId], [SubCauseId], [InterventionId], [ClientStep], [IdOSS], [CreationDate]) VALUES (1, 32737000, N'OLT3404-LT1-PON4-ONT0', NULL, NULL, 0, 5, NULL, NULL, NULL, 1, 0, 16754978, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[CL_CLIENT_RESULTS] ([ID], [SrvID], [ONTPath], [AdditionalInfos], [FirstCallComent], [IsDown], [StatusFirstCallId], [LocalizationId], [ConclusionId], [SubCauseId], [InterventionId], [ClientStep], [IdOSS], [CreationDate]) VALUES (2, 23839713, N'OLT3404-LT1-PON4-ONT1', NULL, NULL, 0, 5, NULL, NULL, NULL, 1, 0, 16757386, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[CL_CLIENT_RESULTS] ([ID], [SrvID], [ONTPath], [AdditionalInfos], [FirstCallComent], [IsDown], [StatusFirstCallId], [LocalizationId], [ConclusionId], [SubCauseId], [InterventionId], [ClientStep], [IdOSS], [CreationDate]) VALUES (3, 33747276, N'OLT3404-LT1-PON4-ONT2', NULL, NULL, 0, 5, NULL, NULL, NULL, 1, 0, 16811442, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[CL_CLIENT_RESULTS] ([ID], [SrvID], [ONTPath], [AdditionalInfos], [FirstCallComent], [IsDown], [StatusFirstCallId], [LocalizationId], [ConclusionId], [SubCauseId], [InterventionId], [ClientStep], [IdOSS], [CreationDate]) VALUES (4, 29133607, N'OLT3404-LT1-PON4-ONT3', N'Rien', N'Rien', 0, 7, 1, 4, NULL, 1, 1, 16840387, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[CL_CLIENT_RESULTS] ([ID], [SrvID], [ONTPath], [AdditionalInfos], [FirstCallComent], [IsDown], [StatusFirstCallId], [LocalizationId], [ConclusionId], [SubCauseId], [InterventionId], [ClientStep], [IdOSS], [CreationDate]) VALUES (5, 28120268, N'OLT3404-LT1-PON4-ONT4', N'Rien', N'Rien', 0, 7, 1, 3, NULL, 1, 1, 16842641, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
SET IDENTITY_INSERT [dbo].[CL_CLIENT_RESULTS] OFF
INSERT [dbo].[LOC_SUB_CAUSES] ([LocalizationsID], [SubCausesID]) VALUES (1, 1)
SET IDENTITY_INSERT [dbo].[PH_PHOTO_RESULTS] ON 

INSERT [dbo].[PH_PHOTO_RESULTS] ([ID], [ClientNumber], [DownClientNumber], [NOKClientNumber], [AverageDownTime], [LowerTo20DownNumber], [GreaterTo20DownNumber], [OperationStatus], [SwitchedOffNumber], [ListDGOFF], [CreationDate]) VALUES (1, 11, 3, 2, 0, 0, 0, NULL, 0, N'', CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[PH_PHOTO_RESULTS] ([ID], [ClientNumber], [DownClientNumber], [NOKClientNumber], [AverageDownTime], [LowerTo20DownNumber], [GreaterTo20DownNumber], [OperationStatus], [SwitchedOffNumber], [ListDGOFF], [CreationDate]) VALUES (2, 11, 0, 0, 0, 0, 0, NULL, 0, N'', CAST(N'2021-10-26T11:31:04.1517297' AS DateTime2))
INSERT [dbo].[PH_PHOTO_RESULTS] ([ID], [ClientNumber], [DownClientNumber], [NOKClientNumber], [AverageDownTime], [LowerTo20DownNumber], [GreaterTo20DownNumber], [OperationStatus], [SwitchedOffNumber], [ListDGOFF], [CreationDate]) VALUES (3, 11, 0, 0, 0, 0, 0, NULL, 0, N'', CAST(N'2021-10-26T11:34:23.3409949' AS DateTime2))
SET IDENTITY_INSERT [dbo].[PH_PHOTO_RESULTS] OFF
SET IDENTITY_INSERT [dbo].[PH_PHOTOS] ON 

INSERT [dbo].[PH_PHOTOS] ([ID], [Description], [InterventionId], [UserID], [PhotoResultID], [CreationDate]) VALUES (1, N'', 1, 1, 1, CAST(N'2021-10-26T11:30:08.0419015' AS DateTime2))
INSERT [dbo].[PH_PHOTOS] ([ID], [Description], [InterventionId], [UserID], [PhotoResultID], [CreationDate]) VALUES (2, N'', 1, 1, 2, CAST(N'2021-10-26T11:31:04.1404075' AS DateTime2))
SET IDENTITY_INSERT [dbo].[PH_PHOTOS] OFF
SET IDENTITY_INSERT [dbo].[PH_ALARMS] ON 

INSERT [dbo].[PH_ALARMS] ([ID], [NotificationId], [AlarmType], [Address], [ONTNr], [StartTime], [ClearTime], [PhotoId], [ClientResultID], [CreationDate]) VALUES (6, N'AMS:379039455', N'INACT', N'OLT3404:0-0-1-4', N'0', CAST(N'2021-10-26T11:32:08.0000000' AS DateTime2), CAST(N'2021-10-26T11:32:28.0000000' AS DateTime2), 1, 1, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[PH_ALARMS] ([ID], [NotificationId], [AlarmType], [Address], [ONTNr], [StartTime], [ClearTime], [PhotoId], [ClientResultID], [CreationDate]) VALUES (7, N'AMS:379040690', N'INACT', N'OLT3404:0-0-1-4', N'1', CAST(N'2021-10-26T11:32:08.0000000' AS DateTime2), CAST(N'2021-10-26T11:32:28.0000000' AS DateTime2), 1, 2, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[PH_ALARMS] ([ID], [NotificationId], [AlarmType], [Address], [ONTNr], [StartTime], [ClearTime], [PhotoId], [ClientResultID], [CreationDate]) VALUES (8, N'AMS:379040690', N'INACT', N'OLT3404:0-0-1-4', N'2', CAST(N'2021-10-26T11:32:08.0000000' AS DateTime2), CAST(N'2021-10-26T11:32:28.0000000' AS DateTime2), 1, 3, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[PH_ALARMS] ([ID], [NotificationId], [AlarmType], [Address], [ONTNr], [StartTime], [ClearTime], [PhotoId], [ClientResultID], [CreationDate]) VALUES (9, N'AMS:379040690', N'INACT', N'OLT3404:0-0-1-4', N'3', CAST(N'2021-10-26T11:32:08.0000000' AS DateTime2), NULL, 1, 4, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[PH_ALARMS] ([ID], [NotificationId], [AlarmType], [Address], [ONTNr], [StartTime], [ClearTime], [PhotoId], [ClientResultID], [CreationDate]) VALUES (10, N'AMS:379040690', N'INACT', N'OLT3404:0-0-1-4', N'4', CAST(N'2021-10-26T11:32:08.0000000' AS DateTime2), NULL, 1, 5, CAST(N'2021-10-26T11:30:08.1270825' AS DateTime2))
INSERT [dbo].[PH_ALARMS] ([ID], [NotificationId], [AlarmType], [Address], [ONTNr], [StartTime], [ClearTime], [PhotoId], [ClientResultID], [CreationDate]) VALUES (11, N'AMS:379040690', N'INACT', N'OLT3404:0-0-1-4', N'4', CAST(N'2021-10-26T11:32:08.0000000' AS DateTime2), CAST(N'2021-10-26T11:33:24.0000000' AS DateTime2), 2, 5, CAST(N'2021-10-26T11:31:04.1517297' AS DateTime2))
INSERT [dbo].[PH_ALARMS] ([ID], [NotificationId], [AlarmType], [Address], [ONTNr], [StartTime], [ClearTime], [PhotoId], [ClientResultID], [CreationDate]) VALUES (12, N'AMS:379040690', N'INACT', N'OLT3404:0-0-1-4', N'3', CAST(N'2021-10-26T11:32:08.0000000' AS DateTime2), CAST(N'2021-10-26T11:33:24.0000000' AS DateTime2), 2, 4, CAST(N'2021-10-26T11:31:04.1517297' AS DateTime2))
SET IDENTITY_INSERT [dbo].[PH_ALARMS] OFF
SET IDENTITY_INSERT [dbo].[NOT_EQUIPEMENTS] ON 

INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (1, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 32737000, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'0', N'OLT3404-LT1-PON4-ONT0', 16754978, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9250003' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (2, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 23839713, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'1', N'OLT3404-LT1-PON4-ONT1', 16757386, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251757' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (3, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 33747276, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'2', N'OLT3404-LT1-PON4-ONT2', 16811442, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251841' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (4, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 29133607, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'3', N'OLT3404-LT1-PON4-ONT3', 16840387, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251906' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (5, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 28120268, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'4', N'OLT3404-LT1-PON4-ONT4', 16842641, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251917' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (6, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 21059900, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'5', N'OLT3404-LT1-PON4-ONT5', 17091103, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251925' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (7, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 34212638, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'6', N'OLT3404-LT1-PON4-ONT6', 17183238, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251933' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (8, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 34339298, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'7', N'OLT3404-LT1-PON4-ONT7', 17262378, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251941' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (9, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 30143386, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'8', N'OLT3404-LT1-PON4-ONT8', 17303941, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251949' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (10, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 32434997, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'9', N'OLT3404-LT1-PON4-ONT9', 17431254, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251957' AS DateTime2))
INSERT [dbo].[NOT_EQUIPEMENTS] ([ID], [Date], [SrvId], [NomNro], [PonPath], [NomPm], [NumONT], [ONTPath], [IdOSS], [LinkStatus], [CreationDate]) VALUES (11, CAST(N'2020-05-15T15:45:57.0000000' AS DateTime2), 35058039, N'49223MUR', N'OLT3404-LT1-PON4', N'FI-49223-0009', N'10', N'OLT3404-LT1-PON4-ONT10', 17628949, N'CONNECTED', CAST(N'2021-10-26T10:54:41.9251965' AS DateTime2))
SET IDENTITY_INSERT [dbo].[NOT_EQUIPEMENTS] OFF
