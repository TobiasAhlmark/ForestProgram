SELECT * FROM ForestArea;

----------------------

Select * From Species
    Where name = @name;

----------------------

INSERT INTO TreeManagements (Action, Date, NumberOfTreesTreated, Responsible, Note, ForestAreId)
    VALUES (@Action, @Date, @NumberOfTreesTreated, @Responsible, @Note, @ForestAreId);

----------------------

SELECT 
    ForestArea.Location AS Location, Species.Name AS Species, Enviroment.Wind AS Wind
FROM 
    ForeastArea
JOIN 
    ForestAreaToSpecy ON ForestArea.ForeatArea.Id = ForestAreaToSpecy. ForestAreaId
JOIN 
    Species ON ForestAreaToSpicy.SpeciesId = Species.SpeciesId
JOIN    
    Enviroment ON ForestArea.EnviromentId = Enviroment.EnviromentId;

---------------------

SELECT COUNT(*)
FROM 
    ForestAreas
WHERE 
    Location = @Location;

---------------------

UPDATE 
    ForestArea
SET 
    Location = 'South Forest', AreaSize = 150.0
WHERE 
    ForestAreaId = 1;

---------------------

SELECT 
    Species.Name
FROM 
    Species
JOIN 
    ForestAreaToSpecy ON Species.SpeciesId = ForestAreaToSpecy.SpeciesId
JOIN 
    ForestArea ON ForestAreaToSpecy.ForestAreaId = ForestArea.ForestAreaId
WHERE 
    ForestArea.Location = 'North Forest';

----------------------

SELECT 
    ForestArea.Location AS ForestLocation, 
    Environment.Wind AS WindCondition
FROM 
    ForestArea
JOIN 
    Environment ON ForestArea.EnvironmentId = Environment.EnvironmentId;

----------------------

SELECT 
    ForestArea.Location AS ForestLocation, 
    Environment.Wind AS WindCondition
FROM 
    ForestArea
JOIN 
    Environment ON ForestArea.EnvironmentId = Environment.EnvironmentId
ORDER BY 
    Environment.Wind DESC;

----------------------

SELECT 
    ForestArea.Location AS ForestLocation, 
    Species.Name AS SpeciesName
FROM 
    ForestArea
JOIN 
    ForestAreaToSpecy ON ForestArea.ForestAreaId = ForestAreaToSpecy.ForestAreaId
JOIN 
    Species ON ForestAreaToSpecy.SpeciesId = Species.SpeciesId
ORDER BY 
    ForestArea.Location ASC;