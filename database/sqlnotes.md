## Task 03 — SELECT + WHERE

### SQL
```sql
SELECT * FROM Employees WHERE Salary > 50000;
--табл.1
EmployeeId	EmployeeName	Salary	HireDate	DepartmentId	ManagerId
1	Анна Иванова	65000.00	2020-03-15	1	NULL
2	Пётр Сидоров	72000.00	2019-07-01	1	NULL
3	Мария Кузнецова	55000.00	2021-11-20	1	2
5	Ольга Смирнова	61000.00	2020-09-05	2	NULL
6	Дмитрий Орлов	58000.00	2022-04-18	2	5
7	Елена Волкова	52000.00	2023-06-01	2	NULL
10	Алексей Лебедев	90000.00	2018-05-25	4	NULL

SELECT * FROM Employees WHERE YEAR(HireDate) = 2023;
--табл.2
EmployeeId	EmployeeName	Salary	HireDate	DepartmentId	ManagerId
4	Иван Петров	48000.00	2023-02-10	1	1
7	Елена Волкова	52000.00	2023-06-01	2	NULL

SELECT * FROM Employees WHERE EmployeeName LIKE N'А%';
--табл.3
EmployeeId	EmployeeName	Salary	HireDate	DepartmentId	ManagerId
1	Анна Иванова	65000.00	2020-03-15	1	NULL
10	Алексей Лебедев	90000.00	2018-05-25	4	NULL

SELECT * FROM Employees WHERE DepartmentId = 2;
--табл.4
EmployeeId	EmployeeName	Salary	HireDate	DepartmentId	ManagerId
5	Ольга Смирнова	61000.00	2020-09-05	2	NULL
6	Дмитрий Орлов	58000.00	2022-04-18	2	5
7	Елена Волкова	52000.00	2023-06-01	2	NULL

SELECT * FROM Employees WHERE Salary BETWEEN 40000 AND 60000;
--табл. ласт(5)
EmployeeId	EmployeeName	Salary	HireDate	DepartmentId	ManagerId
3	Мария Кузнецова	55000.00	2021-11-20	1	2
4	Иван Петров	48000.00	2023-02-10	1	1
6	Дмитрий Орлов	58000.00	2022-04-18	2	5
7	Елена Волкова	52000.00	2023-06-01	2	NULL
8	Сергей Морозов	45000.00	2021-12-12	3	NULL
9	Наталья Соколова	43000.00	2024-01-15	3	10

## Task 04 — ORDER BY + TOP

### SQL
```sql
SELECT TOP 3 EmployeeName, Salary
FROM Employees
ORDER BY Salary DESC;
-- сорт. сотрудников по зарплате -
EmployeeName	Salary
Алексей Лебедев	90000.00
Пётр Сидоров	72000.00
Анна Иванова	65000.00

SELECT TOP 5 EmployeeName, HireDate
FROM Employees
ORDER BY HireDate ASC;
-- топ5сторожил
EmployeeName	HireDate
Алексей Лебедев	2018-05-25
Пётр Сидоров	2019-07-01
Анна Иванова	2020-03-15
Ольга Смирнова	2020-09-05
Мария Кузнецова	2021-11-20

SELECT EmployeeName, DepartmentId, Salary
FROM Employees
ORDER BY DepartmentId ASC, Salary DESC;
-- сорт. по отделу + зп
EmployeeName	DepartmentId	Salary
Пётр Сидоров	1	72000.00
Анна Иванова	1	65000.00
Мария Кузнецова	1	55000.00
Иван Петров	1	48000.00
Ольга Смирнова	2	61000.00
Дмитрий Орлов	2	58000.00
Елена Волкова	2	52000.00
Сергей Морозов	3	45000.00
Наталья Соколова	3	43000.00
Алексей Лебедев	4	90000.00

## Task 05 — COUNT, SUM, AVG, MIN и MAX

### SQL
```sql
SELECT COUNT(*) AS TotalEmployees FROM Employees;
-- кол-во сотр.
TotalEmployees
10

SELECT SUM(Salary) AS TotalSalary FROM Employees;
-- все зп
TotalSalary
589000.00

SELECT ROUND(AVG(Salary), 2) AS AvgSalary FROM Employees;
-- сред зп 2 зн.
AvgSalary
58900.000000

SELECT MIN(Salary) AS MinSalary, MAX(Salary) AS MaxSalary FROM Employees;
-- мин и мак зп
MinSalary	MaxSalary
43000.00	90000.00

SELECT COUNT(*) AS HighPaidCount FROM Employees WHERE Salary > 50000;
-- кол-во сотр. с зп > 50к
HighPaidCount
7

## Task 06 — GROUP BY: статистика по отделам

### SQL
```sql
SELECT 
    DepartmentId,
    COUNT(*) AS EmployeeCount,
    ROUND(AVG(Salary), 2) AS AvgSalary,
    SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
ORDER BY AvgSalary DESC;

DepartmentId	EmployeeCount	AvgSalary	TotalSalary
4	1	90000.000000	90000.00
1	4	60000.000000	240000.00
2	3	57000.000000	171000.00
3	2	44000.000000	88000.00

## Task 07 — GROUP BY + HAVING: фильтрация групп

### SQL
```sql
SELECT DepartmentId, COUNT(*) AS EmployeeCount
FROM Employees
GROUP BY DepartmentId
HAVING COUNT(*) > 2;
-- отделы с 2+ людьми
DepartmentId	EmployeeCount
1	4
2	3

SELECT DepartmentId, ROUND(AVG(Salary), 2) AS AvgSalary
FROM Employees
GROUP BY DepartmentId
HAVING AVG(Salary) > 55000;
-- отделы с ср. зп. > 55000
DepartmentId	AvgSalary
1	60000.000000
2	57000.000000
4	90000.000000

SELECT DepartmentId, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
HAVING SUM(Salary) > 150000;
-- отделы с сумм. зп 150000
DepartmentId	TotalSalary
1	240000.00
2	171000.00

## Task 08 — INNER JOIN: сотрудники и отделы

### SQL
```sql
SELECT 
    e.EmployeeName, 
    d.DepartmentName, 
    e.Salary
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
ORDER BY d.DepartmentName ASC, e.Salary DESC;
-- сотрудники + отделы
EmployeeName	DepartmentName	Salary
Ольга Смирнова	Аналитика	61000.00
Дмитрий Орлов	Аналитика	58000.00
Елена Волкова	Аналитика	52000.00
Алексей Лебедев	Менеджмент	90000.00
Пётр Сидоров	Разработка	72000.00
Анна Иванова	Разработка	65000.00
Мария Кузнецова	Разработка	55000.00
Иван Петров	Разработка	48000.00
Сергей Морозов	Тестирование	45000.00
Наталья Соколова	Тестирование	43000.00

## Task 09 — LEFT JOIN: сотрудники без проектов

### SQL
```sql
SELECT 
    e.EmployeeId, 
    e.EmployeeName
FROM Employees e
LEFT JOIN EmployeeProjects ep ON ep.EmployeeId = e.EmployeeId
WHERE ep.EmployeeId IS NULL;
--белая ворона
EmployeeId	EmployeeName
9	Наталья Соколова

## Task 10 — Множественный JOIN: сотрудники, отделы, проекты

### SQL
```sql
SELECT 
    e.EmployeeName, 
    d.DepartmentName, 
    p.ProjectName, 
    ep.Hours
FROM Employees e
INNER JOIN Departments d ON d.DepartmentId = e.DepartmentId
INNER JOIN EmployeeProjects ep ON ep.EmployeeId = e.EmployeeId
INNER JOIN Projects p ON p.ProjectId = ep.ProjectId
ORDER BY d.DepartmentName ASC, e.EmployeeName ASC, p.ProjectName ASC;
-- назначения
EmployeeName	DepartmentName	ProjectName	Hours
Дмитрий Орлов	Аналитика	AI Assistant	50
Дмитрий Орлов	Аналитика	Data Warehouse	70
Елена Волкова	Аналитика	Mobile App	95
Ольга Смирнова	Аналитика	Data Warehouse	110
Алексей Лебедев	Менеджмент	AI Assistant	200
Алексей Лебедев	Менеджмент	CRM System	30
Анна Иванова	Разработка	CRM System	120
Анна Иванова	Разработка	Data Warehouse	80
Иван Петров	Разработка	Legacy Refactor	40
Иван Петров	Разработка	Mobile App	90
Мария Кузнецова	Разработка	Mobile App	100
Пётр Сидоров	Разработка	AI Assistant	60
Пётр Сидоров	Разработка	CRM System	150
Сергей Морозов	Тестирование	Legacy Refactor	130

## Task 11. Self-join: иерархия руководителей

### SQL
```sql
SELECT 
    e.EmployeeName, 
    m.EmployeeName AS ManagerName
FROM Employees e
LEFT JOIN Employees m ON e.ManagerId = m.EmployeeId;
--сотрудник+ руководитель
EmployeeName	ManagerName
Анна Иванова	NULL
Пётр Сидоров	NULL
Мария Кузнецова	Пётр Сидоров
Иван Петров	Анна Иванова
Ольга Смирнова	NULL
Дмитрий Орлов	Ольга Смирнова
Елена Волкова	NULL
Сергей Морозов	NULL
Наталья Соколова	Алексей Лебедев
Алексей Лебедев	NULL

## Task 12. Подзапросы в WHERE

### SQL
```sql
SELECT EmployeeName, Salary 
FROM Employees 
WHERE Salary > (SELECT AVG(Salary) FROM Employees);
-- сотр. с зп выше ср.
EmployeeName	Salary
Анна Иванова	65000.00
Пётр Сидоров	72000.00
Ольга Смирнова	61000.00
Алексей Лебедев	90000.00

SELECT EmployeeName, Salary 
FROM Employees 
WHERE Salary = (SELECT MAX(Salary) FROM Employees);
-- сотр. с макс зп
EmployeeName	Salary
Алексей Лебедев	90000.00

## Task 13. Скалярные подзапросы в SELECT

### SQL
```sql
SELECT 
    EmployeeName, 
    Salary, 
    DepartmentId,
    ROUND(Salary - (SELECT AVG(Salary) FROM Employees e2 WHERE e2.DepartmentId = e.DepartmentId), 2) AS DiffFromAvg
FROM Employees e;
--откл от зп
EmployeeName	Salary	DepartmentId	DiffFromAvg
Анна Иванова	65000.00	1	5000.000000
Пётр Сидоров	72000.00	1	12000.000000
Мария Кузнецова	55000.00	1	-5000.000000
Иван Петров	48000.00	1	-12000.000000
Ольга Смирнова	61000.00	2	4000.000000
Дмитрий Орлов	58000.00	2	1000.000000
Елена Волкова	52000.00	2	-5000.000000
Сергей Морозов	45000.00	3	1000.000000
Наталья Соколова	43000.00	3	-1000.000000
Алексей Лебедев	90000.00	4	0.000000

## Task 14. EXISTS / NOT EXISTS

### SQL
```sql
SELECT EmployeeName 
FROM Employees e
WHERE EXISTS (
    SELECT 1 FROM EmployeeProjects ep WHERE ep.EmployeeId = e.EmployeeId
);
-- сотр. с проектами
EmployeeName
Анна Иванова
Пётр Сидоров
Мария Кузнецова
Иван Петров
Ольга Смирнова
Дмитрий Орлов
Елена Волкова
Сергей Морозов
Алексей Лебедев

SELECT EmployeeName 
FROM Employees e
WHERE NOT EXISTS (
    SELECT 1 FROM EmployeeProjects ep WHERE ep.EmployeeId = e.EmployeeId
);
-- сотр. без проектов
EmployeeName
Наталья Соколова

## Task 15. UNION / UNION ALL

### SQL
```sql
SELECT EmployeeName, Salary, HireDate FROM Employees WHERE Salary > 60000
UNION
SELECT EmployeeName, Salary, HireDate FROM Employees WHERE YEAR(HireDate) < 2022;
-- UNION без дубликатов
EmployeeName	Salary	HireDate
Алексей Лебедев	90000.00	2018-05-25
Анна Иванова	65000.00	2020-03-15
Мария Кузнецова	55000.00	2021-11-20
Ольга Смирнова	61000.00	2020-09-05
Пётр Сидоров	72000.00	2019-07-01
Сергей Морозов	45000.00	2021-12-12

SELECT EmployeeName, Salary, HireDate FROM Employees WHERE Salary > 60000
UNION ALL
SELECT EmployeeName, Salary, HireDate FROM Employees WHERE YEAR(HireDate) < 2022;
-- UNION ALL (с дубликатами)
EmployeeName	Salary	HireDate
Анна Иванова	65000.00	2020-03-15
Пётр Сидоров	72000.00	2019-07-01
Ольга Смирнова	61000.00	2020-09-05
Алексей Лебедев	90000.00	2018-05-25
Анна Иванова	65000.00	2020-03-15
Пётр Сидоров	72000.00	2019-07-01
Мария Кузнецова	55000.00	2021-11-20
Ольга Смирнова	61000.00	2020-09-05
Сергей Морозов	45000.00	2021-12-12
Алексей Лебедев	90000.00	2018-05-25

## Task 16. UPDATE: изменение данных

### SQL
```sql
--16.1
SELECT EmployeeId, EmployeeName, DepartmentId, Salary FROM Employees WHERE DepartmentId = 1;
-- было
EmployeeId	EmployeeName	DepartmentId	Salary
1	Анна Иванова	1	65000.00
2	Пётр Сидоров	1	72000.00
3	Мария Кузнецова	1	55000.00
4	Иван Петров	1	48000.00

UPDATE Employees 
SET Salary = Salary * 1.1 
WHERE DepartmentId = 1;
-- refrash

SELECT EmployeeId, EmployeeName, DepartmentId, Salary FROM Employees WHERE DepartmentId = 1;
-- стало
EmployeeId	EmployeeName	DepartmentId	Salary
1	Анна Иванова	1	71500.00
2	Пётр Сидоров	1	79200.00
3	Мария Кузнецова	1	60500.00
4	Иван Петров	1	52800.00

--16.2
SELECT EmployeeName, HireDate, Salary FROM Employees WHERE HireDate < '2022-01-01';
-- до
EmployeeName	HireDate	Salary
Анна Иванова	2020-03-15	71500.00
Пётр Сидоров	2019-07-01	79200.00
Мария Кузнецова	2021-11-20	60500.00
Ольга Смирнова	2020-09-05	61000.00
Сергей Морозов	2021-12-12	45000.00
Алексей Лебедев	2018-05-25	90000.00

UPDATE Employees 
SET Salary = Salary + 5000 
WHERE HireDate < '2022-01-01';
-- refrash

SELECT EmployeeName, HireDate, Salary FROM Employees WHERE HireDate < '2022-01-01';
-- после
EmployeeName	HireDate	Salary
Анна Иванова	2020-03-15	76500.00
Пётр Сидоров	2019-07-01	84200.00
Мария Кузнецова	2021-11-20	65500.00
Ольга Смирнова	2020-09-05	66000.00
Сергей Морозов	2021-12-12	50000.00
Алексей Лебедев	2018-05-25	95000.00

--16.3
SELECT EmployeeId, EmployeeName, HireDate FROM Employees WHERE EmployeeId = 1;
--до
EmployeeId	EmployeeName	HireDate
1	Анна Иванова	2020-03-15

UPDATE Employees 
SET HireDate = '2023-05-01' 
WHERE EmployeeId = 1;
-- refrash

SELECT EmployeeId, EmployeeName, HireDate FROM Employees WHERE EmployeeId = 1;
-- после
EmployeeId	EmployeeName	HireDate
1	Анна Иванова	2023-05-01

## Task 17. DELETE: удаление данных

### SQL
```sql
--17.1
SELECT COUNT(*) FROM EmployeeProjects WHERE Hours = 0;
-- до
(Отсутствует имя столбца)
0

DELETE FROM EmployeeProjects WHERE Hours = 0;
-- удаляем

SELECT COUNT(*) FROM EmployeeProjects WHERE Hours = 0;
-- после
(Отсутствует имя столбца)
0

--17.2
SELECT * FROM Employees WHERE EmployeeId = 8;
-- до
EmployeeId	EmployeeName	Salary	HireDate	DepartmentId	ManagerId
8	Сергей Морозов	50000.00	2021-12-12	3	NULL

DELETE FROM EmployeeProjects WHERE EmployeeId = 8;
-- удаляем назначение

DELETE FROM Employees WHERE EmployeeId = 8;
-- удаляем сотрудника

SELECT * FROM Employees WHERE EmployeeId = 8;
-- после
EmployeeId	EmployeeName	Salary	HireDate	DepartmentId	ManagerId

--17.3
SELECT * INTO #TestTable FROM Employees;
-- тест таб.

SELECT COUNT(*) AS BeforeDelete FROM #TestTable;
-- кол-во строк
BeforeDelete
9

DELETE FROM #TestTable;
-- удаляем все

SELECT COUNT(*) AS AfterDelete FROM #TestTable;
-- после
AfterDelete
0

DROP TABLE #TestTable;
-- удаляем тест. таб.

## Task 18. Транзакции: BEGIN TRAN / COMMIT / ROLLBACK

### SQL
```sql
--18.1
SELECT EmployeeName, Salary FROM Employees WHERE EmployeeId = 2;
-- до
EmployeeName	Salary
Пётр Сидоров	84200.00

BEGIN TRANSACTION;
-- транзакция

UPDATE Employees SET Salary = 100000 WHERE EmployeeId = 2;
-- смена зп

SELECT EmployeeName, Salary FROM Employees WHERE EmployeeId = 2;
-- чек внутрь транзакции
EmployeeName	Salary
Пётр Сидоров	100000.00

ROLLBACK TRANSACTION;
-- откат

SELECT EmployeeName, Salary FROM Employees WHERE EmployeeId = 2;
-- после отката
EmployeeName	Salary
Пётр Сидоров	84200.00

--18.2
BEGIN TRANSACTION;
-- начало

UPDATE Employees SET Salary = 100000 WHERE EmployeeId = 2;
-- смена зп

COMMIT TRANSACTION;
-- сохр изменения

SELECT EmployeeName, Salary FROM Employees WHERE EmployeeId = 2;
-- проверка
EmployeeName	Salary
Пётр Сидоров	100000.00

## Task 19. Представления (VIEW)

### SQL
```sql
CREATE VIEW vw_EmployeeDetails AS
SELECT 
    e.EmployeeName, 
    d.DepartmentName, 
    p.ProjectName, 
    ep.Hours
FROM Employees e
INNER JOIN Departments d ON d.DepartmentId = e.DepartmentId
INNER JOIN EmployeeProjects ep ON ep.EmployeeId = e.EmployeeId
INNER JOIN Projects p ON p.ProjectId = ep.ProjectId;
GO
-- создние представления

SELECT * FROM vw_EmployeeDetails;
GO
-- проверка на работаспасобность
EmployeeName	DepartmentName	ProjectName	Hours
Анна Иванова	Разработка	CRM System	120
Анна Иванова	Разработка	Data Warehouse	80
Пётр Сидоров	Разработка	CRM System	150
Пётр Сидоров	Разработка	AI Assistant	60
Мария Кузнецова	Разработка	Mobile App	100
Иван Петров	Разработка	Mobile App	90
Иван Петров	Разработка	Legacy Refactor	40
Ольга Смирнова	Аналитика	Data Warehouse	110
Дмитрий Орлов	Аналитика	Data Warehouse	70
Дмитрий Орлов	Аналитика	AI Assistant	50
Елена Волкова	Аналитика	Mobile App	95
Алексей Лебедев	Менеджмент	CRM System	30
Алексей Лебедев	Менеджмент	AI Assistant	200

DROP VIEW vw_EmployeeDetails;
GO
-- удаление представления

## Task 20. Индексы: создание и анализ

### SQL
```sql
--20.1
SET STATISTICS IO ON;
GO

SELECT * FROM Employees WHERE DepartmentId = 1 AND Salary > 50000;
GO

--20.2
CREATE INDEX IX_Employees_DepartmentId ON Employees(DepartmentId);
CREATE INDEX IX_Employees_Salary ON Employees(Salary);
GO

--20.3
SELECT * FROM Employees WHERE DepartmentId = 1 AND Salary > 50000;
GO
EmployeeId	EmployeeName	Salary	HireDate	DepartmentId	ManagerId
1	Анна Иванова	76500.00	2023-05-01	1	NULL
2	Пётр Сидоров	100000.00	2019-07-01	1	NULL
3	Мария Кузнецова	65500.00	2021-11-20	1	2
4	Иван Петров	52800.00	2023-02-10	1	1

--20.4
SET STATISTICS IO OFF;
GO

## Task 21. Хранимые процедуры

### SQL
```sql
--21.1
DROP PROCEDURE IF EXISTS sp_GetEmployeesByDepartment;
DROP PROCEDURE IF EXISTS sp_GetAvgSalaryByDept;
GO
-- перестраховка

CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DeptId INT
AS
BEGIN
    SELECT EmployeeName, Salary, HireDate
    FROM Employees
    WHERE DepartmentId = @DeptId;
END;
GO
-- с вход. параметром 

CREATE PROCEDURE sp_GetAvgSalaryByDept
    @DeptId INT,
    @AvgSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT @AvgSalary = AVG(Salary)
    FROM Employees
    WHERE DepartmentId = @DeptId;
END;
GO
-- OUTPUT-параметр

--22.2
EXEC sp_GetEmployeesByDepartment @DeptId = 1;
-- вызов 1ой
EmployeeName	Salary	HireDate
Анна Иванова	76500.00	2023-05-01
Пётр Сидоров	100000.00	2019-07-01
Мария Кузнецова	65500.00	2021-11-20
Иван Петров	52800.00	2023-02-10

DECLARE @MyAvg DECIMAL(10,2);
EXEC sp_GetAvgSalaryByDept @DeptId = 1, @AvgSalary = @MyAvg OUTPUT;
SELECT @MyAvg AS AverageSalary_Dept1;
-- вызов 2ой
AverageSalary_Dept1
73700.00

## Task 22. Функции: скалярная и табличная

### SQL
```sql
DROP FUNCTION IF EXISTS fn_GetAnnualSalary;
DROP FUNCTION IF EXISTS fn_GetEmployeesBySalaryRange;
GO
--удаление предидущего задания

CREATE FUNCTION fn_GetAnnualSalary (@MonthlySalary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @MonthlySalary * 12;
END;
GO
-- скалярная фун-я

CREATE FUNCTION fn_GetEmployeesBySalaryRange (@Min DECIMAL(10,2), @Max DECIMAL(10,2))
RETURNS TABLE
AS
RETURN (
    SELECT EmployeeName, Salary 
    FROM Employees 
    WHERE Salary BETWEEN @Min AND @Max
);
GO
-- табличная фун-я

-- вызов

SELECT EmployeeName, Salary, dbo.fn_GetAnnualSalary(Salary) AS AnnualSalary
FROM Employees;
-- скалярная прямо в SELECT
EmployeeName	Salary	AnnualSalary
Анна Иванова	76500.00	918000.00
Пётр Сидоров	100000.00	1200000.00
Мария Кузнецова	65500.00	786000.00
Иван Петров	52800.00	633600.00
Ольга Смирнова	66000.00	792000.00
Дмитрий Орлов	58000.00	696000.00
Елена Волкова	52000.00	624000.00
Наталья Соколова	43000.00	516000.00
Алексей Лебедев	95000.00	1140000.00

SELECT * FROM dbo.fn_GetEmployeesBySalaryRange(50000, 70000);
-- Табличная вызывается в FROM
EmployeeName	Salary
Мария Кузнецова	65500.00
Иван Петров	52800.00
Ольга Смирнова	66000.00
Дмитрий Орлов	58000.00
Елена Волкова	52000.00

## Task 23. Оконные функции: ROW_NUMBER, RANK, DENSE_RANK

### SQL
```sql
SELECT 
    EmployeeName, 
    DepartmentId, 
    Salary,
    ROW_NUMBER() OVER (ORDER BY Salary DESC) AS RowNum,
    -- сквозная нумерация всех сотрудников по убыванию зп

    -- ранжирование внутри отделов.
    RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS Rnk,
    -- при равной зп RANK пропускает числа
    
    DENSE_RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS DenseRnk
    -- при равной зарплате DENSE_RANK не пропускает числа
FROM Employees;
--таб
EmployeeName	DepartmentId	Salary	RowNum	Rnk	DenseRnk
Пётр Сидоров	1	100000.00	1	1	1
Анна Иванова	1	76500.00	3	2	2
Мария Кузнецова	1	65500.00	5	3	3
Иван Петров	1	52800.00	7	4	4
Ольга Смирнова	2	66000.00	4	1	1
Дмитрий Орлов	2	58000.00	6	2	2
Елена Волкова	2	52000.00	8	3	3
Наталья Соколова	3	43000.00	9	1	1
Алексей Лебедев	4	95000.00	2	1	1

## Task 24. Оконные агрегаты: SUM OVER, AVG OVER

### SQL
```sql
SELECT 
    EmployeeName, 
    DepartmentId, 
    Salary,
    SUM(Salary) OVER (PARTITION BY DepartmentId) AS DeptTotalSalary,
    -- cумм зп по отделу
    
    AVG(Salary) OVER (PARTITION BY DepartmentId) AS DeptAvgSalary,
    -- cредняя зарплата по отделу
    
    ROUND(Salary - AVG(Salary) OVER (PARTITION BY DepartmentId), 2) AS DiffFromAvg
    -- отклонение от AVG
FROM Employees;
--таб
EmployeeName	DepartmentId	Salary	DeptTotalSalary	DeptAvgSalary	DiffFromAvg
Анна Иванова	1	76500.00	294800.00	73700.000000	2800.000000
Пётр Сидоров	1	100000.00	294800.00	73700.000000	26300.000000
Мария Кузнецова	1	65500.00	294800.00	73700.000000	-8200.000000
Иван Петров	1	52800.00	294800.00	73700.000000	-20900.000000
Ольга Смирнова	2	66000.00	176000.00	58666.666666	7333.330000
Дмитрий Орлов	2	58000.00	176000.00	58666.666666	-666.670000
Елена Волкова	2	52000.00	176000.00	58666.666666	-6666.670000
Наталья Соколова	3	43000.00	43000.00	43000.000000	0.000000
Алексей Лебедев	4	95000.00	95000.00	95000.000000	0.000000

## Task 25. CTE (Common Table Expressions)

### SQL
```sql
WITH AvgSalary AS (
    SELECT AVG(Salary) AS AvgSal FROM Employees
)
SELECT e.EmployeeName, e.Salary
FROM Employees e
CROSS JOIN AvgSalary a
WHERE e.Salary > a.AvgSal;
-- task12 через WITH
EmployeeName	Salary
Анна Иванова	76500.00
Алексей Лебедев	95000.00
Пётр Сидоров	100000.00

WITH DeptAvg AS (
    SELECT DepartmentId, AVG(Salary) AS AvgSalary
    FROM Employees
    GROUP BY DepartmentId
)
SELECT TOP 3 d.DepartmentName, ROUND(da.AvgSalary, 2) AS AvgSalary
FROM DeptAvg da
JOIN Departments d ON d.DepartmentId = da.DepartmentId
ORDER BY da.AvgSalary DESC;
-- топ 3 отдела по AVG зп CTE
DepartmentName	AvgSalary
Менеджмент	95000.000000
Разработка	73700.000000
Аналитика	58666.670000

## Task 26. CASE: условная логика в SELECT

### SQL
```sql
SELECT 
    EmployeeName, 
    Salary,    
    CASE 
        WHEN Salary > 60000 THEN 'High'
        WHEN Salary BETWEEN 40000 AND 60000 THEN 'Medium'
        ELSE 'Low'
    END AS SalaryLevel,
    -- оценка зп

    HireDate,    
    CASE 
        WHEN YEAR(HireDate) < 2020 THEN 'Senior'
        WHEN YEAR(HireDate) BETWEEN 2020 AND 2022 THEN 'Mid'
        ELSE 'Junior'
    END AS Tenure
    -- оценка стажа
FROM Employees;
--таб
EmployeeName	Salary	SalaryLevel	HireDate	Tenure
Анна Иванова	76500.00	High	2023-05-01	Junior
Пётр Сидоров	100000.00	High	2019-07-01	Senior
Мария Кузнецова	65500.00	High	2021-11-20	Mid
Иван Петров	52800.00	Medium	2023-02-10	Junior
Ольга Смирнова	66000.00	High	2020-09-05	Mid
Дмитрий Орлов	58000.00	Medium	2022-04-18	Mid
Елена Волкова	52000.00	Medium	2023-06-01	Junior
Наталья Соколова	43000.00	Medium	2024-01-15	Junior
Алексей Лебедев	95000.00	High	2018-05-25	Senior

## Task 27. Работа с датами: DATEADD, DATEDIFF, FORMAT

### SQL
```sql
SELECT 
    EmployeeName, 
    HireDate,    
    DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsOfTenure,
    -- лет стажа
    
    DATEDIFF(MONTH, HireDate, GETDATE()) AS MonthsOfTenure,
    -- кол-во месяцев стажа
    
    DATEADD(YEAR, 1, HireDate) AS DateAfterOneYear,
    -- дата через год после найма
    
    FORMAT(HireDate, 'dd.MM.yyyy') AS FormattedHireDate
    -- формат даты
FROM Employees;
--таб
EmployeeName	HireDate	YearsOfTenure	MonthsOfTenure	DateAfterOneYear	FormattedHireDate
Анна Иванова	2023-05-01	3	41	2024-05-01	01.05.2023
Пётр Сидоров	2019-07-01	7	87	2020-07-01	01.07.2019
Мария Кузнецова	2021-11-20	5	59	2022-11-20	20.11.2021
Иван Петров	2023-02-10	3	44	2024-02-10	10.02.2023
Ольга Смирнова	2020-09-05	6	73	2021-09-05	05.09.2020
Дмитрий Орлов	2022-04-18	4	54	2023-04-18	18.04.2022
Елена Волкова	2023-06-01	3	40	2024-06-01	01.06.2023
Наталья Соколова	2024-01-15	2	33	2025-01-15	15.01.2024
Алексей Лебедев	2018-05-25	8	101	2019-05-25	25.05.2018

## Task 28. C# + SQL: подключение и SELECT

### SQL
```sql
