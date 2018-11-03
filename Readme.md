# Lab19: Create an API

## Overview

An API in .NET Core that conducts the standard HTTP verbs (Get,Put,Post,Delete) for a “To-Do” list. The “To-Do” list consists of individual tasks that can be saved into the database and extracted as needed.

## Spec

- 2 Controllers with CRUD endpoints (You may use an empty API controller template for this if you wish)
- 2 Models (ToDo and ToDoList)
- 2 database tables (one database table for each model)

The following actions are available:

- when accessing the Get action on '\api\ToDo', it should output all the individual ToDos
- when accessing the Get action on '\api\ToDo\{id}', it should output the details of the individual tToDo AND the ToDoList it is a part of.
- when accessing the Get action on '\api\ToDoList', it should output all the ToDoLists
- when accessing the Get action on '\api\ToDoList\{id}', it should output the individual ToDo list AND the individual tasks associated with it
- If you choose to delete a ToDoList, it should delete the list AND all of the associated ToDos associated

Tests

- Create a ToDO item
- Read a TODO Item
- Update a ToDo item
- Delete a ToDo Item
- Create a List
- Read a List
- Update a List
- Delete a list
- Add Items to a List
- Remove items from a list

## Getting started

* Clone the repo;
* Build and run;
* Open REST client (ex. Postman) and check endpoints

## Screenshots

![image](https://raw.githubusercontent.com/al1s/Lab19-Create-an-API/master/screenshot1.PNG)
![image](https://raw.githubusercontent.com/al1s/Lab19-Create-an-API/master/screenshot2.PNG)

