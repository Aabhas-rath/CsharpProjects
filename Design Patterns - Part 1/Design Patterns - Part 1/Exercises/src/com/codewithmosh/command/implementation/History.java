package com.codewithmosh.command.implementation;

import com.codewithmosh.iterator.Implementation.Iterator;

import java.util.Stack;

public abstract class History<E> {
    protected Stack<E> undoStack = new Stack<E>();
    protected Stack<E> redoStack = new Stack<E>();

    public Iterator<E> createIterator(){
        return new ListIterator();
    }

    public void push(E object){
        undoStack.push(object);
    }
    public E pop() throws Exception {
        if (!undoStack.empty()) {
            var popped = undoStack.pop();
            redoStack.push(popped);
            return popped;
        }
        else
            throw new Exception("Empty");
    }

    public abstract E undo(E object);
    public abstract E redo(E object);

    public class ListIterator implements Iterator<E>{
        private int index;
        @Override
        public boolean HasNext() {
            return (index < undoStack.size());
        }

        @Override
        public E Current() {
            return undoStack.get(index);
        }

        @Override
        public void Next() {
            index++;
        }
    }
}
