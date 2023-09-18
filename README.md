
The crate db query is in Todo.API
but heres the query also

CREATE DATABASE TodoDb;

Use TodoDb;

CREATE TABLE TodoItems
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255),
    IsCompleted BIT
);
