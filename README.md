# SpecFlow Selenium C#

This repository contains an automated testing framework designed for validating the UI and workflows of the [SauceDemo](https://www.saucedemo.com/) website. It leverages **C#**, **SpecFlow** for Behavior-Driven Development (BDD), and **Selenium WebDriver** for browser automation.

---

## 🚀 Key Features

*   **BDD Approach:** Scenarios written in human-readable Gherkin syntax (`.feature` files).
*   **Selenium WebDriver:** Robust browser interaction and element management.
*   **Hooks Setup:** Automated browser initialization and cleanup (`Hooks.cs`).
*   **Resource Management:** Externalized test data handling via XML configuration (`Resource.xml`, `ResourceManager.s`).
*   **Modular Architecture:** Clean separation of step definitions, helpers, and supporting configurations.

---

## 📂 Project Structure

```text
Specflow_Selenium_CSharp-main/
│
├── Specflow_Selenium_CSharp09112023.sln   # Visual Studio Solution File
└── SauceDemo/                             # Main Test Project Directory
    ├── SauceDemo.csproj                   # Project Dependencies & Configuration
    │
    ├── Features/                          
    │   ├── SauceDemo.feature              # Gherkin Scenario Definitions
    │   └── SauceDemo.feature.cs           # Auto-generated SpecFlow Code Behind
    │
    ├── StepDefinitions/                  
    │   ├── Hooks.cs                       # Before/After Scenario Hooks (Driver Init/Teardown)
    │   └── SauceDemoStepDefinitions.cs    # Step Implementations linking Gherkin to C#
    │
    ├── Support/                           
    │   ├── Helper.cs                      # Interaction utilities (waits, clicks, typing)
    │   ├── ResourceManager.cs             # XML Parsing & Data handling
    │   └── TestObject.cs                  # Shared State / Context Injection
    │
    └── Resource.xml                       # Test Data & Configuration Properties
```
---    
## 🛠️ Setup and Execution
Follow these steps to get the environment ready and run the test suite:

Prerequisites
Install the Google Chrome browser.

Download and install the .NET 7 SDK from Microsoft's Official Download Page.

Open your terminal or Command Prompt (CMD) in the project root folder (the directory containing the .sln file).

Run the following commands in sequence:

### Clean the solution artifacts
dotnet clean

### Build the project while suppressing warnings
dotnet build --property WarningLevel=0

### Execute only the regression test cases
dotnet test --filter TestCategory=regression

-------------------------------------------------------------------------------------------------------------------

<img width="1526" height="704" alt="Gemini" src="https://github.com/user-attachments/assets/faebe402-7378-4790-87dd-165b1f64862b" />

