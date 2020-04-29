# WishList
### CIDM 3312 Final Project
##### Walker Chesley 4/19/2020
---
## OVERVIEW
### 1.	Project Background and Description
This project is to be a summation of all knowledge acquired during the course of CIDM-3312 as taught at WTAMU. The Idea I have for my project is a web-app called  WishList. A tool to assist with long-term shopping on the internet. I believe this app will cover all bases covered in the course of the class as well as some beyond the scope of this course. At a high level the user will find a web page with an item they want to buy; the user saves the URL of the page as well as two items from the pages HTML: The HTML ID of the products name and the HTML ID of the products price. Using these 3 items as one entity, this object will be added to a list of other pre-existing items that will be scheduled for scraping with CRON. Daily price data will be timestamped and saved into the database on a new table.

### 2.	Project Scope
Tool is only to gather price and item name, the system will append a timestamp as to when the data was retrieved. Retrieval of data from webpages will be scheduled within the system using Cron Jobs. Pricing data can be listed as aggregated or per individual item. The prices can be sorted by lowest price or date retrieved, default sorting will be by most recent date. On the aggregated view of prices and items, the same sorting rules will apply, the items can be further filtered by means of text search.  
### 3.	High-Level Requirements
The new system must include the following:
- Your web application should have at least TWO database tables implemented as entity classes.
- Follow steps 1-8 from Week 13 to create your ASP.NET Core project with EF Core.
- Use scaffolding (for step 8) on one of your entity classes.
- Write code to display or modify related data. This can’t be scaffolded.
    - For example, if you have students and courses the course page should list the students in the course. Each student page should list the courses that student is in.
    - Another example would be to have a button or form for a student to add/drop a course.
- Seed the database with records for all your entities. Ensure that one entity has enough records (at least 25) to support paging.
- Add Data Validation to all the necessary properties.
- Add paging support to a razor page. For example, list only ten records at a time and allow the user to navigate to the next and previous pages. Disable the next/prev buttons as appropriate.
- Add a search bar to at least one razor page. Allow the user to type in a search string and show only those results.
- Allow the user to sort on at least one record both ascending and descending –OR –filter records using a SelectList.
- Add links to all appropriate pages in the navigation bar.
### 4.	Specific Exclusions from Scope
 	Depends on how much time I have: I would probably use some front-end JS library ie. HighCharts
~~Graphical representation of historical pricing data.~~ Done.  
~~Deploy to Webserver~~ Done: https://wishlist-bytes.herokuapp.com/
### 5.	Implementation Plan
Web Server and business logic is to be ASP.NET/C#
Data will be retrieved with Selenium and Chrome [https://www.selenium.dev/documentation/] 
Due to the nature of running Selenium, Chrome binaries will be included with the project files, allowing this to be deployed and run anywhere. Fresh binaries can be found [here](https://chromedriver.storage.googleapis.com/index.html).
Naturally the ERD is subject to change during the implementation process. 
### 6.	High-Level Timeline/Schedule
Proposal of Project Due by: 4/18/2020
Final Project Due by: 5/5/2020
### 7.	 Entity Diagram
 
