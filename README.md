# AMMDotNetCoreTrainning

C# .NET

C# Language .NET

Console App Windows Forms ASP.NET Core Web API ASP.NET Core Web MVC Blazor Web Assembly Blazor Web Server

.NET framework (1, 2, 3, 3.5, 4, 4.5, 4.6, 4.7, 4.8) windows .NET Core (1, 2, 2.2, 3, 3.1) vs2019, vs2022 - windows, linux, macos .NET (5 - vs2019, 6 - vs2022, 7, 8 - windows, linux, macos

vscode visual studio 2022

windows

UI + Business Logic + Data Access => Database

Kpay

Mobile No => Transfer

Mobile No Check 10000

SLH => Collin

10000 => 0

-5000 => +5000

Bank + 5000

SLHDotNetCore

```sql
SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]

GO

select * from Tbl_Blog where DeleteFlag = 0

update Tbl_Blog set BlogTitle = 'heehee2' where BlogId = 1
update Tbl_Blog set DeleteFlag = 1 where BlogId = 2

delete from Tbl_Blog where BlogId = 1





-- Product Apple 1000, Orange 1000
-- Staff Apple 2, Orange 1
-- 3000, 2000, 1000
```

Used Command in database first
```bash
dotnet ef dbcontext scaffold "Server=DESKTOP-KPCHONN\SQLEXPRESS;Database=DotNetTrainning;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c EfCoreDbContext -t Tbl_Blog -f
```

## Mini KPay API Base Response Model

> https://github.com/Aung-myat-min/AMMDotNetCoreTrainning/tree/86b31fe0b1101600f0382010db65acdc02150cd2

DotNet Trainning Batch 5 By Ko Sann Lynn Htun
