# _Chirp!_
### Overview

_Chirp!_ is a project which allows users to interact with each other through posting cheeps, following and being followed by users 
similar to a social media platform. This project is part of the course: Analasys, Design and Software Architecture(BDSA) and is created by students studying the course at IT University of Copenhagen. 

---------------------------------------------------------
#### How to run the program

Follow this step-by-step guide on how to run our program.

The first 3 steps will be pre-requirements to running the program.

##### Step 1. Clone the repository
First you need to clone or open the repository in your preferred code editor.

_Note: Make sure you are on the main branch_.

##### Step 2. Download .NET 9.0 SDK

If you currently do not have .NET 9.0 SDK installed, please refer to https://dotnet.microsoft.com/en-us/download/dotnet/9.0 and select the installer for your OS.

##### Step 3. Setting user-secrets
In order to enable GitHub authentication and authorisation for _Chirp!_, you must to set GitHub OAuth user-secrets locally on your computer. Without the secrets the app will not function so it is important to follow these steps:
1. Start off by visiting the [GitHub](https://github.com/) website.
2. Then, navigate to your profile settings by clicking your profile picture in the top right corner, and then 'Settings' in the drop-down menu.
3. In the left sidebar of the profile settings, scroll down and press 'Developer Settings'.
4. On the 'Developer Settings' page, press 'OAuth Apps' and then 'New OAuth App'.
5. Give the app a fitting name, write `https://localhost:5273/` as the Homepage URL and `https://localhost:5273/signin-github` as the Authorization callback URL, and press 'Register application'.
6. Once redirected to the application page, press 'Generate a new client secret'.
8. Finally, in a shell (fx. Command Prompt or PowerShell), navigate to the Chirp.Web project and run the commands
   `dotnet user-secrets set "authentication_github_clientId" <Client ID>` and
    `dotnet user-secrets set "authentication_github_clientSecret" <Client Secret>`,
    replacing \<Client ID\> and \<Client Secret\> with the values found on the application page.

This concludes setting user secrets. 

##### Step 4. Run the program
Open a terminal window found in your code editor.

Navigate to the `Chirp.Web` folder.
If you are currently in the `Chirp` folder, you can run the following command to navigate to the `Chirp.Web` folder:
> cd .\src\Chirp.Web\ 

Enter the following command:
> dotnet run

Proceed by clicking on the link or copy-pasting the link into your webbrowser.
##### Step 5.
The _Chirp!_ application can be viewed and interacted with in two different states.

We will go through both states in the proceeding steps:
- Step 5.1 (signed out state)
- Step 5.2 (signed in state)

##### Step 5.1 Viewing and interacting with the the _Chirp!_ application without being signed in as a user.

You should now be able to see the _Chirp!_ application frontpage being the public timeline. Here all cheeps will be visible for visitors of the public timeline. Interacting by clicking on the username of one of the cheeps, you will be redirected to that user's timeline.

Besides viewing timelines you have access to two other tabs:
- Register (register as a user)
- Login (login as a user)

##### Step 5.2 Viewing and interacting with the the _Chirp!_ application being signed in as a user.

You will now have gained access to the following new features:
- Creating cheeps
- Following and being followed by other users
- Viewing and deletion of account data

And access to the following new sections:
- My timeline
- About me (information regarding your user)
    - Shows user information, your posted cheeps, followers and followed users.
    - "Forget Me!"-button for deletion of account and date.
- Account settings (edit account settings)
    - Update user- and login-information.
    - View or set external links and two-factor authentication



When you are done interacting with the _Chirp!_ application, proceed to the next step.

##### Step 6. Terminating the _Chirp!_ application.

Close _Chirp!_ by clicking (CTRL + C) on your keyboard while your code editor is the current application active on your screen.

If the above solution does not work for you. Instead, close the terminal tab currently running on your code editor.
