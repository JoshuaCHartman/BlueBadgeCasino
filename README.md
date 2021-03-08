# BlueBadgeCasino
# Overview

An online casino model built as a .NET Framework Web API, and designed around the principles of n-tier architecture and RESTful APIs to access a code-first relational database. By implementing user roles, users can simulate interacting with the casino as a player, manager, or casino owner. By incorporating a third party payment processor API (Stripe), players can use real world money to purchase in-game “chips” to wager on logic-based simulations of real world gaming. Players have access to retrieve and analyze their own gaming history and account activities, as well as request withdrawal of in-game “chips” for fiat currency. Managers can retrieve and analyze gaming and account histories for all players and games, and can manipulate player accounts to perform administrative functions. However, managers cannot participate in games as players. The casino owner can perform all functions, and is responsible for creating new manager accounts.

The casino gives a player the option to play several simulated  games. Initially, a new user may create a player account, make a deposit through Stripe, then choose from a limited number of general admission games to place a bet. If the player wins, the funds are immediately added to their chip balance. Otherwise, their account is decreased by the bet amount. After placing enough bets or wagering above a certain threshold, the player may increase their status to a higher tier, and access the SilverLevel or HighRoller game with altered parameters and greater risk/reward mechanics.

Utilizing user roles, a player has the ability see their transaction history, bet history, and profile. An admin has the ability to generate reports for the individual games showing the outcomes for each game by type, timespan, etc. The admin can also see reports for each player, all players, all bets ever, all bets in a day, etc. This allows him to analyze the data to make decisions or changes for the casino. The Superadmin role is unlimited in functionality.  Additionally, player 1 (seeded) represents the house and the house vault, filling the role of the casino chip bank secured by cash. Players who experience losses over the course of their gaming will have their wagers removed from their personal accounts, and deposited into the house account. 

## User Story

As an adjunct to planning and wire-framing, a user story was created to guide the development of the casino. The project participants imagined themselves as a small software development firm which had been approached by a tech-minded entrepreneurial client.

The client, Samson, is an entrepreneur behind several tech-oriented startup projects including a rideshare app, a lead generator program for small service business, and an online auto parts fulfillment service. He is currently partnering with a small group of investors on two additional projects: food delivery and cryptocurrency apps. Samson pays attention to trends with an eye to creating new avenues to generate income. With a rapidly changing legislative environment opening the way for increased avenues for online gaming, a wholly-online casino is a potential next project. He intends to eventually build a player following by providing an ad-free, “play money” version, and after its success, and the satisfaction of regulatory requirements, he will go live and start taking actual bets for real money. Samson approached the developers with his requirements. He wants to keep the initial project simple, with limited games and low interactivity. He wants the initial working project to only simulate the casino experience; he wants to be able to do extensive testing of processes, structures, odds, and security mechanisms to better understand his needs before commissioning a final site. 

## Technologies Employed

- Visual Studio Community 2019
  - Web API .NET Framework 
  - Individual Authentication
  - Identity Framework 
  - User Roles / Authorization
  - Entity Framework 
- Database Mapping using Code-First Approach
- CSharp - Utilized for Logic throughout Application
- Linq - Found in Service Layer to Query the SQL Database
- SMTP - automated email sender with queried details concerning users
- Seeded data encompassing all roles, and of sufficient richness to adequately utilize all business logic and architecture layers
- ASP.NET MVC - display a simple credit card capture form to work with a deprecated Stripe charge API
- Postman - Utilized to test endpoints
- Stripe - third party API integration using Stripe.net official Nu.Get package
- Stripe dashboard - confirm transactions externally, manage API keys
- Mailtrap.io - provides secure environment to simulate dedicated SMTP server & account credentials
- 



## Installation and Setup (Windows)

Requirements: Visual Studio, Postman or equivalent testing application, web browser

Create a new folder where you would like to contain the casino project and associated files. Open the empty folder and in the top search bar, type cmd to open a terminal window. From the command prompt, clone the git repository by entering the following: 

```bash
git clone https://https://github.com/JoshuaCHartman/BlueBadgeCasino
```

Open the project by clicking on Casino.sln inside the Casino folder. Navigate to the Package Manager Console, and enter the following :

```bash
Update-database
```

This will seed the database with user accounts for testing, and an extensive history of players, bets, transactions, and games to test endpoints.

[Section to be added after assignment graded to include instructions for setting up Stripe and Mailtrap.io accounts, and filing in keys & credentials in the appropriate parts of the program. These are included for Eleven Fifty evaluation purposes]

Click on the green play button at the top of Visual Studio to run the application. The application homepage will appear inside a new browser window. Navigate to CasinoAPI for a list of endpoints. 


## Instructions

**Creating a User / Obtaining a Token**

**If you would like to use a seeded account, login details are included here. Please skip to the section detailing the process to get a token to use with these accounts.**

> **SuperAdmin**
> >Username = HouseAccount
> >
> >Email = house@casino.com
> >
> >Pwd = Test1!

> **Admin**
> >Username = FirstAdmin
> >
> >Email = firstAdmin@casino.com
> >
> >Pwd = Test1!

> **User with associated Player Account**
> >Username = user2
> >
> >Email = user2@abc.com
> >
> >Pwd = Test1!

**Creating a New User**

Create a **new user** with the following steps (skip if using a seeded account):
1. Navigate to the CasinoAPI endpoint lists through the navigation bar at the top of the page.
2. Copy the URL from the opened Casino webpage, and paste it into the URL bar of a new Postman tab. It should look similar to, the following, but your localhost number will be different :

```bash
https://localhost:44367/
```

3. Navigate to the Account section, and click on the link for 
```bash
POST api/Account/Register_New_Account
```

4. Copy the following from the title of the page, and add it to your existing URL in Postman. It should look similar to :
```bash
https://localhost:44367/api/Account/Register_New_Account
```

5. In Postman, change the dropdown to the left of the url bar to POST.
Click on the Body tab, and then the radio button for
```bash
x-www-form-urlencoded
``` 

6. In the body, add the following parameters under **KEY / VALUE** :

- Username /  any email address adhering to standard email conventions
- Password /  for testing, we recommend Test
- Confirm-Password /  the same entry as above

7. Hit send. You should receive a *200OK* and an acknowledgment message if all went well.

**Get a token (start with this section if using a seeded account):**

1. Copy the following from the title of the page, and paste it into your existing URL in Postman. It should look similar to the following, but your localhost number may be different :
```bash
https://localhost:44367/token
``` 
2. In the body, add the following parameters under **KEY / VALUE** :
- Grant_type / password
- Username / the email you used above, or a seeded account’s username
- Password / the password you used above, or a seeded account’s pwd

3. Use the drop down box on the left to select **POST**. Hit send. A **token** should be returned in the body of the response. Highlight and copy the contents of the token (everything between quotation marks).

**Using the token:**
1. Under the Headers tab, add the following parameters under **KEY / VALUE** :
- Content-type / application/x-www-form-urlencoded
- Authorization / type Bearer then paste the token here
2. You can now make a **Player**.


**Creating a Player**
1. Navigate to the Player  section, and click on the link for 
```bash
POST api/makePlayer
``` 
2. Copy the following from the title of the page, and paste it into your existing URL in Postman. It should look similar to :
```bash
https://localhost:44367/api/makePlayer
``` 

3. In Postman, change the dropdown to the left of the URL bar to **POST**.
4. In the body, include the following **KEY / VALUE** pairs, as described on the page (do not include quotation marks):
  - "PlayerFirstName": "any value"
  - "PlayerLastName": "any value"
  - "PlayerPhone": "any value"
  - "PlayerEmail": "any email"
  - "PlayerAddress": "any value"
  - "PlayerState": Standard two letter abbreviation	
  - “PlayerZipCode”: any value
  - "PlayerDob": "Format of MMDDYYYY"
5. Hit send, and you should receive a **200OK** with a confirmation message. The final step is to add money to your Player’s in-house “bank” by charging a card through Stripe’s external API.

**Adding Money to Your Player Account**
1. Navigate to the Player  section, and click on the link for 
```bash
POST charge_deposit_as_chips
``` 

2. Copy the following from the title of the page, and paste it into your existing URL in Postman. It should look similar to :
```bash
https://localhost:44367/charge_deposit_as_chips
``` 
3. In Postman, change the dropdown to the left of the URL bar to **POST**.
4. In the body, include the following **KEY / VALUE** pairs, as described on the page (do not include quotation marks) :
   
  - "CardNumber": "use a test card number provided by stripe such as: **4242424242424242**"
  - "Month": any month in a 2 digit format, example : 02
  - "Year": any future year in a 4 digit format, example: 2023
  - "Cvc": "3 digits"
  - "Zip": "5 digits"
  - "Value": a value in dollars and cents without a decimal point (example: $100 is entered as 10000)

5. Hit send, and you should receive a **200OK** with a confirmation message.


At this point, you can explore various endpoints as a **Player** to play games and analyze the player’s account. You can explore more detailed Player histories by using a seeded Player account and obtaining a token from it as described above. You can also explore endpoints limited to **Admin** and **SuperAdmin** accounts by using the respective seeded accounts, and obtaining a token for those accounts as listed above. 

## Features
- A unique GUID is generated for each user, and is assigned to their corresponding Player account. This GUID follows Players throughout all of their transactions.
- Players can play games and review their account and betting histories, including histories of charges made and amounts deposited/withdrawn from their chip bank. As their status increases, players have access to games with higher risks and rewards. After a certain period of time of no activity on their account, their account is switched to inactive.
- When Players withdraw money, an email is sent to an imagined Accounts department that shows a PlayerID and the amount they wish to withdraw, and requests a bank transfer be made.
- Admins can return lists of all users, players, and admins, and for each of the tables. Admins have the ability to query very specifically for bet histories, via implementation of nested logic and lambda statements/LINQ.
- SuperAdmin has the ability to alter Player balances to account for potential issues like fraud or card misuse (subject to verification).
- Games are composed of unique nested logic simulating the probabilities and payout amounts of real world gaming. SuperAdmin, and Admin can adjust the payout amounts to a high degree, allowing maximum profits while still staying within the scope of standard payout amounts.

## Discussion

- Games
- GUIDs
- Bet class / table as the engine of the project
- Buying in-game currency and cashing out

## Resources

**User Roles / Authorization**

- [MS Documentation Role-Based Authorization in C#](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/older-versions-security/roles/role-based-authorization-cs)

- [User Roles Blog Series - note the tutorial is out of date with current versions of Visual Studio and typical packages](https://bitoftech.net/2015/01/21/asp-net-identity-2-with-asp-net-web-api-2-accounts-management/)

- [Stack Overflow discussion of Authorization and Roles](https://stackoverflow.com/questions/1407742/net-membership-in-ntier-app)

**Seeding**

- [Use Code First Migrations to Seed the Database](https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-3)

- [Stack Overflow discussion of seeding using Foreign Keys](https://stackoverflow.com/questions/20107702/entity-framework-code-first-seeding-entities-having-user-id-foreign-key)

**Try-Catch Blocks**

- [C# Exception Handling Best Practices](https://stackify.com/csharp-exception-handling-best-practices/)

- [Stack Overflow discussion of using try-catch blocks and messages](https://stackoverflow.com/questions/16145401/display-exception-on-try-catch-clause)

**Stripe**

- [Stripe official documentation](https://stripe.com/docs/api)

- [Creating a token](https://stripe.com/docs/api/tokens)

- [The Charge object and making a charge](https://stripe.com/docs/api/charges/object)

- [YouTube series on creating a charge and capture form using deprecated charge API (no long supported by Stripe)](https://www.youtube.com/watch?v=zV3_KQXwewo&ab_channel=OOPCoders)

**Miscellaneous**

- [Discussion of processing properties in setter](https://softwareengineering.stackexchange.com/questions/359832/is-it-appropriate-to-process-a-property-in-the-setter)

- [Sending emails from within a C# application](https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/)







## Team Members / Contact
[Joshua Hartman](https://github.com/JoshuaCHartman)




