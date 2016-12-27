# Enums from Excel

It is simple application for "scaffolding" enums and associated files from excel for code first approach. Things it will generate: 

  - Enum files
  - List of enum properties to use in model
  - Database (enum) model files which is basically database table keeping specific enum
  - List of database (enum) model properties to use in model
  - DbSets to use in DbContext
 
It was created to fit my needs but I decided to share it in hope that someone will find it useful, enjoy ;)

## Sample Files

I suggest trying to use sample files and find out what application does. Just drop the content of SampleFiles folder (I trust you will find it) to Debug directory (one where main exe resides) and run application, after console prints "Ready" you can inspect files located in Output folder. For additional information look below.

### Input

Input file should be an excel file named "input.xlsx". Every column is another enum, first row is always title. Program supports multiple sheets.

### Base Files

DatabaseBase - is base for database enum model file, you can change everything but two things:
- FILL_IN_TITLE - which is where program puts title (more or less modified first row of excel)
- FILL_IN_ENUM_TITLE - very similar (the name of enum)

*Prefix ADD stands for Additional Database Data and is same convention that I use for that sort of files

EnumBase - similar case, we have FILL_IN_TITLE and FILL_IN_LIST where text goes, rest is up to you.
