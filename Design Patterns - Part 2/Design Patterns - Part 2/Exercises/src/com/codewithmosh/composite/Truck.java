package com.codewithmosh.composite;

import com.codewithmosh.composite.implimentation.resource;

public class Truck implements resource {
  @Override
  public void deploy() {
    System.out.println("Deploying a truck");
  }
}
