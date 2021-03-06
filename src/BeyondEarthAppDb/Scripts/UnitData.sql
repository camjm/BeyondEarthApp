﻿DECLARE @unitTechnologyId INT

SELECT @unitTechnologyId = TechnologyId FROM Technology WHERE Name = 'Habitation';
IF @unitTechnologyId IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.Unit WHERE Name = 'Worker')
		INSERT INTO dbo.Unit(TechnologyId, Name, Cost, Strength) 
		VALUES (@unitTechnologyId, 'Worker', 60, 0);

	IF NOT EXISTS (SELECT * FROM dbo.Unit WHERE Name = 'Explorer')
		INSERT INTO dbo.Unit(TechnologyId, Name, Cost, Strength) 
		VALUES (@unitTechnologyId, 'Explorer', 40, 3);

	IF NOT EXISTS (SELECT * FROM dbo.Unit WHERE Name = 'Soldier')
		INSERT INTO dbo.Unit(TechnologyId, Name, Cost, Strength) 
		VALUES (@unitTechnologyId, 'Soldier', 50, 10);
END