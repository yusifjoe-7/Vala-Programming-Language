using SharpVala.Lexing.Tokens;
using SharpVala.Lexing.Expr;

using _expr = global::SharpVala.Lexing.Expr.Expr;

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
        return Equality();
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

    if (_helper.Match(TokenType.LEFT_PAREN)) {
      _expr expr = Expression();
      _error.Consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
      return new _expr.Grouping(expr);
        }
        throw _error.AnError(_helper.Peek(), "Expect expression.");
    }

    public _expr parse() {
    try {
      return Expression();
    } catch (ErrorHandler.ParseError error) {
      return null;
    }
  }
}
