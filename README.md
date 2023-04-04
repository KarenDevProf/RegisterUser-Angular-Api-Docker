# Register User

Docker is used in the project.

Run the project in Docker.

Download Repo and run RegisterUser-API.sln file ..  UI and BackEnd in this solution

# Method #1 for run application

1) Set startup project docker-compose , and run it 
2) After opening the browser, in the address bar type  http://localhost:8011/  - UI starts at this address
 
# Method #2 for run application
 
1) Download Docker Desktop application  https://docs.docker.com/desktop/install/windows-install/

2) Open the Developer Powershell tab at the bottom of Visual Studio.. Enter the command
 
 for debug
"docker-compose build"


for run in Docker Desktop application 
"docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d"


You will see instance , backend , frontend , and mssql 
for example ..
![image](https://user-images.githubusercontent.com/46989769/229551315-5437592c-495d-41f1-97e9-59c57057d27b.png)


after run , you can open ui project in browser

![image](https://user-images.githubusercontent.com/46989769/229552051-55cc319a-e92f-4063-a1c7-00264cb0cc2e.png)

---------------------------------------------------------------

# You can also run the project without using Docker

1) Set startup project RegisterUser.API project

2) Run RegisterUser.API

3) Open UserDetailsUI  project in Visual Studio Code
 
4) In file userdetails/src/app/config.ts
   change apiUrl , set https://localhost:5001 (url backend address RegisterUser.API  project)
   
 The database is created in your local MSSQL
