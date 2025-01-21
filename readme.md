### Running the application

1) Install Docker Desktop.
2) Open a command prompt and navigate to the root project folder
3) Create an .env file with the following content and update the values accordingly.
   ```.env
   SA_PASSWORD=<Password>
   ```
4) Make the necessary port modification in "docker-compose.override.yml" 
5) Run the following command
   ```cmd
   docker-compose up --build -d
   ```
5) Browse to http://localhost:{port} to access the react app. The default port is 80.

