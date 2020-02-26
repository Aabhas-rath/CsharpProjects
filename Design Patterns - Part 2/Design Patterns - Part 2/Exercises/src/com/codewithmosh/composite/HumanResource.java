package com.codewithmosh.composite;

import com.codewithmosh.composite.implimentation.resource;

public class HumanResource implements resource {
  @Override
  public void deploy() {
    System.out.println("Deploying a human resource");
  }
}
