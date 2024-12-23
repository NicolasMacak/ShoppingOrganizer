## Overview
This mobile application is designed to streamline the process of generating grocery lists. Its key feature is the ability to create reusable templates (Recipes) for items you frequently purchase, complete with their components (Ingredients or anothe Recipe). 
These templates can be easily reused and customized for future shopping trips.

### Example
Suppose you plan to shop for the next three weeks and anticipate having risotto at least six times. You can define a "risotto" recipe with all its necessary ingredients and specify in the app that you need ingredients for six servings. \
The app will automatically calculate the quantities needed for your grocery list, eliminating manual calculations. Moreover, on subsequent trips, you won’t need to recall the ingredients for risotto; simply reuse your predefined template.

## Technology stack
- .NET MAUI: Enables crossplatform mobile developement
- SQLite-net: A lightweight database framework for .NET applications

## Planned vs implemented features
[Progression (Github Projets)](https://github.com/users/NicolasMacak/projects/2/views/1)

:heavy_check_mark: Design objects and their relations [Database design](https://ibb.co/jwQ3Yjt) <br>
:heavy_check_mark: Integrate AutoMapper instead of mapping through explicit operators <br>
:heavy_check_mark: Attaching recipes and ingredients to the main recipe <br>
🔨 Searchbar integration and filtering <br>
:large_blue_diamond: Variants for components of an item (Backlog) <br>
:large_blue_diamond: Linking ingredients to specific stores where they can be purchased (Backlog) <br>
:large_blue_diamond: Generating grocery lists based on selected recipes and quantities (Backlog) <br>
:large_blue_diamond: Converting grocery lists into checklists for shopping trips (Backlog) <br>
