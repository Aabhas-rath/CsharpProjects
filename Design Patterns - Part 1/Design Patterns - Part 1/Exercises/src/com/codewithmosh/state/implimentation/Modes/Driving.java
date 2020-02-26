package com.codewithmosh.state.implimentation.Modes;

import com.codewithmosh.state.implimentation.TravelMode;

public class Driving extends TravelMode {
    @Override
    public Object getEta() {
        System.out.println("Calculating ETA (driving)");
        return 1;
    }

    @Override
    public Object getDirection() {
        System.out.println("Calculating Direction (driving)");
        return 1;
    }
}
