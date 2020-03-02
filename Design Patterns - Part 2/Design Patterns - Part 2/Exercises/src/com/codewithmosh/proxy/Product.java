package com.codewithmosh.proxy;

import com.codewithmosh.proxy.implimentation.IProduct;

public class Product implements IProduct {
  private int id;
  private String name;

  public Product(int id) {
    this.id = id;
  }

  public int getId() {
    return id;
  }

  public String getName() {
    return name;
  }

  public void setName(String name) {
    this.name = name;
  }
}
