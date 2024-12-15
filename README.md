# Apointment Scheduler
WGU Software 2 - Advanced C# - C969

--Scenario--

You are working for a software company that has been contracted to develop a scheduling desktop user interface application. The contract is with a global consulting organization that conducts business in multiple languages and has main offices in the following locations: Phoenix, Arizona; New York, New York; and London, England. The consulting organization has provided a MySQL database that your C# application must pull data from. However, this database is used for other systems, so its structure cannot be modified.

The organization has outlined specific business requirements that must be included as part of the application. From these requirements, a system analyst at your company created solution statements for you to implement when developing the application. These statements are listed in the “Requirements” section.

--Requirements--
1.   Create a login form that has the ability to do the following:

    a.   Determine a user’s location.
  
    b.   Translate login and error control messages (e.g., “The username and password do not match.”) into English and one additional language.
  
    c.   Verify the correct username and password.

2.   Provide the ability to add, update, and delete customer records.

    a.   Validate each of the following requirements for customer records:

        •    that a customer record includes name, address, and phone number fields

        •    that fields are trimmed and non-empty

        •    that the phone number field allows only digits and dashes

    b.   Add exception handling that can be used when performing each of the following operations for customer records:
    
        •    “add” operations
        
        •    “update” operations
        
        •    “delete database” operations

3.   Provide the ability to add, update, and delete appointments, capture the type of appointment, and link to a specific customer record in the database.

    a.   Validate each of the following requirements for appointments:
    
        •    Require appointments to be scheduled during the business hours of 9:00 a.m. to 5:00 p.m., Monday–Friday, eastern standard time.
        
        •    Prevent the scheduling of overlapping appointments.
    
    b.   Add exception handling that can be used when performing each of the following operations for appointments:
    
        •    “add” operations
        
        •    “update” operations
        
        •    “delete database” operations

4.   Create a calendar view feature, including the ability to view appointments on a specific day by selecting a day of the month from a calendar of the  months of the year.

5.   Provide the ability to automatically adjust appointment times based on user time zones and daylight saving time.

6.   Create a function that generates an alert whenever a user who has an appointment within 15 minutes logs in to their account.

7.   Create a function that allows users to generate the three reports listed using collection classes, incorporating a lambda expression into the code for each of the following reports:

    •    the number of appointment types by month
    
    •    the schedule for each user
    
    •    one additional report of your choice

8.   Record the timestamp and the username of each login in a text file named “Login_History.txt,” ensuring that each new record is appended to the log file.
