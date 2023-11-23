--Create table tbl_Employee
--(
--EmployeeID int identity primary key,
--EmployeeCode int,
--EmployeeName varchar(50),
--DateofBirth datetime,
--Gender bit,
--Department varchar(20),
--Designation	varchar(20),
--BasicSalary	float(8)
--)

--Create Procedure Proc_GetEmployeeDetails
--as
--Begin
-- select * from tbl_Employee 
--End

Create Procedure Proc_SaveEmployeeDetails
(
@EmployeeID int,
@EmployeeCode int,
@EmployeeName varchar(20),
@DateofBirth datetime,
@Gender bit,
@Department varchar(20),
@Designation	varchar(20),
@BasicSalary	float(8),
@mode varchar(5)

)
as
Begin
	if(@mode='ADD')
	Begin
		insert into tbl_Employee values(@EmployeeCode,@EmployeeName,@DateofBirth,@Gender,@Department,@Designation,@BasicSalary)
	End
	if(@mode='EDIT')
	Begin
		update tbl_Employee 
		set EmployeeCode=@EmployeeCode,EmployeeName=@EmployeeName,DateofBirth=@DateofBirth,Gender=@Gender,
		Department=@Department,Designation=@Designation,
		BasicSalary=@BasicSalary 
		Where Employeeid=@EmployeeID
	End
End