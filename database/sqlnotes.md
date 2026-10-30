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