package com.codewithmosh.state.implimentation.Modes;

import com.codewithmosh.state.implimentation.TravelMode;

public class Walking extends TravelMode {
    @Override
    public Object getEta() {
        System.out.println("Calculating ETA (walking)");
        return 1;
    }

    @Override
    public Object getDirection() {
        System.out.println("Calculating Direction (walking)");
        return 1;
    }
}
