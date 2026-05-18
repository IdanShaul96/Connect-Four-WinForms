# Connect Four (4 in a Row) - WinForms Game

A classic Connect Four board game implemented in **C#** using **Windows Forms (WinForms)**, following clean object-oriented programming (OOP) principles and a clear separation between game logic and UI.

## Features
* **Game Modes:** Support for both 2-Player local mode and Single-Player mode against a computer component.
* **Dynamic Board Size:** Fully configurable board grid sizes ranging from 4x4 up to 10x10, customizable via the settings menu.
* **Robust Game Logic:** Efficient algorithms for checking horizontal, vertical, and diagonal win conditions, as well as tie detection.
* **Score Tracking:** Keeps track of wins and continuous rounds until players decide to quit.
* **Input Validation & Safety:** Prevents illegal moves (e.g., dropping a disc into a full column) and deactivates full columns dynamically.

## Architecture & Design
The project is structured with a strict separation of concerns:
* `BoardLogic.cs`: Manages the underlying matrix grid, turn states, win condition algorithms, and basic computer move generation.
* `GameInterfaceForm.cs` & `GameSettingForm.cs`: Handle the UI presentation, dynamic button generation based on grid dimensions, and user interactions.
* `GameManager.cs`: Acts as the controller coordinating data flow and actions between the logic engine and the UI layer.

## How to Run
1. Clone the repository.
2. Open `ConnectFour.sln` in Visual Studio.
3. Press **Start** (F5) to build and run the application.
