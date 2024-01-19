
<a name="readme-top"></a>


![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/f962930e-6661-4608-b5fa-97cd16352968)

<br />
<div align="center">


  <h3 align="center">Game Hub</h3>

  <p align="center">
    <a href="https://aesthetic-manatee-2ad4b8.netlify.app/">View Demo</a>
    <br/>
    <b>Don't forget to run the backend app first</b>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Screenshots</a></li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This is Game Hub a website where you could search for video games, view details, add to favorites... 
<br/>I created this website in React.ts only the first time then I created the backend using ASP.NET 8 to make a fullstack application, the backend and frontend are separated in different repositories, see the frontend repository [here](https://github.com/skillmaker-dev/game-hub)
<br/>The app uses [RAWG](https://rawg.io/apidocs) API to get data about the games. 
<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

This section should list any major frameworks/libraries used to bootstrap your project. Leave any add-ons/plugins for the acknowledgements section. Here are a few examples.
* ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
* ![React](https://img.shields.io/badge/React-61DAFB.svg?style=for-the-badge&logo=React&logoColor=black)
* ![TypeScript](https://img.shields.io/badge/typescript-%23007ACC.svg?style=for-the-badge&logo=typescript&logoColor=white)
* ![Chakra](https://img.shields.io/badge/chakra-%234ED1C5.svg?style=for-the-badge&logo=chakraui&logoColor=white)
* ![React Query](https://img.shields.io/badge/-React%20Query-FF4154?style=for-the-badge&logo=react%20query&logoColor=white)
* ![React Router](https://img.shields.io/badge/React_Router-CA4245?style=for-the-badge&logo=react-router&logoColor=white)
* ![Vite](https://img.shields.io/badge/vite-%23646CFF.svg?style=for-the-badge&logo=vite&logoColor=white)
* ![Netlify](https://img.shields.io/badge/netlify-%23000000.svg?style=for-the-badge&logo=netlify&logoColor=#00C7B7)
* ![Docker](https://img.shields.io/badge/Docker-2496ED.svg?style=for-the-badge&logo=Docker&logoColor=white)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started
**For the React app you can start it using the following commands**:
```sh
npm install
npm run dev
```
To get the app up and running, follow the steps below:

* First, generate a valid dev certificate in your local machine using the following commands:

  - on Windows:
  ```sh
  dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\gamehub.pfx -p gamehub123
  dotnet dev-certs https --trust
  ```
  >**Note: When using PowerShell, replace `%USERPROFILE%` with `$env:USERPROFILE`.**
  - on Mac:
   ```sh
  dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p gamehub123
  dotnet dev-certs https --trust
  ```
* Then run docker compose file using the following command:
  ```sh
  docker-compose up
  ```
  >**Note: run the command on the root folder of the project**

  - You can now use swagger UI on the following link: https://localhost:7023/swagger/index.html
  - Visit this link then to view a live demo of the frontend app: https://aesthetic-manatee-2ad4b8.netlify.app/
* When using the app, you might want to create an account using the frontend app, in that case a confirmation email will be sent to you, you can view the emails on the local smtp server using the following link: http://localhost:3000/
* If you want to login using Swagger UI, don't forget to set the useCookies query parameter to true
## Prerequisites
- You will need the following things before being able to work with the project if you aren't running the app via docker:
  * .NET 8
  * smtp4dev
- You will need the following things before being able to work with the react project:
  * Node.js
  * Vite

For the frontend, you can visit the [hosted app](https://aesthetic-manatee-2ad4b8.netlify.app/) on netlify or you can dive inside the project in this [repository](https://github.com/skillmaker-dev/game-hub)

## Screenshots
![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/bb5e6dd8-6e6c-4438-8e60-90b7a683109b)
![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/f81263f3-307f-4a0c-99de-71e16562c4d3)
![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/0adb534a-9dd9-4946-a518-f51c6f84ef13)
![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/1f665550-350a-4a4e-aafc-a8e55cda5f40)
![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/9c185dd3-a988-48f9-88d9-c946f7ef6cff)
![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/3d043fb0-2aa0-43e0-a1ef-1cecb7c296c8)
![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/ef43a488-1005-4a1a-b2e3-face9021bee4)
![image](https://github.com/skillmaker-dev/GameHubBackend/assets/64654197/087ddece-fc0d-465c-9116-312c919dd10a)




<!-- CONTACT -->
## Contact

* You can visit my website and send me messages via the contact form: [Website](https://anaschahid.work/)
* Visit my linkedin profile: [Linkedin](https://www.linkedin.com/in/anas-chahid/)
<p align="right">(<a href="#readme-top">back to top</a>)</p>




<p align="right">(<a href="#readme-top">back to top</a>)</p>
