package com.codewithmosh.flyweight;

import com.codewithmosh.flyweight.implimentation.Font;

public class Cell {
  private final int row;
  private final int column;
  private String content;
  private Font font;

  private boolean isBold;

  public Cell(int row, int column) {
    this.row = row;
    this.column = column;
  }

  public String getContent() {
    return content;
  }

  public void setContent(String content) {
    this.content = content;
  }

  public boolean isBold() {
    return isBold;
  }

  public void setBold(boolean bold) {
    isBold = bold;
  }

  public Font getFont() {
    return font;
  }

  public void setFont(Font font) {
    this.font = font;
  }
  public void render() {
    System.out.printf("(%d, %d): %s [%s]\n", row, column, content, font.getFontFamily());
  }
}
