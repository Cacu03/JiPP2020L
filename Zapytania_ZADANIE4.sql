
SELECT * FROM CONVERSIONS WHERE name = 'Czas';



SELECT * FROM CONVERSIONS WHERE dateOfConversion BETWEEN '2020-04-14' AND '2020-04-15';



SELECT name, COUNT(*) AS 'aaa' 
    FROM     CONVERSIONS
    GROUP BY name
    ORDER BY 'aaa' DESC;