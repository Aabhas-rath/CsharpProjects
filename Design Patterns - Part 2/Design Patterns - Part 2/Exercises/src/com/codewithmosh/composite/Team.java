package com.codewithmosh.composite;

import com.codewithmosh.composite.implimentation.resource;

import java.util.ArrayList;
import java.util.List;

public class Team implements resource {
  private List<resource> resources = new ArrayList<>();

  public void add(resource resource) {
    resources.add(resource);
  }
  @Override
  public void deploy() {
    for (var resource : resources)
      resource.deploy();
  }
}
