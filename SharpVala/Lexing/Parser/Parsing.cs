using SharpVala.Lexing.Tokens;
using SharpVala.Expr;

using _expr = global::SharpVala.Expr.Expr;
using SharpVala.AStmt;

namespace SharpVala.Lexing.Parser;

public class Parsing
{

    private readonly Helpers _helper;
    private readonly ErrorHandler _error;

    public Parsing(List<Token> tokens)
    {
        _helper = new Helpers(tokens);
        _error = new ErrorHandler(_helper);

    }


    private _expr Expression() {
        return Assignment();
    }

    _expr Assignment() {
        _expr expr = Equality();

        if (_helper.Match(TokenType.EQUAL)) {
        Token equals = _helper.Previous();
        _expr value = Assignment();

        if (expr is _expr.Variable) {
            Token name = ((_expr.Variable)expr).Name;
            return new _expr.Assign(name, value);
        }

        _error.AnError(equals, "Invalid assignment target."); 
        }

        return expr;
        }

    private _expr Equality() {
        _expr expr = Comparison();

        while (_helper.Match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL)) {
        Token op = _helper.Previous();
        _expr right = Comparison();
        expr = new _expr.Binary(expr, op, right);
        }

        return expr;
    }

    private _expr Comparison() {
        _expr expr = Term();

        while (_helper.Match(TokenType.GREATER, TokenType.GREATER_EQUAL, TokenType.LESS, TokenType.LESS_EQUAL)) {
        Token op = _helper.Previous();
        _expr right = Term();
        expr = new _expr.Binary(expr, op, right);
        }

        return expr;
    }

    private _expr Term() {
        _expr expr = Factor();

        while (_helper.Match(TokenType.MINUS, TokenType.PLUS)) {
        Token op = _helper.Previous();
        _expr right = Factor();
        expr = new _expr.Binary(expr, op, right);
        }

        return expr;
    }

    private _expr Factor() {
        _expr expr = Unary();

        while (_helper.Match(TokenType.SLASH, TokenType.STAR)) {
        Token op = _helper.Previous();
        _expr right = Unary();
        expr = new _expr.Binary(expr, op, right);
        }

        return expr;
    }

    private _expr Unary() {
        if (_helper.Match(TokenType.BANG, TokenType.MINUS)) {
        Token op = _helper.Previous();
        _expr right = Unary();
        return new _expr.Unary(op, right);
        }

        return Primary();
    }

    private _expr Primary() {
    if (_helper.Match(TokenType.FALSE)) return new _expr.Literal(false);
    if (_helper.Match(TokenType.TRUE)) return new _expr.Literal(true);
    if (_helper.Match(TokenType.NIL)) return new _expr.Literal(null);

    if (_helper.Match(TokenType.NUMBER, TokenType.STRING)) {
      return new _expr.Literal(_helper.Previous().literal);
        }
        if (_helper.Match(TokenType.IDENTIFIER)) {
        return new _expr.Variable(_helper.Previous());
        }

    if (_helper.Match(TokenType.LEFT_PAREN)) {
      _expr expr = Expression();
      _error.Consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
      return new _expr.Grouping(expr);
        }
        throw _error.AnError(_helper.Peek(), "Expect expression.");
    }

    Stmt Statement() {
        if (_helper.Match(TokenType.PRINT)) return PrintStatement();
        if (_helper.Match(TokenType.LEFT_BRACE)) return new Stmt.Block(Block());
        return ExpressionStatement();
    }

    List<Stmt> Block() {
        List<Stmt> statements = new();

        while (!_helper.Check(TokenType.RIGHT_BRACE) && !_helper.IsAtEnd()) {
        statements.Add(Declaration());
        }

        _error.Consume(TokenType.RIGHT_BRACE, "Expect '}' after block.");
        return statements;
    }
    
    Stmt PrintStatement() {
        _expr value = Expression();
        _error.Consume(TokenType.SEMICOLON, "Expect ';' after value.");
        return new Stmt.Print(value);
    }
    Stmt ExpressionStatement() {
        _expr expr = Expression();
        _error.Consume(TokenType.SEMICOLON, "Expect ';' after expression.");
        return new Stmt.Expression(expr);
    }

    Stmt Declaration() {
        try {
        if (_helper.Match(TokenType.LET)) return letDeclaration();

        return Statement();
        } catch (Exception error) {
            _error.Synchronize();
        return null;
        }
    }

    Stmt letDeclaration() {
        Token name = _error.Consume(TokenType.IDENTIFIER, "Expect variable name.");

        _expr initializer = null;
        if (_helper.Match(TokenType.EQUAL)) {
        initializer = Expression();
        }

        _error.Consume(TokenType.SEMICOLON, "Expect ';' after variable declaration.");
        return new Stmt.Let(name, initializer);
    }


    public List<Stmt> parse() {
        List<Stmt> statements = new() ;
        while (!_helper.IsAtEnd()) {
        statements.Add(Declaration());
        }

        return statements; 
    }
}
