package com.codewithmosh.template.implementation;

public abstract class GUIObject {
    public void close() {
        doBeforeClose();

        System.out.println("Removing the window from the screen");

        doAfterClose();
    }
    protected void doBeforeClose(){
        System.out.println("done before close");
    }
    protected void doAfterClose(){
        System.out.println("done after close");
    }

}
