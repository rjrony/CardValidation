# CardValidation
It's test project for credit card validation.

#	Technology Highlights:
Language: C#

Framework & Library: Asp .Net Web Api, OWIN, AutoMapper

ORM: EntityFramework Code First

Database: Microsoft SQL Server

Logging: NLog

API Versioning: Microsoft.AspNet.WebApi.Versioning

API Documentation(Swagger): Swashbuckle 

Dependency Injection: Unity

API Validation: SpecExpress

Unit Test: Microsoft.VisualStudio.TestTools.UnitTesting

ORM mocking: Effort


How to run:
1.	Change connection string from “Web.config” file 
  a.	No need to run any db script 
  b.	It’s code first project after building solution just hit any endpoint that will create database automatically 
  c.	Has some default data that will also insert in database creating time.
2.	Build the solution
  a.	That will create app in IIS named “CardValidation”
  b.	Then hit endpoint http://localhost/CardValidation/docs/index#!
3.	If you don’t have IIS or want to run in any port without deploying in IIS, then
  a.	Change the project setting from “Local IIS” to “”IIS Express”
  b.	That will run the project in http://localhost:60889 
  c.	Then hit endpoint http://localhost:60889/docs/index#!
4.	Now call “GET /api/Card/ValidateCard” endpoint with parameters:
  a.	cardNumber: “4412345678901234”
  b.	expiryDate “012020”

