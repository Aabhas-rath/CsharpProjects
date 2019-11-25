package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.*;

/**
 * Holder class for a well's place
 */
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
public class WellPlace {
    /**
     * the well's location string
     */
    String location;
    /**
     * the well's latitude
     */
    double lat;

    /**
     * the well's longitude
     */
    double lng;
}
