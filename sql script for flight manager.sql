
USE FlightManager;

--Get users
SELECT TOP (1000) [Id]
      ,[UserName]
      ,[NormalizedUserName]
      ,[Email]
      ,[NormalizedEmail]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[ConcurrencyStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEnd]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,[Address]
      ,[FirstName]
      ,[LastName]
      ,[PersonalIdentificationNumber]
      ,[Role]
  FROM [FlightManager].[dbo].[AspNetUsers]

  --Get roles
  SELECT TOP (1000) [Id]
      ,[Name]
      ,[NormalizedName]
      ,[ConcurrencyStamp]
  FROM [FlightManager].[dbo].[AspNetRoles]

  --Get flights
  SELECT TOP (1000) [UniquePlaneNumber]
      ,[LocationFrom]
      ,[LocationTo]
      ,[DepartureTime]
      ,[LandingTime]
      ,[PlaneType]
      ,[PilotName]
      ,[FreePassengerSeats]
      ,[FreeBusinessSeats]
  FROM [FlightManager].[dbo].[Flights]

  --Delete all users
  DELETE FROM [AspNetUsers] WHERE 1=1

  --Delete all roles
  DELETE FROM [AspNetRoles] WHERE 1=1

  --Delete all flights
  DELETE FROM [Flights] WHERE 1=1

  --Adds 60 test users to the database. You cannot log in these user accounts. They exist for testing the filtering and paging functionality of the project.
  --ENSURE THAT ADMIN EXISTS BEFORE EXECUTING THIS QUERY
  INSERT INTO [AspNetUsers] ([Id],[PersonalIdentificationNumber],[UserName],[FirstName],[LastName],[Email],[PhoneNumber],[Address],[Role],[EmailConfirmed],[PhoneNumberConfirmed],
  [TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount])
  VALUES ('2222d9f0-9c9a-41ca-8fce-4326350z0001','0000000001','user1','u1','u1','u1@abv.bg','0000000001','u1 street','User','0','0','0','0','0'),
    ('2222d9f0-9c9a-41ca-8fce-4326350z0002','0000000002','user2','u2','u2','u2@abv.bg','0000000002','u2 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0003','0000000003','user3','u3','u3','u3@abv.bg','0000000003','u3 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0004','0000000004','user4','u4','u4','u4@abv.bg','0000000004','u4 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0005','0000000005','user5','u5','u5','u5@abv.bg','0000000005','u5 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0006','0000000006','user6','u6','u6','u6@abv.bg','0000000006','u6 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0007','0000000007','user7','u7','u7','u7@abv.bg','0000000007','u7 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0008','0000000008','user8','u8','u8','u8@abv.bg','0000000008','u8 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0009','0000000009','user9','u9','u9','u9@abv.bg','0000000009','u9 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0010','0000000010','user10','u10','u10','u10@abv.bg','0000000010','u10 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0011','0000000011','user11','u11','u11','u11@abv.bg','0000000011','u11 street','User','0','0','0','0','0'),
    ('2222d9f0-9c9a-41ca-8fce-4326350z0012','0000000012','user12','u12','u12','u12@abv.bg','0000000012','u12 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0013','0000000013','user13','u13','u13','u13@abv.bg','0000000013','u13 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0014','0000000014','user14','u14','u14','u14@abv.bg','0000000014','u14 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0015','0000000015','user15','u15','u15','u15@abv.bg','0000000015','u15 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0016','0000000016','user16','u16','u16','u16@abv.bg','0000000016','u16 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0017','0000000017','user17','u17','u17','u17@abv.bg','0000000017','u17 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0018','0000000018','user18','u18','u18','u18@abv.bg','0000000018','u18 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0019','0000000019','user19','u19','u19','u19@abv.bg','0000000019','u19 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0020','0000000020','user20','u20','u20','u20@abv.bg','0000000020','u20 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0021','0000000021','user21','u21','u21','u21@abv.bg','0000000021','u21 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0022','0000000022','user22','u22','u22','u22@abv.bg','0000000022','u22 street','User','0','0','0','0','0'),
    ('2222d9f0-9c9a-41ca-8fce-4326350z0023','0000000023','user23','u23','u23','u23@abv.bg','0000000023','u23 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0024','0000000024','user24','u24','u24','u24@abv.bg','0000000024','u24 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0025','0000000025','user25','u25','u25','u25@abv.bg','0000000025','u25 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0026','0000000026','user26','u26','u26','u26@abv.bg','0000000026','u26 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0027','0000000027','user27','u27','u27','u27@abv.bg','0000000027','u27 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0028','0000000028','user28','u28','u28','u28@abv.bg','0000000028','u28 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0029','0000000029','user29','u29','u29','u29@abv.bg','0000000029','u29 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0030','0000000030','user30','u30','u30','u30@abv.bg','0000000030','u30 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0031','0000000031','user31','u31','u31','u31@abv.bg','0000000031','u31 street','User','0','0','0','0','0'),	
    ('2222d9f0-9c9a-41ca-8fce-4326350z0032','0000000032','user32','u32','u32','u32@abv.bg','0000000032','u32 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0033','0000000033','user33','u33','u33','u33@abv.bg','0000000033','u33 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0034','0000000034','user34','u34','u34','u34@abv.bg','0000000034','u34 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0035','0000000035','user35','u35','u35','u35@abv.bg','0000000035','u35 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0036','0000000036','user36','u36','u36','u36@abv.bg','0000000036','u36 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0037','0000000037','user37','u37','u37','u37@abv.bg','0000000037','u37 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0038','0000000038','user38','u38','u38','u38@abv.bg','0000000038','u38 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0039','0000000039','user39','u39','u39','u39@abv.bg','0000000039','u39 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0040','0000000040','user40','u40','u40','u40@abv.bg','0000000040','u40 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0041','0000000041','user41','u41','u41','u41@abv.bg','0000000041','u41 street','User','0','0','0','0','0'),	
    ('2222d9f0-9c9a-41ca-8fce-4326350z0042','0000000042','user42','u42','u42','u42@abv.bg','0000000042','u42 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0043','0000000043','user43','u43','u43','u43@abv.bg','0000000043','u43 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0044','0000000044','user44','u44','u44','u44@abv.bg','0000000044','u44 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0045','0000000045','user45','u45','u45','u45@abv.bg','0000000045','u45 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0046','0000000046','user46','u46','u46','u46@abv.bg','0000000046','u46 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0047','0000000047','user47','u47','u47','u47@abv.bg','0000000047','u47 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0048','0000000048','user48','u48','u48','u48@abv.bg','0000000048','u48 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0049','0000000049','user49','u49','u49','u49@abv.bg','0000000049','u49 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0050','0000000050','user50','u50','u50','u50@abv.bg','0000000050','u50 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0051','0000000051','user51','u51','u51','u51@abv.bg','0000000051','u51 street','User','0','0','0','0','0'),	
    ('2222d9f0-9c9a-41ca-8fce-4326350z0052','0000000052','user52','u52','u52','u52@abv.bg','0000000052','u52 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0053','0000000053','user53','u53','u53','u53@abv.bg','0000000053','u53 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0054','0000000054','user54','u54','u54','u54@abv.bg','0000000054','u54 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0055','0000000055','user55','u55','u55','u55@abv.bg','0000000055','u55 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0056','0000000056','user56','u56','u56','u56@abv.bg','0000000056','u56 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0057','0000000057','user57','u57','u57','u57@abv.bg','0000000057','u57 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0058','0000000058','user58','u58','u58','u58@abv.bg','0000000058','u58 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0059','0000000059','user59','u59','u59','u59@abv.bg','0000000059','u59 street','User','0','0','0','0','0'),
	('2222d9f0-9c9a-41ca-8fce-4326350z0060','0000000060','user60','u60','u60','u60@abv.bg','0000000060','u60 street','User','0','0','0','0','0');


	-- Adds 60 test flights to the database. They exist for testing the filtering and paging functionality of the project.
	INSERT INTO [Flights] ([LocationFrom],[LocationTo],[DepartureTime],[LandingTime],[PlaneType],[PilotName],[FreePassengerSeats],[FreeBusinessSeats])
	VALUES ('From1','To1','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType1','Pilot1','5','5'),
	('From2','To2','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType2','Pilot2','5','5'),
	('From3','To3','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType3','Pilot3','5','5'),
	('From4','To4','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType4','Pilot4','5','5'),
	('From5','To5','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType5','Pilot5','5','5'),
	('From6','To6','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType6','Pilot6','5','5'),
	('From7','To7','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType7','Pilot7','5','5'),
	('From8','To8','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType8','Pilot8','5','5'),
	('From9','To9','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType9','Pilot9','5','5'),
	('From10','To10','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType10','Pilot10','5','5'),
	('From11','To11','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType11','Pilot11','5','5'),
	('From12','To12','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType12','Pilot12','5','5'),
	('From13','To13','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType13','Pilot13','5','5'),
	('From14','To14','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType14','Pilot14','5','5'),
	('From15','To15','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType15','Pilot15','5','5'),
	('From16','To16','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType16','Pilot16','5','5'),
	('From17','To17','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType17','Pilot17','5','5'),
	('From18','To18','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType18','Pilot18','5','5'),
	('From19','To19','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType19','Pilot19','5','5'),
	('From20','To20','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType20','Pilot20','5','5'),
	('From21','To21','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType21','Pilot21','5','5'),
	('From22','To22','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType22','Pilot22','5','5'),
	('From23','To23','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType23','Pilot23','5','5'),
	('From24','To24','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType24','Pilot24','5','5'),
	('From25','To25','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType25','Pilot25','5','5'),
	('From26','To26','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType26','Pilot26','5','5'),
	('From27','To27','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType27','Pilot27','5','5'),
	('From28','To28','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType28','Pilot28','5','5'),
	('From29','To29','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType29','Pilot29','5','5'),
	('From30','To30','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType30','Pilot30','5','5'),
	('From31','To31','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType31','Pilot31','5','5'),
	('From32','To32','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType32','Pilot32','5','5'),
	('From33','To33','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType33','Pilot33','5','5'),
	('From34','To34','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType34','Pilot34','5','5'),
	('From35','To35','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType35','Pilot35','5','5'),
	('From36','To36','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType36','Pilot36','5','5'),
	('From37','To37','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType37','Pilot37','5','5'),
	('From38','To38','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType38','Pilot38','5','5'),
	('From39','To39','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType39','Pilot39','5','5'),
	('From40','To40','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType40','Pilot40','5','5'),
	('From41','To41','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType41','Pilot41','5','5'),
	('From42','To42','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType42','Pilot42','5','5'),
	('From43','To43','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType43','Pilot43','5','5'),
	('From44','To44','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType44','Pilot44','5','5'),
	('From45','To45','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType45','Pilot45','5','5'),
	('From46','To46','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType46','Pilot46','5','5'),
	('From47','To47','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType47','Pilot47','5','5'),
	('From48','To48','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType48','Pilot48','5','5'),
	('From49','To49','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType49','Pilot49','5','5'),
	('From50','To50','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType50','Pilot50','5','5'),
	('From51','To51','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType51','Pilot51','5','5'),
	('From52','To52','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType52','Pilot52','5','5'),
	('From53','To53','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType53','Pilot53','5','5'),
	('From54','To54','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType54','Pilot54','5','5'),
	('From55','To55','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType55','Pilot55','5','5'),
	('From56','To56','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType56','Pilot56','5','5'),
	('From57','To57','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType57','Pilot57','5','5'),
	('From58','To58','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType58','Pilot58','5','5'),
	('From59','To59','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType59','Pilot59','5','5'),
	('From60','To60','2021-03-06 22:11:00.0000000','2021-03-07 22:11:00.0000000','PlaneType60','Pilot60','5','5');