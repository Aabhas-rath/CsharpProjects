package com.codewithmosh.state.implimentation.Modes;

import com.codewithmosh.state.implimentation.TravelMode;

public class Bicycling extends TravelMode {
    @Override
    public Object getEta() {
        System.out.println("Calculating ETA (bicycling)");
        return 1;
    }

    @Override
    public Object getDirection() {
        System.out.println("Calculating Direction (bicycling)");
        return 1;
    }
}
