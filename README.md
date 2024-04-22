# Desbravador
Job opportunity test project.

## Atlas Project Manager
The project is an ASP.NET Core application that allows for the management of construction projects, including their status, risk level, and the association of random employees. Users can perform CRUD operations on projects, specifying details such as name, description, start and end dates, status, and responsible party. Random employees are fetched from a public API and can be linked to projects. The application features a responsive and user-friendly interface, ensuring a pleasant user experience.

## Environment Setup
Make sure you have the following requirements installed on your machine:

- Node.js (v14.x or higher)
- npm (usually installed along with Node.js)
- .NET SDK (v5.x or higher)
- A PostgreSQL database (v12.x or higher)
- How to Start the .NET API

### Clone the repository
`git clone https://github.com/gsweissheimer/desbravador.git`

## Navigate to the API directory
`cd v1`

### Install project dependencies:
`dotnet restore`

### Run database migrations to create the necessary tables:
`dotnet ef database update`

### Start the API:
`dotnet run`

##### The API will be available at http://localhost:5130.

## How to Start the React Application

### Navigate to the React application directory
`cd react-app-directory-name`

### Install project dependencies
`npm install`

### Start the React application
`npm start`

##### The application will be available at http://localhost:3000.



