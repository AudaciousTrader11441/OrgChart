/*Create function fn_countofsub
(@manager_id int)
returns int
as 
begin
declare @count_reporties int
select @count_reporties=count(Employee.EmployeeId) from Employee where ManagerEmployeeCode = @manager_id and Active = 'True'
return @count_reporties
end

select dbo.fn_countofsub(11811);

Alter procedure sp_employee_details
@Employee_id int
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,RoleName as Role,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(@Employee_id) as Reporties from Employee where EmployeeId=@Employee_id and Active='True'
end
--child--
alter procedure sp_reporting_employee_details
@Employee_id int
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,RoleName as Role,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties from Employee where ManagerEmployeeCode=@Employee_id and Active='True'
end

create procedure sp_all_employee_details
as
begin
select EmployeeId as Id ,ManagerEmployeeCode as ParentId,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,RoleName as Title,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties from Employee where Active='True'
end
--parent--
Create Procedure sp_manager
@Employee_id  int
as 
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,RoleName as Role,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties from Employee where EmployeeId=(select ManagerEmployeeCode from Employee where EmployeeId=@Employee_id and RoleName<>'CEO') and Active = 'True'
end
--siblings
Create Procedure sp_sibling
@Employee_id int
as 
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,RoleName as Role,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties from Employee where ManagerEmployeeCode=(select ManagerEmployeeCode from Employee where EmployeeId=@Employee_id) and Active = 'True'
end
--child
Create procedure sp_child
@Employee_id int
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,RoleName as Role,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties from Employee where ManagerEmployeeCode=@Employee_id and Active='True'
end
--Initial
Create procedure sp_Init
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,RoleName as Role,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties from Employee where EmployeeId=10001 and Active = 'True'
end
*/