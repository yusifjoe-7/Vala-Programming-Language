# Vala

**Vala** is a small, dynamically typed programming language implemented in C#.

The project focuses on building a programming language from the ground up, including lexical analysis, parsing, expression evaluation, and variable management.

Vala uses `.val` as its source file extension.

> **Project Status:** Early development. The current implementation covers the language features developed up to Chapter 8 of *Crafting Interpreters*.

## Features

Vala currently supports:

- **Dynamic Typing** — Variables can hold values of different types without explicit type declarations.
- **Data Types**
  - Numbers
  - Strings
  - Booleans (`true` and `false`)
  - `nil`
- **Arithmetic Operators** — Addition, subtraction, multiplication, and division.
- **Comparison Operators** — Greater than, less than, and their inclusive variants.
- **Equality Operators** — Equality and inequality comparisons.
- **Logical Operators** — Logical `and` and `or`.
- **Variable Declaration** — Declare variables using `let`.
- **Variable Assignment** — Update the value of previously declared variables.
- **Expression Evaluation** — Evaluate expressions with operator precedence and grouping.
- **Print Statements** — Output evaluated values.
- **Expression Statements** — Execute expressions as standalone statements.
- **Lexical Analysis** — Tokenize source code into language tokens.
- **Parsing** — Parse tokens into an Abstract Syntax Tree (AST).
- **AST Interpretation** — Execute parsed statements and evaluate expressions.

## Installation

### Requirements

- [.NET 10 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

Make sure you have .NET 10 installed before proceeding.

### 1. Clone the repository

```bash
git clone https://github.com/yusifjoe-7/Vala-Programming-Language.git
cd Vala-Programming-Language
```

### 2. Build the project

```bash
dotnet publish SharpVala/SharpVala.csproj -c Release -o ./publish
```

### 3. Install Vala

#### Linux

Copy the published executable to `/usr/local/bin`:

```bash
sudo install -Dm755 ./publish/SharpVala /usr/local/bin/ShaprVala
```

You can now run Vala from anywhere:

```bash
ShaprVala
```

#### Windows

1. Open the published `publish` directory.
2. Locate `SharpVala.exe`.
3. Add the directory containing `SharpVala.exe` to your system's `PATH` environment variable.

You can now run Vala from Command Prompt or PowerShell:

```powershell
SharpVala.exe
```

## Usage

Vala source files use the `.val` extension.

Create a file named `hello.val`:

```vala
let name = "World";
let version = 1;

print "Hello, " + name + "!";
print version + 1;
```

Run the file:

**Linux:**

```bash
ShaprVala hello.val
```

**Windows:**

```powershell
SharpVala.exe hello.val
```

## Learning Resource & Acknowledgements

This project is heavily inspired by and follows the educational approach presented in:

**[Crafting Interpreters](https://craftinginterpreters.com/)** by **Robert Nystrom**.

The book serves as the primary learning resource and architectural reference for this project. The implementation is my own work, developed while studying the concepts and building the language in C#.

One intentional syntax difference in the language is the use of `let` for variable declarations instead of `var`.

This project is an educational implementation and is not affiliated with or endorsed by the author of *Crafting Interpreters*.

## License

See the [LICENSE](LICENSE) file for licensing information.