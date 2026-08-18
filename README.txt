🧩 Logic Gate Builder
An interactive desktop application for Boolean algebra and logic gate circuitry.

📌 Overview
Logic Gate Builder is a high-level educational and engineering tool designed to help students master Boolean algebra and logic circuitry. Unlike basic simulators, it implements degree-level algorithms to ensure 100% accurate logic simplification and robust circuit validation, making it an ideal bridge between textbook theory and practical application.

✨ Key Features
Dual-Mode Experience: Toggle between an Education Mode (notes, video walkthroughs, and mock tests) and a Free-build Mode (professional sandbox).

Custom Gate Creation: Encapsulate large, complete circuits into a single custom component to aid in the decomposition of complex systems.

Engineering Suite: Advanced tools including a Circuit Cost Calculator and a Format Modifier that converts standard circuits into NAND-only or NOR-only representations.

Interactive Testing: A built-in mock exam generator that provides customizable practice questions based on past paper specifications.

Logic Gate Locator: A powerful search tool (CTRL+F) to instantly find and pan to specific components in massive circuits.

🧮 Logic & Algorithms
The core functionality is powered by several complex computer science algorithms:
Quine-McCluskey Algorithm: Used for 100% accurate Boolean simplification, identifying essential prime implicants for any valid circuit.

Khan’s Algorithm: Performs topological sorting on Directed Acyclic Graphs (DAGs) to manage correct component execution order and file saving.

Breadth-First Search (BFS): Utilized for connectivity validation, ensuring the circuit is fully connected before processing.

Sum of Products (SOP): Recursively generates unsimplified Boolean expressions from current circuit states.

🛠️ Technical Architecture
Hand-Built Data Structures: To demonstrate low-level memory management, the project utilizes custom-coded Abstract Data Types (List, Stack, and Queue) rather than standard library collections.

Command Pattern: A robust Undo/Redo system is implemented using a stack-based architecture to track and reverse user actions.

OOP Principles: Features a strict interface-based design (IGate) and complex inheritance hierarchies to manage components like switches, lamps, and custom gates.

Multi-Format Serialization: Supports saving and exporting data in .txt, .json, .xml, and .csv formats for maximum interoperability.

🚀 Getting Started
Prerequisites
Operating System: Windows (Required for WinForms).
IDE: Visual Studio 2022 (recommended).
Framework: .NET Framework 4.8 or .NET SDK 6.0+.
Dependencies: Newtonsoft.Json (installed via NuGet).

Installation
Clone the repository:
Restore dependencies:
Build the application:
Run the application: Ensure the R/ resource folder remains in the same directory as the executable to load necessary UI assets and educational videos.

⌨️ Shortcuts & Controls
The application utilizes industry-standard hotkeys for a fluid user experience:

CTRL + S: Save logic circuit.

CTRL + O: Open existing circuit.

CTRL + R / F5: Run circuit execution.

CTRL + F: Open logic gate locator.

CTRL + Z / CTRL + SHIFT + Z: Undo/Redo.

CTRL + X / C / V: Cut, Copy, and Paste.

DEL: Delete selected component.

👥 Authors
Oskar — Lead Developer — Project Documentation.