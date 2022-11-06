# Hello_Notepad

## Description

A script to run Notepad and start a new file.

This is an exercise to test my ability to write a script that can open Notepad and automatically run tasks by triggering mouse clicks.

## Installation

- Pull the repository
- CD into the repo with CMD or Git Bash
- Run `dotnet run`

## Requirements

- Windows
- .NET already installed

## Usage

If Notepad is not open, this script will open it.

If Notepad is already running, this script will use the running instance.

If Notepad is minimized or hidden behind another window, this script will restore it and bring it to the front.

The script will open a new file. If a dialog box opens asking the user whether they want to save their data before closing, the script will automatically select to close without saving.

The script will then type "Hello" into the text field and save it to the current location with the name "Hello_IanM.txt".

Finally, the script will close Notepad.

## Additional Notes

This version of the script goes further than the challenge requirements I was given. In the branch called `Basic`, I stop after Notepad is opened and clicks on File -> New