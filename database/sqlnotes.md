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