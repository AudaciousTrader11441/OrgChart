﻿/*
alter function fn_countofsub
(@manager_id int)
returns int
as 
begin
declare @count_reporties int
select @count_reporties=count(Employee.EmployeeId) from Employee where ManagerEmployeeCode = @manager_id and Active = 'True'and RoleName<>'CEO'
return @count_reporties
end

select dbo.fn_countofsub(12474);

alter procedure sp_employee_details
@Employee_id int
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,
RoleName as Role,WorkLocation, Department_name as DepartmentName,
 dbo.fn_countofsub(@Employee_id) as Reporties 
 ,right('000'+cast(dbo.fn_relationship(EmployeeId) as varchar(3)),3) as relationship
 from Employee where EmployeeId=@Employee_id and Active='True'
end
--child--
alter procedure sp_reporting_employee_details
@Employee_id int
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,
RoleName as Role,WorkLocation, Department_name as DepartmentName, 
dbo.fn_countofsub(EmployeeId) as Reporties 
,right('000'+cast(dbo.fn_relationship(EmployeeId) as varchar(3)),3) as relationship
from Employee where ManagerEmployeeCode=@Employee_id and Active='True'
end

alter procedure sp_all_employee_details
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,
 RoleName as Role,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties,
 right('000'+cast(dbo.fn_relationship(EmployeeId) as varchar(3)),3) as relationship
  from Employee where Active='True' 
end
--search--
alter procedure sp_search_employee
@Employee_name  nvarchar(30)
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name
  from Employee where Active='True' and CONCAT(FirstName,' ', MiddleName,' ', LastName) like '%'+@Employee_name+'%'
end
--parent--
alter Procedure sp_manager
@Employee_id  int
as 
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,
RoleName as Role,WorkLocation, Department_name as DepartmentName, 
dbo.fn_countofsub(EmployeeId) as Reporties
,right('000'+cast(dbo.fn_relationship(EmployeeId) as varchar(3)),3) as relationship
 from Employee where EmployeeId=(select ManagerEmployeeCode from Employee where EmployeeId=@Employee_id and RoleName<>'CEO') and Active = 'True'
end
--siblings
alter Procedure sp_sibling
@Employee_id int
as 
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,
RoleName as Role,WorkLocation, Department_name as DepartmentName,
 dbo.fn_countofsub(EmployeeId) as Reporties
 ,right('000'+cast(dbo.fn_relationship(EmployeeId) as varchar(3)),3) as relationship
  from Employee where ManagerEmployeeCode=(select ManagerEmployeeCode from Employee where EmployeeId=@Employee_id) and Active = 'True'and RoleName<>'CEO' and EmployeeId<>@Employee_id
end
--child
alter procedure sp_child
@Employee_id int
as
begin
select EmployeeId as Id , CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name
,RoleName as Role,WorkLocation, Department_name as DepartmentName, 
dbo.fn_countofsub(EmployeeId) as Reporties
,right('000'+cast(dbo.fn_relationship(EmployeeId) as varchar(3)),3) as relationship
 from Employee where ManagerEmployeeCode=@Employee_id and RoleName<>'CEO' and Active = 'True'
end

--family
create procedure sp_family
@Employee_id int
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name
,RoleName as Role,WorkLocation, Department_name as DepartmentName, 
dbo.fn_countofsub(EmployeeId) as Reporties
,right('000'+cast(dbo.fn_relationship(EmployeeId) as varchar(3)),3) as relationship
 from Employee where ManagerEmployeeCode=@Employee_id and Active='True'
end

--Initial
alter procedure sp_Init
as
begin
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name
,RoleName as Role,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties
,right('000'+cast(dbo.fn_relationship(EmployeeId) as varchar(3)),3) as relationship
 from Employee where EmployeeId=11358 and Active = 'True'
end

select* from Employee where FirstName like '%giri%'


--functions
create function fn_has_child(@employee_id int)
returns int
as 
begin
declare @count_reporties int
select @count_reporties=count(Employee.EmployeeId) from Employee where ManagerEmployeeCode = @employee_id and Active = 'True'
if(@count_reporties>0)
set @count_reporties = 1
return @count_reporties
end

create function fn_has_parent(@employee_id int)
returns int
as 
begin
declare @count_reporties int
select @count_reporties=count(Employee.EmployeeId) from Employee where EmployeeId = @employee_id and ManagerEmployeeCode is not null and RoleName<>'CEO' and Active = 'True'

return @count_reporties
  end


 create function fn_has_sibling(@employee_id int)
returns int
as 
begin
declare @count_reporties int
select @count_reporties=count(Employee.EmployeeId) from Employee where EmployeeId<> @employee_id and ManagerEmployeeCode= (select ManagerEmployeeCode from Employee where EmployeeId= @employee_id and RoleName<>'CEO')  and Active = 'True'
if(@count_reporties>0)
set @count_reporties = 1
return @count_reporties
end


select dbo.fn_relationship(10001);

alter function fn_relationship(@employee_id int)
returns varchar(3)
as 
begin
declare @relationship int
if(dbo.fn_has_child(@employee_id)=1 and dbo.fn_has_parent(@employee_id)=1 and dbo.fn_has_sibling(@employee_id)=1)
set @relationship = 111

if(dbo.fn_has_child(@employee_id)=1 and dbo.fn_has_parent(@employee_id)=0 and dbo.fn_has_sibling(@employee_id)=0)
set @relationship = 001

if(dbo.fn_has_child(@employee_id)=0 and dbo.fn_has_parent(@employee_id)=1 and dbo.fn_has_sibling(@employee_id)=1)
set @relationship = 110
if(dbo.fn_has_child(@employee_id)=0 and dbo.fn_has_parent(@employee_id)=1 and dbo.fn_has_sibling(@employee_id)=0)
set @relationship = 100
if(dbo.fn_has_child(@employee_id)=1 and dbo.fn_has_parent(@employee_id)=1 and dbo.fn_has_sibling(@employee_id)=0)
set @relationship = 101
if(dbo.fn_has_child(@employee_id)=0 and dbo.fn_has_parent(@employee_id)=0 and dbo.fn_has_sibling(@employee_id)=0)
set @relationship = 000

return @relationship
end

alter procedure sp_Tree_Child
@Employee_id int
as
begin
if @Employee_id != 10000
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,
dbo.fn_countofsub(EmployeeId) as Reporties 
from Employee where ManagerEmployeeCode=@Employee_id and Active='True' and RoleName<>'CEO';
else
select EmployeeId as Id ,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,
dbo.fn_countofsub(EmployeeId) as Reporties 
from Employee where ManagerEmployeeCode=10001 and Active='True' and RoleName='CEO';
end
*/