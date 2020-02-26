package com.codewithmosh.state.implimentation.Modes;

import com.codewithmosh.state.implimentation.TravelMode;

public class Transist extends TravelMode {
    @Override
    public Object getEta() {
        System.out.println("Calculating ETA (transit)");
        return 1;
    }

    @Override
    public Object getDirection() {
        System.out.println("Calculating Direction (transit)");
        return 1;
    }
}
