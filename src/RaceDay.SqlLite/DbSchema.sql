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