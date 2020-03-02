package com.codewithmosh.flyweight.implimentation;

import java.util.HashMap;
import java.util.Map;

public class FontFactory {
    Map<String,Font> Fonts = new HashMap<>();


    public Font getFont(String fontFamilyName){
        if (!Fonts.containsKey(fontFamilyName))
        {
            Fonts.put(fontFamilyName,new Font(fontFamilyName,12));
        }
        return Fonts.get(fontFamilyName);
    }
}
