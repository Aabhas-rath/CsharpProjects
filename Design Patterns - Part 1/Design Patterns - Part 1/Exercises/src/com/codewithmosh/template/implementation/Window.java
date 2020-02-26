package com.codewithmosh.template.implementation;

public class Window extends GUIObject {
    @Override
    protected void doBeforeClose(){
        System.out.println("done before closing window");
    }
    @Override
    protected void doAfterClose(){
        System.out.println("done after closing window");
    }
}
