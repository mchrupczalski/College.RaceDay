DROP VIEW IF EXISTS "vwRaceSummary";
DROP VIEW IF EXISTS "vwDaySummary";
DROP VIEW IF EXISTS "vwLapDetails";
DROP VIEW IF EXISTS "vwDayDetails";

DROP TABLE IF EXISTS "RaceLaps";
DROP TABLE IF EXISTS "RaceRacers";
DROP TABLE IF EXISTS "Racers";
DROP TABLE IF EXISTS "Races";
DROP TABLE IF EXISTS "Days";

CREATE TABLE "Days" (
  "Id" INTEGER NOT NULL UNIQUE 
  ,"Name" TEXT NOT NULL
  ,"Fee" REAL NOT NULL
  ,"LapDistanceKm" REAL NOT NULL
  ,"PetrolCostPerLap" REAL NOT NULL
  
  ,CONSTRAINT "pkDays" PRIMARY KEY("Id" AUTOINCREMENT) 
);


CREATE TABLE "Races" (
  "Id" INTEGER NOT NULL UNIQUE 
  ,"RaceDayId" INTEGER NOT NULL
  ,"RaceDate" TEXT NOT NULL
  
  ,CONSTRAINT "pkRaces" PRIMARY KEY("Id" AUTOINCREMENT)
  ,CONSTRAINT "fkRaces_Days" FOREIGN KEY("RaceDayId") REFERENCES "Days"("Id")
);


CREATE TABLE "Racers" (
  "Id" INTEGER NOT NULL UNIQUE 
  ,"Name" TEXT NOT NULL
  ,"Age" INTEGER NOT NULL
  
  ,CONSTRAINT "pkRacers" PRIMARY KEY("Id" AUTOINCREMENT)
);


CREATE TABLE "RaceRacers" (
  "RaceId" INTEGER NOT NULL
  ,"RaceDayId" INTEGER NOT NULL
  ,"RacerId" INTEGER NOT NULL
  
  ,CONSTRAINT "pkRaceRacers" PRIMARY KEY ("RaceId", "RaceDayId", "RacerId")
  ,CONSTRAINT "fkRaceRacers_Races" FOREIGN KEY("RaceId", "RaceDayId") REFERENCES "Races"("Id", "RaceDayId")
  ,CONSTRAINT "fkRaceRacers_Racers" FOREIGN KEY("RacerId") REFERENCES "Racers"("Id")
);


CREATE TABLE "RaceLaps" (
  "Id" INTEGER NOT NULL UNIQUE 
  ,"RaceId" INTEGER NOT NULL
  ,"RaceDayId" INTEGER NOT NULL  
  ,"RacerId" INTEGER NOT NULL
  ,"LapTimeSeconds" REAL NOT NULL
  
  ,CONSTRAINT "pkRaceLaps" PRIMARY KEY("Id" AUTOINCREMENT)
  ,CONSTRAINT "fkRaceLaps_RaceRacers" FOREIGN KEY("RaceId", "RaceDayId", "RacerId") REFERENCES "RaceRacers"("RaceId", "RaceDayId", "RacerId")
);


CREATE UNIQUE INDEX "ixRaces" ON "Races" (
    "Id"    ASC,
    "RaceDayId"	ASC
);


CREATE VIEW vwDayDetails AS
SELECT
	d.Id AS RaceDayId
	,d.Name AS RaceDayName
	,d.Fee AS SignUpFee
	,d.LapDistanceKm AS LapDistanceKm
	,d.PetrolCostPerLap AS PetrolCostPerLap
	,count(DISTINCT r.Id) AS TotalRaces
	,count(DISTINCT rr.RacerId) AS TotalRacers
	,count(DISTINCT rl.Id) AS TotalLaps
	,d.Fee * count(DISTINCT rr.RacerId) AS TotalIncome
	,d.PetrolCostPerLap * count(DISTINCT rl.Id) AS TotalCost
FROM Days AS d
LEFT JOIN Races AS r
	ON r.RaceDayId = d.Id
LEFT JOIN RaceRacers AS rr
	ON rr.RaceId = r.Id
	AND rr.RaceDayId = r.RaceDayId
LEFT JOIN RaceLaps AS rl
	ON rl.RaceId = r.Id
	AND rl.RaceDayId = r.RaceDayId
	AND rl.RacerId = rr.RacerId
GROUP BY
	d.Id
	,d.Name
	,d.Fee
	,d.LapDistanceKm
	,d.PetrolCostPerLap;
	
	
CREATE VIEW vwLapDetails AS
SELECT 
	rl.RaceId  AS RaceId
	,rl.RaceDayId AS RaceDayId
	,rl.Id AS LapId
	,rl.LapTimeSeconds AS LapTimeSeconds
	,r.Name AS RacerName
	,r.Age AS RacerAge
FROM RaceLaps AS rl
LEFT JOIN Racers AS r
	ON r.Id = rl.RacerId;
	
	
CREATE VIEW vwDaySummary AS
SELECT 
	dd.RaceDayId
	,dd.RaceDayName
	,dd.SignUpFee
	,dd.LapDistanceKm
	,dd.PetrolCostPerLap
	,dd.TotalRaces
	,min(ld.LapTimeSeconds) AS RecordLapTime
	,ld.RacerName AS RecordHolderName
	,dd.TotalIncome AS TotalIncome
	,dd.TotalCost AS TotalCost
FROM vwDayDetails AS dd
LEFT JOIN vwLapDetails AS ld
	ON ld.RaceDayId = dd.RaceDayId
GROUP BY
	dd.RaceDayId
	,dd.RaceDayName
	,dd.SignUpFee
	,dd.LapDistanceKm
	,dd.PetrolCostPerLap
	,dd.TotalRaces
	,dd.TotalIncome
	,dd.TotalCost;
	
	
CREATE VIEW vwRaceSummary AS 
SELECT
     r.Id AS RaceId
	,r.RaceDayId AS RaceDayId
	,r.RaceDate AS RaceDate
	,count(DISTINCT rr.RacerId) AS TotalRacers
	,count(DISTINCT vld.LapId) AS TotalLaps
	,min(vld.LapTimeSeconds) AS BestLapTime
	,vld.RacerName AS BestLapTimeHolder
	,d.Fee * count(DISTINCT rr.RacerId) AS TotalIncome
	,d.PetrolCostPerLap * count(DISTINCT vld.LapId) AS TotalExpense
FROM Races AS r
INNER JOIN Days AS d
	ON d.Id = r.RaceDayId
LEFT JOIN RaceRacers AS rr
	ON rr.RaceId = r.Id
	AND rr.RaceDayId = r.RaceDayId
LEFT JOIN vwLapDetails AS vld
	ON vld.RaceId = r.Id
	AND vld.RaceDayId = r.RaceDayId
GROUP BY
    r.Id
	,r.RaceDayId
	,r.RaceDate;