/*
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
select EmployeeId as Id ,dbo.fn_relatioship(EmployeeId) ,
 ManagerEmployeeCode as ParentId,CONCAT(FirstName,' ', MiddleName,' ', LastName) as Name,
 RoleName as Title,WorkLocation, Department_name as DepartmentName, dbo.fn_countofsub(EmployeeId) as Reporties
  from Employee where Active='True'
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
alter procedure sp_family
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

//functions
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
  

  alter function fn_has_sibling(@employee_id int)
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

ALTER function fn_relationship(@employee_id int)
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

return @relationship
end

*/

/*
Relationship:parrent,sibbling,child
001 CEO
101 1-0-1
111 ceter
110 no child root
100 only child
*/