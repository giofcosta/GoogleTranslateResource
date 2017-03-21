# GoogleTranslateResource
GoogleTranslateResource is an App to translate your ASP.Net resources files using Google Cloud Platform Translate API.

## About Google API

To use this app is necessary to make a Google Cloud Api Account, you can get this in https://console.developers.google.com/.
After create your account and setup a billing account, you can use the application.

## Arguments

The app has the follow required arguments:

| Args         | Args(=)   | Description |
| -------------| :------   | :----------- |
| -s           |--source=  | The full path of the source file       |
| -t           |--target=  | The target language       |
| -k           |--apikey=  | The key from your Google API account      |
| -n           |--appname= | The application name from your Google API account      |

## Usage

GoogleTranslateResource.exe -s **_.resx file path with file name_** -t **_locale_** -k **_APIKey_** -n **_APPName_**

