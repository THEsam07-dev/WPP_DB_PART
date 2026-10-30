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