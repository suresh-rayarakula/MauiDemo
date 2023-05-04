USE [Demo]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5/4/2023 7:06:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Phone] [varchar](20) NULL,
	[Address] [varchar](200) NULL,
	[HireDate] [date] NULL,
	[Salary] [decimal](10, 2) NULL,
 CONSTRAINT [PK__Employee__7AD04FF1B0D2D50D] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Employee_CRUD]    Script Date: 5/4/2023 7:06:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Employee_CRUD]
    @EmployeeID INT = NULL,
    @FirstName VARCHAR(50) = NULL,
    @LastName VARCHAR(50) = NULL,
    @Email VARCHAR(100) = NULL,
    @Phone VARCHAR(20) = NULL,
    @Address VARCHAR(200) = NULL,
    @HireDate DATE = NULL,
    @Salary DECIMAL(10,2) = NULL,
    @Action VARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @Action = 'INSERT'
    BEGIN
        INSERT INTO Employee (FirstName, LastName, Email, Phone, Address, HireDate, Salary)
        VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @HireDate, @Salary);
        
        SELECT SCOPE_IDENTITY() AS NewID; -- Returns the ID of the newly inserted row
    END
    
    IF @Action = 'UPDATE'
    BEGIN
        UPDATE Employee
        SET FirstName = ISNULL(@FirstName, FirstName),
            LastName = ISNULL(@LastName, LastName),
            Email = ISNULL(@Email, Email),
            Phone = ISNULL(@Phone, Phone),
            Address = ISNULL(@Address, Address),
            HireDate = ISNULL(@HireDate, HireDate),
            Salary = ISNULL(@Salary, Salary)
        WHERE EmployeeID = @EmployeeID;
    END
    
    IF @Action = 'DELETE'
    BEGIN
        DELETE FROM Employee
        WHERE EmployeeID = @EmployeeID;
    END
    
    IF @Action = 'SELECT'
    BEGIN
        SELECT EmployeeID, FirstName, LastName, Email, Phone, Address, HireDate, Salary
        FROM Employee
        WHERE EmployeeID = @EmployeeID;
    END
    
    IF @Action = 'SELECTALL'
    BEGIN
        SELECT EmployeeID, FirstName, LastName, Email, Phone, Address, HireDate, Salary
        FROM Employee;
    END
END
GO
