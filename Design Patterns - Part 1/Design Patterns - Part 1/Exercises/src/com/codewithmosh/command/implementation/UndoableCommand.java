package com.codewithmosh.command.implementation;

public interface UndoableCommand extends Command{
    void unExecute();
}
