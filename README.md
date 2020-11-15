# 2C2P: Software Architect Technical Assignment
Implement the features that the first one use to show transaction log and the second one use for uploading file 

# Technologies
1. .Net Core 3.1  
1. Entity Framework Core 3.1  
1. MSTest  
1. Angular Framework 8  
# Architecture (3-tier)
* Presentation Tier : Use to be the front end layer and often built on web technologies such as HTML5, JavaScript, CSS or web development frameworks
  * Project that place in this tier
    * Transaction.Web  
* Application Tier : contains the functional business logic which can drive the application capabilities  
  * Project that place in this tier
    * Transaction.BusinessLogic
    * Transaction.Domain
    * Transaction.IoC
    * Transaction.Persistence
* Data Tier : Use to handle database or data storage connection
  * Project that place in this tier
    * Transaction.DataAccess
