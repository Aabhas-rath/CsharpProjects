package com.codewithmosh.iterator.Implementation;

public interface Iterator<E> {
    boolean HasNext();
    E Current();
    void Next();
}
