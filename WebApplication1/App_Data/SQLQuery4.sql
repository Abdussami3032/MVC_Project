create Database Vehicle_Insurance_Managment;
Use Vehicle_Insurance_Managment;
Create table Customer(Cust_Id int primary Key identity(1,1),Cust_Name varchar(30)not null,Cust_Add varchar (30)not null,Cust_Phone varchar(11)not null,Cust_Email varchar(30) not null,Cust_Pass varchar(15) not null,Cust_CNIC int not null);
Create table Vehicle(Veh_Id int primary key identity(1,1), Veh_Name varchar(30)not null,Veh_OwnerName int foreign key references Customer(Cust_id),Veh_Model varchar(30)not null,Veh_Version varchar(30)not null,Veh_Rate int not null,Veh_BodyNo varchar(30)not null,Veh_EngineNo varchar (30)not null, Veh_No  int);
Create table InsurancePolicy(Pol_Id int primary key identity(1,1),PolicyName varchar(30),Est_Amount int not null,Ins_Amount int not null,Pol_Duration varchar (10) not null);
Create table Purchase(Pur_Id int primary key identity(1,1),Vehicle int foreign key references Vehicle(Veh_Id),Pur_Pol int foreign key references InsurancePolicy(Pol_Id),Pur_Date varchar(20)not null,Total_Amt int not null,PP_Status varchar(10));
Create table Billing(Bill_Id int primary key identity(1,1),Pur_Details int foreign key references Purchase(Pur_Id),Bill_Date varchar(20)not null,Pur_Amount int not null);
Create table Claim(Claim_Id int primary key identity (1,1),Bill_Id int foreign key references Billing(Bill_Id),Claim_Date varchar (10),Accident_Place varchar(30) not null,Accident_Description varchar(MAX) not null,Claim_Amount int not null); 
Create table Company_Expense(Exp_Id int primary key identity(1,1),Exp_Description varchar(MAX) not null,Exp_Amt int not null,Exp_Date varchar(10)not null);
 
 create Table Company_Admin(Admin_Id int primary key identity(1,1), Admin_Name varchar(30) not null unique,Admin_Pass nvarchar(30) not null,Admin_Email nvarchar(30)not null,Admin_Phone int not null);

 alter table Customer
add unique (Cust_Name);

create table testimonials(Test_ID int primary key identity(1,1),Cust_Test varchar(MAX),Cust_Name int foreign key references Customer(Cust_Id));

create table Team(Member_Id int primary key identity(1,1)not null,Member_Name varchar(30) not null,Member_Des varchar(30)not null,Member_img nvarchar(MAX)not null);