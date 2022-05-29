# Coin Spy, What is?

It is an application created purely in .NET 5 whose objective is to analyze multiple virtual currency exchanges to collect data on coins in circulation. The bot sends a notification via email to one or more users. The notification may contain up to 2 attachments with information about new currencies added or currencies that already existed and were updated by a large increase in values ​​or a large decrease in values.

## Requirements
- .NET 5 installed on the machine will run the application. It can be downloaded at [.NET 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

- 64-bit operating system.

- Minimum indicated 4GB RAM memory

- Dual core processor or better

## Installation

### Using github
- STEP 1: Open the repository page
- STEP 2: Find the CODE button and then click on "Download ZIP", then unzip the file
- STEP 3: After downloading the file and unzipping. Go to the project folder where the CoinCheck.sln file is located
- STEP 4: Open the project with an editor of your choice and compile the file to generate the executables.

### Easy Mode
- STEP 1: Click on the following link to download the program ready to run. [DOWNLOAD](https://github.com/lucas-fsousa/Coin-Spy/raw/master/CoinCheck.MainExec/ExtraFiles/release.zip)
- STEP 2: Unzip the release file to use it

## Configuration
- STEP 1: Copy the sql script to create the database model used. [Show model.sql](https://github.com/lucas-fsousa/Coin-Spy/blob/master/CoinCheck.MainExec/ExtraFiles/modelDB.sql)
- STEP 2: Make sure you have sqlserver installed on the machine and run the script to create the bases.
- STEP 3: Go to the project folder and locate the App.Config file, if it is the version already compiled and ready to run, the file will be named CoinCheck.dll.config
- STEP 4: open the file with a text editor of your choice and change the following information as needed.

**ATTENTION - KEYS AND NAMES MUST NOT BE UPDATED. UPDATE VALUES AND CONNECTIONSTRING ONLY**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<!--CONFIG FOR LOGS-->
		<add key="logPath" value="C:\Users\Public\Documents\" />
		
		<!--CONFIG TO CLIENT EMAIL | A generic or personal email to be used as a source for notifications.-->
		<add key="CredLoginEmail" value="yourGenericEmailHere"/>
		<add key="CredPasswordEmail" value="yourPasswordhere"/>

		<!--CONFIG USERS FOR NOTIFY | Emails that will receive notifications sent by the bot.-->
        <!--mailto refers to the main ones to be notified and it needs to be filled out. 
            Copy is optional to fill and can be used to send notifications to other not so important users.-->
		<add key="mailTo" value="test01@gmail.com;test02@gemail.com"/>
		<add key="mailCopy" value="copy01@gmail.com;copy02@gmail.com;@copy03@gmail.com"/>

		<!--PERCENT TO CHANGE COINS IN THE DATABASE - DEFAULT IS 0.05
		    Represents the gross change in currency value of currencies.-->
		<add key="percentChange" value="0.05"/>
	</appSettings>
	<connectionStrings>
        <!--This item must be replaced by the connection 
            string from your local or external database, 
            otherwise it will not let the robot run.-->
		<add name="dataBaseString" connectionString="your sql connection string here"/>
	</connectionStrings>
</configuration>
```

After performing the steps above, just run the bot as you see fit (I use a windows task scheduler) that has been configured to run the bot every 6 hours. See how to configure a windows task scheduler by clicking [here](https://www.youtube.com/watch?v=DVUlkU2AxgQ)
## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)