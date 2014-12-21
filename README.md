Game Idea Generator
===================

Randomly generates an idea for a video game project. The results are generated using the contents of the Attributes.xml. Nodes can be added to the xml to increase the diversity of amount. However, new node "types" being declared won't do anything without a code change.

Creates the sentences in the following format:  
- Create a(n) {genre} {genre} game that utilizes {perk}, {perk}, and {perk}. 
- This game has a(n) {setting} setting with a {style} art style.
- Try using {engine} to build this game, which can be found at {engine.url}. {engine} uses {engine.language} as its language.

Example: 
- Create a role playing horror game that utilizes character customization, destructible environments, and turn-based gameplay.
- This game has an underwater setting with a gothic art style.
- Need an engine? Try using Unity to build this game, which can be found at http://unity3d.com. Unity uses C# / JavaScript as its language.
