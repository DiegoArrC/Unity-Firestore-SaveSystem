# Overview

What I have built is a cookie-clicker-esque microgame in Unity that works with Google Firebase’s Firestore to simulate a save system, where a player can create, update and load data from the cloud database as they put entries and button submissions. There are four main interactable components: The username entry field, the load button, the save button, and the point increment button. Respectively, the entry field allows a user to enter a username, the load button will load data respective to the username in the username field, and the save button will the game state data to the database, either updating the existing user specified or creating a new user if the associated name doesn’t currently exist. The update button will take the current user’s score and increment it by one, send that update to the database, and reflect that change locally in near real-time.

My purpose for developing this microgame stems from two distinct objectives: learning how to use a cloud-based database, and interact with and integrate a cloud database into a game to establish a foundation in understanding how games

[Software Demo Video]( https://youtu.be/USCBA1pHWyk)

# Cloud Database
I am using Google Firebase’s Firestore. This is a NoSQL database that uses collections and documents as its structure, and is maintained online by Google itself.

The database that I created uses a straightforward structure, with a singular collection of users, where each user document, which is given an id that is the same as the username provided in the document fields. The document structure contains four basic fields: current score, previous score, player level, and the player’s username.

# Development Environment


{Describe the tools that you used to develop the software}
Google Firebase
Google Firestore
Unity (Editor version 2021.3.11f1)
Firebase Unity SDK 9.5.0
Visual Studio Code 1.74.0
Unity UI Library
C#
Dotnet 4.0
Firebase Extensions library
Firebase Firestore library

# Useful Websites

{Make a list of websites that you found helpful in this project}
* [Firestore Documentation]( https://firebase.google.com/docs/firestore)
* [Firebase Firestore in Unity Video]( https://youtu.be/b5h1bVGhuRk)

# Future Work

* Implement a custom security rule system for reading and writing data
* Add an authentication system to the game
* Fix any issues with building app to Android environment
* Set up individual user systems, rather than an open pool of player d
