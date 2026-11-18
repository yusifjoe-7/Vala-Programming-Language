using SharpVala.Lexing.Tokens;
using SharpVala.Interpreter;

namespace SharpVala;

public class TheEnvironment
{
    Dictionary<string, object> values = new();

    TheEnvironment enclosing;
    
    public void Define(string key, object value)
    {
        values.Add(key, value);
    } 

    public TheEnvironment() {
        enclosing = null;
    }

    public TheEnvironment(TheEnvironment _enclosing) {
        enclosing = _enclosing;
    }


    public void Assign(Token name, object value) {
        if (values.TryGetValue(name.lexeme, out _)  ) {
        values[Convert.ToString(name.lexeme)] = value;
        return;
        }

        if (enclosing != null) {
            enclosing.Assign(name, value);
            return;
            }

        throw new RuntimeError(name,
            "Undefined variable '" + name.lexeme + "'.");
    }

    public object Get(Token name) {
    
        if (values.TryGetValue(name.lexeme, out var value))
        {
            return value;
        }

        if (enclosing != null) return enclosing.Get(name);

    throw new RuntimeError(name,
        "Undefined variable '" + name.lexeme + "'.");
  }
}
