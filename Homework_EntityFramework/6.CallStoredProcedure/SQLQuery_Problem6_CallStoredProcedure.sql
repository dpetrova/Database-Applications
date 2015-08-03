CREATE PROC usp_get_projects_by_employee(@firstName NVARCHAR(50), @lastName NVARCHAR(50))
AS
SELECT p.Name, p.Description, p.StartDate
FROM Employees e
JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects p ON p.ProjectID = ep.ProjectID
WHERE e.FirstName = @firstName AND e.LastName = @lastName
GO